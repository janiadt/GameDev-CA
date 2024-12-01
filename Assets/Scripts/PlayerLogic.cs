using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        if (_playerLogic == null){                      //Instantiatin the singleton so that other classes can access our playerLogic
            _playerLogic = this;
        }
        
        currentHp = maxHp;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageToTake){                   //General takeDamage method that will be used for enemies and hazards. If the player has less than 0 hp, they die, else they take the given damage
        if (currentHp > 0){
            currentHp -= damageToTake;
            animator.SetTrigger("damaged");                    //The animator will set the damaged trigger when the player takes damage. (SET UP PARTICLES HERE TOO)
        }
        if (currentHp <= 0){
            Debug.Log("I'm dead");
        }
    }
}
