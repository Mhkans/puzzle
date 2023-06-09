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
				if (BossMonster.isdead == true)
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
					if (EnemySpawner.stagecode == 1)
					{
						EnemySpawner.stage01clear = true;
					}
					else if (EnemySpawner.stagecode == 2)
					{
						EnemySpawner.stage02clear = true;
					}
					else if (EnemySpawner.stagecode == 3)
					{
						EnemySpawner.stage03clear = true;
					}
					else if (EnemySpawner.stagecode == 4)
					{
						EnemySpawner.stage04clear = true;
					}
					else if (EnemySpawner.stagecode == 5)
					{
						EnemySpawner.stage05clear = true;
					}
					
					this.next_step = STEP.REWARD;
					break;
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
					EnemySpawner.stage01clear = false;
					EnemySpawner.stage02clear = false;
					EnemySpawner.stage03clear = false;
					EnemySpawner.stage04clear = false;
					EnemySpawner.stage05clear = false;
					BossMonster.isdead = false;
					SceneManager.LoadScene("Clear");
					next_step = STEP.NONE;
					
					break;


				case STEP.GAMEOVER:
					EnemySpawner.stage01clear = false;
					EnemySpawner.stage02clear = false;
					EnemySpawner.stage03clear = false;
					EnemySpawner.stage04clear = false;
					EnemySpawner.stage05clear = false;
					BossMonster.isdead = false;

					SceneManager.LoadScene("Fail");

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
			
			GUI.color = Color.white;
			guistyle.fontSize = 50;
			guistyle.fontStyle = FontStyle.Bold;
			// 색상을 변경한 부분은 다음 두 줄입니다.
			guistyle.normal.textColor = Color.white;
			GUI.Label(new Rect(300f, 100.0f, 200.0f, 20.0f),
				"시간: " + Mathf.CeilToInt(step_timer).ToString() + "초",
				guistyle);
		
			break;
		
		}
	}


}
