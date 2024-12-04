using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public static PlayerLogic _playerLogic;                     

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

    // Start is called before the first frame update
    void Start()
    {
        if (_playerLogic == null){                      //Instantiatin the singleton so that other classes can access our playerLogic
            _playerLogic = this;
        }
        
        currentHp = maxHp;
        playerModel = playerModel.transform.GetChild(0).gameObject;
        playerController = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {       
            if (playerController.isGrounded){
                animator.SetBool("isFalling", false);
            } else {
                animator.SetBool("isFalling", true);
            }
            if ((playerController.velocity.x != 0 || playerController.velocity.z != 0) && playerController.isGrounded == true){
                animator.SetBool("isWalking", true);
            } else {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetButtonDown("Jump")){
                animator.SetBool("isJumping", true);
            } else if (Input.GetButtonUp("Jump")){
                animator.SetBool("isJumping", false);
            }
        
    }

    public void TakeDamage(int damageToTake){                   //General takeDamage method that will be used for enemies and hazards. If the player has less than 0 hp, they die, else they take the given damage
        if (currentHp > 0){
            currentHp -= damageToTake;
            animator.SetTrigger("damaged");                    //The animator will set the damaged trigger when the player takes damage.
            // StartCoroutine(characterFlash());    //This coroutine is started and plays out over the course of the 2 seconds invulnerability frame
            Invoke("damageTextureFlash", 0.3f);        //Texture pops up when the player takes damage
            damageTexture.layer = 0;
        }
        if (currentHp <= 0){
            Debug.Log("I'm dead");
        }
    }

    IEnumerator characterFlash(){
        for (int i = 0; i < 5; i++){
            playerModel.layer = 1;   //Enabling the mesh renderer so it looks like the character is blinking when they take damage
            yield return new WaitForSeconds(0.2f);
            playerModel.layer = 0;   
            yield return new WaitForSeconds(0.2f);
        }
        
    }

    void damageTextureFlash(){
        damageTexture.layer = 1;
    }


}
