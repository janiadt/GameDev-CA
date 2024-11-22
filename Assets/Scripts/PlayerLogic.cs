using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageToTake){
        if (currentHp > 0){
            currentHp -= damageToTake;
        }
        else {
            Debug.Log("I'm dead");
        }
    }
}
