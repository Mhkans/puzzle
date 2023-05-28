using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public Button Stage01; 
    public Button Stage02; 
    public string SceneToLoad;
    
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.stage01clear == true) {
            Stage01.interactable = false;  
        }
        else {
            Stage01.interactable = true; 
        }
        if (EnemySpawner.stage02clear == true) {
            Stage02.interactable = false;  
        }
        else {
            Stage02.interactable = true; 
        }
    }
}
