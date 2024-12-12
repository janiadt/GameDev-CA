using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisions : MonoBehaviour
{
    public List<GameObject> collisionObjects = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision){
        collisionObjects.Add(collision.gameObject);
        Debug.Log("HIT");
    }

    private void OnTriggerExit(Collider collision){
        collisionObjects.Remove(collision.gameObject);
    }
}
