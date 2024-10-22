using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;

public class TeleporterScript : MonoBehaviour
{
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform endPos;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        endPos.GetComponent<BoxCollider>().enabled = false;
        
        Invoke("teleportPlayer", 0.5f);
        
    }

    private void teleportPlayer(){
        player.position = endPos.position;
        
        Invoke("teleportedBoolSet", 3f);
    }

    private void teleportedBoolSet(){
        endPos.GetComponent<BoxCollider>().enabled = true;
    }
}
