using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [SerializeField]
    private int damageToDeal;

    [SerializeField]
    private float waitBetweenDamageTicks;

    IEnumerator _dealDamage;

    // Start is called before the first frame update
    void Start()
    {
        waitBetweenDamageTicks = 2f;
        _dealDamage = dealDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player" && PlayerLogic._playerLogic.invincibilityFramesActive != true){
            StartCoroutine(_dealDamage);                //Starting the specific dealDamage coroutine when player enters the hazard          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("EXITED");   
            StopCoroutine(_dealDamage);                 //Coroutine stops when player leaves the hazard
            PlayerLogic._playerLogic.invincibilityFramesActive = false;
        }
    }

    IEnumerator dealDamage(){
        PlayerLogic._playerLogic.invincibilityFramesActive = true;        //Player has i-frames while they're taking damage from a hazard
        while (PlayerLogic._playerLogic.currentHp > 0){                     //While the player has more than 0 health, this runs
            PlayerLogic._playerLogic.TakeDamage(damageToDeal);                  
            Debug.Log(PlayerLogic._playerLogic.currentHp);
            yield return new WaitForSeconds(waitBetweenDamageTicks);
        }
        
    }
}
