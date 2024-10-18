using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float frequency;
    public float amp;

    Vector3 initPosit;
    // Start is called before the first frame update
    void Start()
    {
        initPosit = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);

        transform.position = new Vector3(initPosit.x, Mathf.Sin(frequency * Time.deltaTime) * amp, initPosit.z);
    }
}
