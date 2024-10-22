using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class RotateScript : MonoBehaviour
{
    public float frequency;
    public float amp;

    public bool isRotate;
    Vector3 initPosit;
    // Start is called before the first frame update
    void Start()
    {
        initPosit = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (isRotate){
            transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
 

        transform.position = new Vector3(initPosit.x, Mathf.Sin(frequency * Time.time) * amp + initPosit.y, initPosit.z);
    }
}
