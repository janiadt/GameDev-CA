using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBehavior : MonoBehaviour
{   
    private NavMeshAgent enemy;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int currentHp;

    private bool isStunned;

    [SerializeField]
    private bool isAggroed;

    private UnityEngine.Vector3 originalPosition;

    private UnityEngine.Vector3 lastKnownPlayerPosition;

    private bool hasInvoked; //This variable makes sure the invoke only runs once

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        player = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        AggroManagement();
        if (isStunned == false && isAggroed == true){
            lastKnownPlayerPosition = player.transform.position;
            enemy.SetDestination(lastKnownPlayerPosition); // If the enemy isn't stunned, move to the player
        } else if (isAggroed == false){
            enemy.SetDestination(lastKnownPlayerPosition);
            if (hasInvoked == false){
                hasInvoked = true;
                StartCoroutine(BackToSpawn());
                
                
            }
            
        }
        
    }

    public IEnumerator TakeDamage(int damageToTake){                   //General takeDamage method that will be used for enemies and hazards. If the player has less than 0 hp, they die, else they take the given damage
        if (currentHp > 0){
            currentHp -= damageToTake;

            enemy.SetDestination(transform.position);
            isStunned = true;
            // animator.SetTrigger("damaged");                    //The animator will set the damaged trigger when the enemy takes damage. The animation should be shorter than the stun
            yield return new WaitForSeconds(2f);                // Enemies stop their pursuit for this many seconds
            isStunned = false;
            
            // Invoke("damageTextureFlash", 0.3f);        //Texture pops up when the player takes damage
            // damageTexture.layer = 0;
        }
        if (currentHp <= 0){
            // animator.SetBool("isDead", true); 
            Debug.Log("I'm dead");
            Destroy(gameObject);
            // isDead = true;
        }
    }

    void AggroManagement(){
        if (player.transform.position.x - transform.position.x < 5f && player.transform.position.z - transform.position.z < 5f){
            isAggroed = true;
            
        } else {
            isAggroed = false;

        }
        
    }

    IEnumerator BackToSpawn(){
        
        yield return new WaitForSeconds(10f);
        lastKnownPlayerPosition = originalPosition;
        enemy.SetDestination(lastKnownPlayerPosition);
        
        Debug.Log("INVOKE");

        hasInvoked = false;
    }
}
