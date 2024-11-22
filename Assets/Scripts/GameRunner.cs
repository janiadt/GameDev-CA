using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameRunner : MonoBehaviour
{   
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private TextMeshProUGUI playerThoughts;

    // This will govern how much of an item the player needs to collect.
    [SerializeField]
    private int objectiveNumber;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("textReveal", 2f);
        Invoke("textClear", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void textReveal(){
        playerThoughts.text = "I need to find 12 mushrooms...";
    }

    private void textClear(){
        playerThoughts.text = "";
    }
}
