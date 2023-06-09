using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Reward : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip stageSound;
    
    Item selectedItem;
    public enum Item
    {
        attboost,
        Shieldboost,
        healboost,
        hpboost,
        machinegun,
        additionalblow
    }
    List<Item> allItems = new List<Item> { Item.attboost, Item.Shieldboost, Item.healboost, Item.hpboost, Item.machinegun , Item.additionalblow };
    public List<Item> itemList = new List<Item>();
    public static Item[] items;
    public class ItemInfo
    {
        public string name;
        public string description;
        public ItemInfo(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }

    public Text buttonText1;
    public Text buttonText2;
    public Text buttonText3;
    public static bool ismachinegun = false;
    public static bool hasMachinegun = false;
    public static bool isadditionalblow = false;
    public static int additionalcount = 0;
    Dictionary<Item, ItemInfo> itemInfoDict = new Dictionary<Item, ItemInfo>()
    {
        { Item.attboost, new ItemInfo("attboost", "40%의 공격력 증가 효과를 받습니다.") },
        { Item.Shieldboost, new ItemInfo("Shieldboost", "방어력이 2 증가합니다.") },
        { Item.healboost, new ItemInfo("healboost", "50%의 체력 회복 증가 효과를 받습니다.") },
        { Item.hpboost, new ItemInfo("hpboost", "30%의 체력 증가 효과를 받습니다.") },
        { Item.machinegun, new ItemInfo("machinegun", "적에게 주는 데미지가 반으로 줄어들지만 모든 적을 공격합니다.(중첩불가능)") },
        { Item.additionalblow, new ItemInfo("additionalblow", "30%의 확률로 무속성 공격을 추가로 가합니다.(중첩가능)") },
    };
    void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        AddRandomItemsToList();
        audio.clip = stageSound;
        audio.Play();
    }

    void Update()
    {
        
    }

    public void AddRandomItemsToList()
    {

        
        for (int i = 0; i < 3; i++)
        {
            // 머신건이 이미 선택된 경우 다른 아이템을 랜덤하게 선택
            if (hasMachinegun)
            {
                allItems.Remove(Item.machinegun);
            }
            Item randomItem = allItems[UnityEngine.Random.Range(0, allItems.Count)];
            if (!itemList.Contains(randomItem))
            {
                itemList.Add(randomItem);
            }
            else
            {
                i--;
            }
            
        }

        buttonText1.fontSize = 30;
        buttonText2.fontSize = 30;
        buttonText3.fontSize = 30;
        buttonText1.text = itemList[0].ToString();
        buttonText2.text = itemList[1].ToString();
        buttonText3.text = itemList[2].ToString();
        buttonText1.text = itemList[0].ToString() + "\n"  + "\n"+ itemInfoDict[itemList[0]].description;
        buttonText2.text = itemList[1].ToString() + "\n"  + "\n"+ itemInfoDict[itemList[1]].description;
        buttonText3.text = itemList[2].ToString() + "\n"  + "\n" +itemInfoDict[itemList[2]].description;

    }

    // 아이템 이름을 기반으로 아이템 정보를 반환하는 함수
    public void RewardSelection(Item selectedItem)
    {
        switch (selectedItem)
        {
            case Item.attboost:
                Player.attCoefficient *= 1.4f;
                break;
            case Item.Shieldboost:
                Player.shield += 2;
                break;
            case Item.healboost:
                Player.healCoefficient *= 1.5f;
                break;
            case Item.hpboost:
                Player.hpCoefficient *= 1.3f;
                break;
            case Item.machinegun:
                ismachinegun = true;
                break;
            case Item.additionalblow:
                additionalblowcount();
                break;
            
        }
    }

    public static void additionalblowcount()
    {
        
        isadditionalblow = true;
        additionalcount += 1;

    }

    
    
}
