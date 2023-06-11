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
        EnhanceText.text = /*"    ENEMY  " + " \n\n " +
                           "보스의 체력 : " + 500 +"\n" +
                           "보스의 공격력 : " + 10 +"\n" +
                           "보스의 방패 소환 주기 : " + EnemySpawner.spawnInterval +"초"+"\n" +
                           "방패의 체력 : " + 50 +"\n" +
                           "적의 체력 : " + 200 +"\n" +
                           "적의 공격력 : " + 5 +"\n\n" +*/
                           "    PLAYER  " + " \n " +
                           "\n현재 공격력 : 블럭당 " + 2 * Player.attCoefficient  +
                           "\n현재 회복력 : 블럭당" + 2 * Player.healCoefficient +
                           "\n속성 공격력 : 3블럭일때 " + 2 * Player.attCoefficient*8  +
                           "\n\n속성 블럭을 길게 연결할수록 \n더 강해집니다\n" +
                           "\n현재 방어력 : "  + Player.shield+ " "+
                           "\n현재 체력 : " + 100 * Player.hpCoefficient +
                           "\n머신건 장착 : " + (Reward.ismachinegun ? "YES" : "NO") +
                           "\n추가타 적용 : " + (Reward.isadditionalblow ? "YES   \n" : "NO   \n") + Reward.additionalcount + "회 추가공격";




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
