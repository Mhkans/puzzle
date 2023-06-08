using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public Button Stage01; 
    public Button Stage02; 
    public Button Stage03; 
    public Button Stage04; 
    public Button Stage05;
    public Button Stage06;
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
        if (EnemySpawner.stage03clear == true) {
            Stage03.interactable = false;  
            if (EnemySpawner.stage04clear == true) {
                Stage04.interactable = false;  
            }
            else {
                Stage04.interactable = true; 
            }
            if (EnemySpawner.stage05clear == true) {
                Stage05.interactable = false;  
            }
            else {
                Stage05.interactable = true; 
            }

            if (BossMonster.isdead == true)
            {
                Stage06.interactable = false;
            }
            else
            {
                Stage06.interactable = true;
            }
        }
        else {
            Stage03.interactable = true; 
            Stage04.interactable = false; 
            Stage05.interactable = false;
            Stage06.interactable = false;
        }
        
    }
}
