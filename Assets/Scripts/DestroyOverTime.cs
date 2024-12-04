using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject(){
        Destroy(gameObject);
    }
}
