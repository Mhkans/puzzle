using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class button3 : MonoBehaviour
{
    Reward rewardInstance;
    Reward.Item selectedItem;
    
    void Start()
    {
        rewardInstance = FindObjectOfType<Reward>();
    }

    public void OnButtonClick3()
    {
        selectedItem = rewardInstance.itemList[2];
        Debug.Log("Button 3 clicked. Selected item is: " + selectedItem.ToString());
        rewardInstance.RewardSelection(selectedItem);
        SceneManager.LoadScene("STAGE01");
        if (selectedItem == Reward.Item.machinegun)
        {
            Reward.hasMachinegun = true;
        }
    }
}