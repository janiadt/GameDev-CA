using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// struct that keeps data of the events and what actions they will perform
[System.Serializable]
public class EventsAndActions{
    public Transform eventLocation;
    public string eventAction; 
    public float timeBeforeStarting;

    public List<GameObject> enemiesToSpawn;

    public List<ThoughtTimePair> eventThoughtTimeStruct;
    
}


// struct that keeps thoughts and time to next thought. This was we can manually set the text and time to next text in the unity editor
[System.Serializable]
public class ThoughtTimePair{
    public string thoughtString;
    public float timeToFadeAway;
    public float timeToThoughtAfterFadeAway; 
}

public class GameRunner : MonoBehaviour
{   
    public static GameRunner _gameRunner;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private TextMeshProUGUI playerThoughts;

    // This will govern how much of an item the player needs to collect.
    [SerializeField]
    private int objectiveNumber;

    [SerializeField]
    public List<EventsAndActions> eventsAndActions = new List<EventsAndActions>();

    [SerializeField]
    public List<ThoughtTimePair> thoughtTimePairs = new List<ThoughtTimePair>();

    [SerializeField]
    private float thoughtStartTime;

    // [SerializeField]
    // private List<Transform> gameEventColliders;

    // Start is called before the first frame update
    void Start()
    {
        if (_gameRunner == null)
        {
            _gameRunner = this;
        }

        StartCoroutine("textReveal", thoughtTimePairs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This coroutine will be called when an event is triggered, and the event action will be passed in. If the event action is a designated aciton, the action will be performed after the set time
    public IEnumerator gameEventAction(EventsAndActions eventsAndActions){
        yield return new WaitForSeconds(eventsAndActions.timeBeforeStarting);
        if (eventsAndActions.eventAction == "SpawnEnemies"){
            for (int i = 0; i < eventsAndActions.enemiesToSpawn.Count; i++){
                // For each enemy in the enemiestospawn array, instantiate a new enemy prefab at the event position
                var newEnemy = Instantiate(eventsAndActions.enemiesToSpawn[i], eventsAndActions.eventLocation);
                newEnemy.transform.position = eventsAndActions.eventLocation.transform.position + new Vector3(i, 0.5f, i);
            }
            // If the event action is "initiate thoughts", we start our textreveal coroutine with the event data
        } else if (eventsAndActions.eventAction == "InitiateThoughts"){
            StartCoroutine("textReveal", eventsAndActions.eventThoughtTimeStruct);
        }
    }

    IEnumerator textReveal(List<ThoughtTimePair> _thoughtTimePairs){
        // IEnumerator coroutine that changes the text in the character thoughts texbox based on how many texts there are to display one after another. 
        // It can also be triggered by an event
        yield return new WaitForSeconds(thoughtStartTime);
        for(int i = 0; i < _thoughtTimePairs.Count; i++){
            playerThoughts.text = _thoughtTimePairs[i].thoughtString;
            yield return new WaitForSeconds(_thoughtTimePairs[i].timeToFadeAway);
            playerThoughts.text = " ";
            yield return new WaitForSeconds(_thoughtTimePairs[i].timeToThoughtAfterFadeAway);
        }
        
    }
}
