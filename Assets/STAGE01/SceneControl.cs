using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour {
	
	private BlockRoot block_root = null;
	private ScoreCounter score_counter = null;
	private Player player = null;
	private EnemySpawner enemy = null;
	public RestartButton restartbutton = null;
	public enum STEP { 
		NONE = -1,// 상태 정보 없음.
		PLAY ,// 플레이 중.(전투와 퍼즐게임을 처리)
		REWARD,// 적을 이기면 강화 보상 획득
		CLEAR, // 클리어.
		GAMEOVER, //게임오버 실패
		NUM, // 상태의 종류가 몇 개인지 나타낸다(= 2).
	};
	public STEP step = STEP.NONE; // 현재 상태.
	public STEP next_step = STEP.NONE; // 다음 상태.
	public float step_timer = 0.0f; // 경과 시간.
	private float clear_time = 0.0f; // 클리어 시간.
	public GUIStyle guistyle; // 폰트 스타일.



	void Start()
	{
		this.block_root = this.gameObject.GetComponent<BlockRoot>();
		this.block_root.initialSetUp();
		this.score_counter = this.gameObject.GetComponent<ScoreCounter>();
		this.next_step = STEP.PLAY; // 다음 상태를 '플레이 중'으로.
		this.guistyle.fontSize = 24; // 폰트  크기를 24로.
		this.player = GameObject.FindObjectOfType<Player>();
		this.enemy = GameObject.FindObjectOfType<EnemySpawner>();
	}




	void Update() {
		
		this.step_timer += Time.deltaTime;
		// 상태 변화 대기 -----.
		if(this.next_step == STEP.NONE) {
			switch(this.step) {
			case STEP.PLAY:
				// 클리어 조건을 만족하면.
				if (this.score_counter.isGameClear())
				{
					this.next_step = STEP.CLEAR; // 클리어 상태로 이행.

					break;
				}

				if (player.IsDead())
				{
					this.next_step = STEP.GAMEOVER; 
					
					break;
				}

				if (enemy.GetEnemyCount() == 0)
				{
					this.next_step = STEP.REWARD; 
					
				}
					
				break; 
			}
		}
		// 상태가 변화했다면 ------.
		while(this.next_step != STEP.NONE) {
			this.step = this.next_step;
			this.next_step = STEP.NONE;
			switch (this.step)
			{
				case STEP.CLEAR:
					// block_root를 정지.
					this.block_root.enabled = false;
					// 경과 시간을 클리어 시간으로 설정.
					this.clear_time = this.step_timer;
					break;


				case STEP.GAMEOVER:
					if (restartbutton != null)
					{
						restartbutton.gameObject.SetActive(true);
					}
					break;

			case STEP.REWARD:
					SceneManager.LoadScene("Reward");
				break;
			}

			this.step_timer = 0.0f;
		}
	}

	void OnGUI()
	{
		switch(this.step) {
		case STEP.PLAY:
			GUI.color = Color.black;
			// 경과 시간을 표시.
			GUI.Label(new Rect(960.0f, 50.0f, 200.0f, 20.0f),
			          "시간" + Mathf.CeilToInt(this.step_timer).ToString() + "초",
			          guistyle);
			GUI.color = Color.white;
			break;
		case STEP.CLEAR:
			GUI.color = Color.black;
			// 「☆클리어-！☆」라는 문자열을 표시.
			GUI.Label(new Rect(
				Screen.width/2.0f - 80.0f, 20.0f, 200.0f, 20.0f),
			          "☆클리어-!☆", guistyle);
			// 클리어 시간을 표시.
			GUI.Label(new Rect(
				Screen.width/2.0f - 80.0f, 40.0f, 200.0f, 20.0f),
			          "클리어 시간" + Mathf.CeilToInt(this.clear_time).ToString() +
			          "초", guistyle);
			GUI.color = Color.white;
			break;
		}
	}


}
