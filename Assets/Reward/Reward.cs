using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Reward : MonoBehaviour
{
    Item selectedItem;
    public enum Item
    {
        attboost,
        Shieldboost,
        healboost,
        Pinkboost,
        bombboost,
        machinegun
    }

    Item[] allItems = (Item[])Enum.GetValues(typeof(Item));
    List<Item> itemList = new List<Item>();
    public class ItemInfo
    {
        public string name;
        public string description;
    }

    ItemInfo[] itemInfo = new ItemInfo[]
    {
        new ItemInfo { name = "attboost", description = "공격력이 20% 강해집니다"},
        new ItemInfo { name = "Shieldboost", description = "10초간 방어력이 3 강해집니다" },
        new ItemInfo { name = "healboost", description = "회복력이 20% 강해집니다"},
        new ItemInfo { name = "Pinkboost", description = "10초간 회복패널이 더 많이 생성됩니다"},
        new ItemInfo { name = "bombboost", description = "10초간 폭탄패널이 더 많이 생성됩니다" },
        new ItemInfo { name = "machinegun", description = "공격력이 절반 감소하지만 모든적을 공격합니다" },
    };

    public Text buttonText1;
    public Text buttonText2;
    public Text buttonText3;

    void Start()
    {
        AddRandomItemsToList();
    }

    void Update()
    {
        
    }

    public void OnButtonClick1()
    {
        selectedItem = itemList[0];
        Debug.Log("Button 1 clicked. Selected item is: " + selectedItem.ToString());
    }

    public void OnButtonClick2()
    { 
        selectedItem = itemList[1];
        Debug.Log("Button 2 clicked. Selected item is: " + selectedItem.ToString());
    }

    public void OnButtonClick3()
    {
        selectedItem = itemList[2];
        Debug.Log("Button 3 clicked. Selected item is: " + selectedItem.ToString());
    }

    public void AddRandomItemsToList()
    {

        for (int i = 0; i < 3; i++)
        {
            Item randomItem = allItems[UnityEngine.Random.Range(0, allItems.Length)];
            if (!itemList.Contains(randomItem))
            {
                itemList.Add(randomItem);
                Debug.Log("Selected item is: " + randomItem);

            }
            else
            {
                i--;
            }
        }

        buttonText1.fontSize = 30;
        buttonText2.fontSize = 30;
        buttonText3.fontSize = 30;
        buttonText1.text = $"{itemList[0].ToString()}\n{GetItemDescription(itemList[0])}";
        buttonText2.text = $"{itemList[1].ToString()}\n{GetItemDescription(itemList[1])}";
        buttonText3.text = $"{itemList[2].ToString()}\n{GetItemDescription(itemList[2])}";
    }

// 아이템 이름을 기반으로 아이템 정보를 반환하는 함수
    private string GetItemDescription(Item item)
    {
        foreach (ItemInfo itemInfo in itemInfo)
        {
            if (itemInfo.name == item.ToString())
            {
                return $"({itemInfo.description})";
            }
        }

        return "";
    }

    private void ReewardSelection()
    {
        Player.attCoefficient += 5;
        Debug.Log("att is: " + Player.attCoefficient);

    }
}