using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button1 : MonoBehaviour
{
    Reward rewardInstance;
    Reward.Item selectedItem;
    
    void Start()
    {
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
}