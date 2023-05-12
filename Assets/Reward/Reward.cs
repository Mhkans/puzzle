using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reward : MonoBehaviour
{
  
    public enum Item {
        attboost,
        Shieldboost,
        healboost,
        Pinkboost,
        bombboost,
        changeBossStat,
        submachinegun
        
    }
    void Start()
    {
        AddRandomItemsToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddRandomItemsToList() {
        Item[] allItems = (Item[]) Enum.GetValues(typeof(Item)); 
        List<Item> itemList = new List<Item>(); // 아이템을 저장할 리스트 생성

        for (int i = 0; i < 3; i++) {
            Item randomItem = allItems[Random.Range(0, allItems.Length)]; // 랜덤으로 하나 선택
            if (!itemList.Contains(randomItem)) { // 이미 선택된 아이템이 아니면 리스트에 추가
                itemList.Add(randomItem);
            }
            else {
                i--; // 이미 선택된 아이템이면 i 값을 감소하여 다시 선택
            }
        }

        Debug.Log("Selected Items: " + string.Join(", ", itemList)); // 선택된 아이템 로그 출력
    }
}
