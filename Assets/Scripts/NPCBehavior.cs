using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TextMeshProUGUI dialogue;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private BoxCollider passCollider;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(){
        dialogue.text = "Bring me 12 mushrooms";

        if (PickupCounter.pickupCounter.amountOfShrooms >= 12){
            dialogue.text = "You may pass, we will meet again";

            passCollider.isTrigger = true;
        }
    }

    void OnTriggerExit(){
        dialogue.text = "";
    }
}
