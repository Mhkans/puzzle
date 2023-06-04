using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private AudioSource audio1;

    public AudioClip TitleSound = null;
    public string SceneToLoad;
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.audio1 = this.gameObject.AddComponent<AudioSource>();
        audio1.clip = TitleSound;
        audio1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
