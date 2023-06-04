using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreCounter : MonoBehaviour {

  
    public struct Count { // 점수 관리용 구조체.
        public int ignite; // 착화 수.
        public int score; // 점수.
        public int total_socre; // 합계 점수.
    };

    public Count last; // 마지막(이번) 점수.
    public Count best; // 최고 점수.
    public GUIStyle guistyle; // 폰트 스타일.
    public BlockControl blockControl;
    public Player player = null;
    void Start() {
        this.last.ignite = 0;
        this.last.score = 0;
        this.last.total_socre = 0;
        this.guistyle.fontSize = 16;
        this.player = GameObject.FindObjectOfType<Player>();
    }

    void OnGUI()
    {
        /*int x = 20;
        //int y = 50;
        GUI.color = Color.black;
        //this.print_value(x + 20, y, "player 체력 :", player.currentHp); 
        y += 30;*/
        
    }
    public void print_value(int x, int y, string label, int value)
    {
        // label을 표시.
        GUI.Label(new Rect(x, y, 100, 20), label, guistyle);
        y += 15;
        // 다음 행에 value를 표시.
        GUI.Label(new Rect(x + 20, y, 100, 20), value.ToString(), guistyle);
        y += 15;
    }
    public void addIgniteCount(int count)
    {
        this.last.ignite += count; // 점화 수에 count를 더한다.
        this.update_score(); // 점수를 계산한다.
    }
    public void clearIgniteCount()
    {
        this.last.ignite = 0; // 점화 횟수 리셋.
    }
    private void update_score()
    {
       
    }
    public void updateTotalScore()
    {
        
    }
    

}