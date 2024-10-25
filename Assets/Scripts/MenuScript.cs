using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsText;
    [SerializeField]
    private GameObject menu;
    // Start is called before the first frame update
    void Awake(){
        creditsText.SetActive(false);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScene(){
        SceneManager.LoadScene("SwampLevel");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ShowCredits(){
        menu.SetActive(false);
        creditsText.SetActive(true);
    }

    public void CloseCredits(){
        menu.SetActive(true);
        creditsText.SetActive(false);
    }
}
