using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")){
            Debug.Log("TRIGGERED");
            PlayerLogic._playerLogic.TakeDamage(10000);
        }
    }
}
