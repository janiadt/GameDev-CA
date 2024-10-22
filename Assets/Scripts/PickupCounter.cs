using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class PickupCounter : MonoBehaviour
{
    public static PickupCounter pickupCounter;

    private int amountOfShrooms;

    private TextMeshProUGUI text;
    // Start is called before the first frame update    
    void Awake(){
        if (pickupCounter == null)
        {
            pickupCounter = this;
        }

        DontDestroyOnLoad(gameObject);
        amountOfShrooms = pickupCounter.amountOfShrooms;

    }
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shroomIterate(){
        amountOfShrooms++;
        text.text = amountOfShrooms.ToString();
    }

}
