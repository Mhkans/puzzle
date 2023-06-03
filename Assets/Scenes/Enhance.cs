using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enhance : MonoBehaviour
{
    public Text EnhanceText;
    
    // Start is called before the first frame update
    void Start()
    {
        EnhanceText =  GetComponent<Text>();
        EnhanceText.text = "보스의 공격력 : "+ 6 + "\n"+
                               "적의 공격력 : " + 3 +"\n\n" +
                           "    PLAYER  " + " \n " +
                           "\n현재 공격력 : " + 2 * Player.attCoefficient +
                           "\n현재 방어력 : "  + Player.shield+ " "+
                           "\n현재 회복력 : " + 10 * Player.healCoefficient +
                           "\n현재 체력 : " + 100 * Player.hpCoefficient +
                           "\n머신건 장착 : " + (Reward.ismachinegun ? "YES" : "NO") +
                           "\n추가타 적용 : " + (Reward.isadditionalblow ? "YES   \n" : "NO   \n") + Reward.additionalcount + "회 추가공격";




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
