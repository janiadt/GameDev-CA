using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupCounter : MonoBehaviour
{
    public static PickupCounter pickupCounter;

    [SerializeField]
    public int amountOfShrooms;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private UnityEngine.SceneManagement.Scene scene;
    // Start is called before the first frame update    
    void Awake(){
        if (pickupCounter == null)
        {
            pickupCounter = this;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(transform.root.gameObject);
        
        scene = SceneManager.GetActiveScene();

    }
    void Start()
    {   
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shroomIterate(){
        amountOfShrooms++;
        text.text = amountOfShrooms.ToString();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode){

        Debug.Log("Re-Initializing", this);

        text = GameObject.Find("ShroomCounter").GetComponent<TextMeshProUGUI>();
        amountOfShrooms = pickupCounter.amountOfShrooms;
        text.text = amountOfShrooms.ToString();
        Debug.Log(amountOfShrooms);
    }

}
