using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapcamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// lock rotation
		transform.rotation = Quaternion.Euler(Vector3.forward * 90);

	}
}
