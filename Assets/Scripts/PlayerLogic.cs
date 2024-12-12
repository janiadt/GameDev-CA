using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

// Health Meter
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic _playerLogic;     //This is a singleton, which means only one player can exist at a time. Obviously this isn't multiplayer friendly, but it lets me set up events easier.                

    [SerializeField]
    private int maxHp;

    [SerializeField]
    public int currentHp {get; private set;}

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject playerModel;

    [SerializeField]
    private GameObject player;

    private CharacterController playerController;

    [SerializeField]
    private GameObject damageTexture;

    public bool invincibilityFramesActive;

    public bool isDead;

    [SerializeField]
    private GameObject attackBox;

    private bool justAttacked;

    private ObjectCollisions attackCollisions;

	[SerializeField] private Image HealthBar = null; //UI Element


    // Start is called before the first frame update
    void Start()
    {
        if (_playerLogic == null){                      //Instantiatin the singleton so that other classes can access our playerLogic
            _playerLogic = this;
        }
        
        currentHp = maxHp;
        playerModel = playerModel.transform.GetChild(0).gameObject;
        playerController = player.GetComponent<CharacterController>();
        attackCollisions = attackBox.GetComponent<ObjectCollisions>();
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetButtonDown("Fire1") && justAttacked == false && isDead != true){
            StartCoroutine(Attack());
        }
        
    }

    public void TakeDamage(int damageToTake){                   //General takeDamage method that will be used for enemies and hazards. If the player has less than 0 hp, they die, else they take the given damage
        if (currentHp > 0){
            currentHp -= damageToTake;
            animator.SetTrigger("damaged");                    //The animator will set the damaged trigger when the player takes damage.
            // StartCoroutine(characterFlash());    //This coroutine is started and plays out over the course of the 2 seconds invulnerability frame
            Invoke("DamageTextureFlash", 0.3f);        //Texture pops up when the player takes damage
            damageTexture.layer = 0;
			HealthBar.fillAmount = (float)(currentHp - 0.25f) / (maxHp - 0.25f); //Turns currentHp into a 0-1 value for the UI
			// If we change the sprite out on HealthBar we could do HealthBar.colour to change on low health?
        }
        if (currentHp <= 0){
            animator.SetBool("isDead", true); 
            Debug.Log("I'm dead");
            isDead = true;
        }
    }

    IEnumerator CharacterFlash(){
        for (int i = 0; i < 5; i++){
            playerModel.layer = 1;   //Enabling the mesh renderer so it looks like the character is blinking when they take damage
            yield return new WaitForSeconds(0.2f);
            playerModel.layer = 0;   
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    void DamageTextureFlash(){
        damageTexture.layer = 1;
    }

    IEnumerator Attack(){
        justAttacked = true;

        // Casting a overlap at the location of the attackBox. If any hitbox is overlapping this and its an enemy, it takes damage and gets added to a list, so it doesn't take extra damage
        Collider[] tempObjects = Physics.OverlapSphere(attackBox.transform.position, 2f);
        List<GameObject> alreadyHit = new List<GameObject>();

        // Test sphere

        // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // sphere.transform.position = attackBox.transform.position;
        // sphere.transform.localScale = new Vector3(1f, 1f, 1f);
        // sphere.GetComponent<SphereCollider>().radius = 2f;
        // sphere.GetComponent<SphereCollider>().enabled = false;

        
        for(int i = 0; i < tempObjects.Length; i++){
            if (tempObjects[i].gameObject.CompareTag("Enemy") && !alreadyHit.Contains(tempObjects[i].gameObject)){
                Debug.Log("HIT");
                tempObjects[i].gameObject.GetComponent<EnemyBehavior>().StartCoroutine("TakeDamage", 50);
            }
            Debug.Log(tempObjects[i].gameObject.name);
            alreadyHit.Add(tempObjects[i].gameObject);
        }

        animator.SetTrigger("attack");
        Debug.Log("ATTACK");      
        yield return new WaitForSeconds(0.3f);
        justAttacked = false;
    }
}
