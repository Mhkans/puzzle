using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSound : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip clickSound;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
       
    }

    // Update is called once per frame
    public void buttonclickSound()
    { 
        audio.clip = clickSound;
        audio.Play();
        
    }
}
