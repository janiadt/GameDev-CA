using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameEventScript : MonoBehaviour
{
    [SerializeField]
    public EventsAndActions eventAndActions;

    private bool hasHappened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && hasHappened == false){
            Debug.Log("TTTTTTTTTTTTTTTT");
            StartCoroutine(GameRunner._gameRunner.gameEventAction(eventAndActions));
            hasHappened = true;
        }
    }
}
