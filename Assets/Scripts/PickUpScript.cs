using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private bool isKeyItem;

    [SerializeField]
    private GameObject[] itemsToInfluence;

    private AudioSource itemAudio;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private bool hasParticles;

    // Start is called before the first frame update
    void Start()
    {
        itemAudio = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        itemAudio.PlayOneShot(audioClip, 1.3f);
        if (isKeyItem){
            Invoke("setItemsActive", 1f);
        }
        if (hasParticles){
            gameObject.transform.Find("Particle System").gameObject.SetActive(false);
        }
        gameObject.transform.Find("r").gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;

    }

    private void setItemsActive(){
        foreach (GameObject item in itemsToInfluence){
                item.SetActive(true);
        }
    }
}
