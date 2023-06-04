using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button1 : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip clickSound;
    Reward rewardInstance;
    Reward.Item selectedItem;
    
    void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        rewardInstance = FindObjectOfType<Reward>();
    }

    public void OnButtonClick1()
    {
        selectedItem = rewardInstance.itemList[0];
        Debug.Log("Button 1 clicked. Selected item is: " + selectedItem.ToString());
        rewardInstance.RewardSelection(selectedItem);
        SceneManager.LoadScene("Stage");
        if (selectedItem == Reward.Item.machinegun)
        {
            Reward.hasMachinegun = true;
        }
        
    }
    public void buttonclick()
    {
        this.audio.clip = this.clickSound;
        audio.Play();
    }
}