using UnityEngine;
using UnityEngine.UIElements;

public class BlockRoot : MonoBehaviour {

	private AudioSource audio;
	

	public AudioClip sound;
	public AudioClip bombSound;
	public GameObject BlockPrefab = null; // 만들어야 할 블록의 Prefab.
	public BlockControl[,] blocks; // 그리드.
	public Block.COLOR blockColor;
	private GameObject main_camera = null; // 메인 카메라.
	private BlockControl grabbed_block = null; // 잡은 블록.
	private ScoreCounter score_counter = null; // 점수 카운터 ScoreCounter.
	protected bool is_vanishing_prev = false; // 앞에서 발화했는가?.
	public Player player = null;
	public Enemy enemys = null;
	public BossMonster boss = null;
	public BossEnemy BossEnemy = null;
	public EnemySpawner enemyspawner = null;
	private int DAMAGE = 4;
	void Start() {
		this.main_camera = GameObject.FindGameObjectWithTag("MainCamera");
		this.score_counter = this.gameObject.GetComponent<ScoreCounter>();
		GameObject playerObj = GameObject.FindWithTag("Player");
		player = playerObj.GetComponent<Player>();

		this.audio = this.gameObject.AddComponent<AudioSource>();

		enemyspawner = gameObject.GetComponent<EnemySpawner>();
		
	}


	void Update() {
		
		GameObject enemyObj = GameObject.FindWithTag("Enemy");
		enemys = enemyObj.GetComponent<Enemy>();
		boss = enemyObj.GetComponent<BossMonster>();
		BossEnemy = enemyObj.GetComponent<BossEnemy>();

		
		Vector3 mouse_position; // 마우스의 위치.
		this.unprojectMousePosition(out mouse_position, Input.mousePosition);
		// 가져온 마우스 위치를 하나의 Vector2에 저장한다.
		Vector2 mouse_position_xy =
			new Vector2(mouse_position.x, mouse_position.y);
		if(this.grabbed_block == null) { // 잡은 블록이 비어있으면.
			if(!this.is_has_falling_block()) {
				if(Input.GetMouseButtonDown(0)) { // 마우스 버튼이 눌렸다면.
					// blocks 배열의 모든 요소를 차례로 처리한다.
					foreach(BlockControl block in this.blocks)
					{
						int pinkcount = 0;
						int bluecount = 0;
						int greencount = 0;
						int yellowcount = 0;
						int blockcount = 0;
						if(! block.isGrabbable()) { // 블록을 잡을 수 없다면.
							continue; // 루프의 맨 앞으로 점프.
						}
						// 마우스 위치가 블록 영역 안에 없으면.
						if(!block.isContainedPosition(mouse_position_xy)) {
							continue; // 루프의 맨 앞으로 점프.
						}

						if (block.color == Block.COLOR.SPBLOCK01)
						{
							audio.clip = sound;
							audio.Play();
							int lx = block.i_pos.x;
							int rx = block.i_pos.x;
							int dy = block.i_pos.y;
							int uy = block.i_pos.y;
							
							// 블록의 왼쪽을 체크.
							for (int x = lx - 1; x >= 0; x--)
							{
								BlockControl nextBlock = this.blocks[x, block.i_pos.y];
								if (nextBlock.step == Block.STEP.FALL || nextBlock.next_step == Block.STEP.FALL)
								{
									break; // 루프 탈출.
								}

								if (nextBlock.step == Block.STEP.SLIDE || nextBlock.next_step == Block.STEP.SLIDE)
								{
									break; // 루프 탈출.
								}

								if (nextBlock.color == Block.COLOR.PINK)
								{
									pinkcount++;

								}

								if (nextBlock.color == Block.COLOR.BLUE)
								{
									bluecount++;

								}
								if (nextBlock.color == Block.COLOR.GREEN)
								{
									greencount++;

								}
								if (nextBlock.color == Block.COLOR.YELLOW)
								{
									yellowcount++;

								}
								if (nextBlock.color == Block.COLOR.SPBLOCK02)
								{
									foreach (Enemy enemy in EnemySpawner.Instance.enemies)
									{
										enemy.currentHp -= 15;
							
									}
									foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
									{
										enemy.currentHp -= 15;
							
									}
								}

								if (nextBlock.color == Block.COLOR.SPBLOCK01)
								{
									int slx = x;
									int srx = x;
									int sdy = block.i_pos.y;
									int suy = block.i_pos.y;

									for (int sy = sdy - 1; sy >= 0; sy--)
									{
										BlockControl snextBlock = this.blocks[x, sy];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE || snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

								
										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
									
										else
										{
											blockcount++;
										}
										sdy = sy;
									}

									// 블록의 위쪽을 체크.
									for (int sy = suy + 1; sy< Block.BLOCK_NUM_Y; sy++)
									{
										BlockControl snextBlock = this.blocks[x, sy];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE || snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
									
										else
										{
											blockcount++;
										}
										suy = sy;
									}
									for (int y = sdy; y <= suy; y++)
									{
										blocks[x, y].toVanishing();
									}

									
								}

								else
								{
									blockcount++;
								}
								lx = x;
								
							}
							

							// 블록의 오른쪽을 체크.
							for (int x = rx + 1; x < Block.BLOCK_NUM_X; x++)
							{
								BlockControl nextBlock = this.blocks[x, block.i_pos.y];
								if (nextBlock.step == Block.STEP.FALL || nextBlock.next_step == Block.STEP.FALL)
								{
									break; // 루프 탈출.
								}

								if (nextBlock.step == Block.STEP.SLIDE || nextBlock.next_step == Block.STEP.SLIDE)
								{
									break; // 루프 탈출.
								}

								
								if (nextBlock.color == Block.COLOR.PINK)
								{
									pinkcount++;

								}

								if (nextBlock.color == Block.COLOR.BLUE)
								{
									bluecount++;

								}
								if (nextBlock.color == Block.COLOR.GREEN)
								{
									greencount++;

								}
								if (nextBlock.color == Block.COLOR.YELLOW)
								{
									yellowcount++;

								}
								if (nextBlock.color == Block.COLOR.SPBLOCK02)
								{
									foreach (Enemy enemy in EnemySpawner.Instance.enemies)
									{
										enemy.currentHp -= 15;
							
									}
									foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
									{
										enemy.currentHp -= 15;
							
									}
								}

								if (nextBlock.color == Block.COLOR.SPBLOCK01)
								{
									int slx = x;
									int srx = x;
									int sdy = block.i_pos.y;
									int suy = block.i_pos.y;

									for (int sy = sdy - 1; sy >= 0; sy--)
									{
										BlockControl snextBlock = this.blocks[x, sy];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE || snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

								
										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
									
										else
										{
											blockcount++;
										}
										sdy = sy;
									}

									// 블록의 위쪽을 체크.
									for (int sy = suy + 1; sy< Block.BLOCK_NUM_Y; sy++)
									{
										BlockControl snextBlock = this.blocks[x, sy];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE || snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

								
										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
									
										suy = sy;
									}
									for (int y = sdy; y <= suy; y++)
									{
										blocks[x, y].toVanishing();
									}

								}
								else
								{
									blockcount++;
								}
								rx = x;
							}

							// 블록의 아래쪽을 체크.
							for (int y = dy - 1; y >= 0; y--)
							{
								BlockControl nextBlock = this.blocks[block.i_pos.x, y];
								if (nextBlock.step == Block.STEP.FALL || nextBlock.next_step == Block.STEP.FALL)
								{
									break; // 루프 탈출.
								}

								if (nextBlock.step == Block.STEP.SLIDE || nextBlock.next_step == Block.STEP.SLIDE)
								{
									break; // 루프 탈출.
								}

								
								if (nextBlock.color == Block.COLOR.PINK)
								{
									pinkcount++;

								}

								if (nextBlock.color == Block.COLOR.BLUE)
								{
									bluecount++;

								}
								if (nextBlock.color == Block.COLOR.GREEN)
								{
									greencount++;

								}
								if (nextBlock.color == Block.COLOR.YELLOW)
								{
									yellowcount++;

								}
								if (nextBlock.color == Block.COLOR.SPBLOCK02)
								{
									foreach (Enemy enemy in EnemySpawner.Instance.enemies)
									{
										enemy.currentHp -= 15;
							
									}
									foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
									{
										enemy.currentHp -= 15;
							
									}
								}

								if (nextBlock.color == Block.COLOR.SPBLOCK01)
								{
									int slx = block.i_pos.x;
									int srx = block.i_pos.x;
									int sdy = y;
									int suy = y;
									for (int sx = slx - 1; sx >= 0; sx--)
									{
										BlockControl snextBlock = this.blocks[sx, y];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE ||
										    snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
										else
										{
											blockcount++;
										}
										slx = sx;
									}
									for (int sx = srx + 1; sx < Block.BLOCK_NUM_X; sx++)
									{
										BlockControl snextBlock = this.blocks[sx, y];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE ||
										    snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}
										else
										{
											blockcount++;
										}
										srx = sx;
									}
									for (int x = slx; x <= srx; x++)
									{
										// 완성된 블록을 발화 상태로.
										this.blocks[x, y].toVanishing();
									}
								}
								else
								{
									blockcount++;
								}
								dy = y;
							}

							// 블록의 위쪽을 체크.
							for (int y = uy + 1; y < Block.BLOCK_NUM_Y; y++)
							{
								BlockControl nextBlock = this.blocks[block.i_pos.x, y];
								if (nextBlock.step == Block.STEP.FALL || nextBlock.next_step == Block.STEP.FALL)
								{
									break; // 루프 탈출.
								}

								if (nextBlock.step == Block.STEP.SLIDE || nextBlock.next_step == Block.STEP.SLIDE)
								{
									break; // 루프 탈출.
								}

								
								if (nextBlock.color == Block.COLOR.PINK)
								{
									pinkcount++;

								}

								if (nextBlock.color == Block.COLOR.BLUE)
								{
									bluecount++;

								}
								if (nextBlock.color == Block.COLOR.GREEN)
								{
									greencount++;

								}
								if (nextBlock.color == Block.COLOR.YELLOW)
								{
									yellowcount++;

								}
								if (nextBlock.color == Block.COLOR.SPBLOCK02)
								{
									foreach (Enemy enemy in EnemySpawner.Instance.enemies)
									{
										enemy.currentHp -= 15;
							
									}
									foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
									{
										enemy.currentHp -= 15;
							
									}
								}
								if (nextBlock.color == Block.COLOR.SPBLOCK01)
								{
									int slx = block.i_pos.x;
									int srx = block.i_pos.x;
									int sdy = y;
									int suy = y;
									for (int sx = slx - 1; sx >= 0; sx--)
									{
										BlockControl snextBlock = this.blocks[sx, y];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE ||
										    snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}

										slx = sx;
									}
									for (int sx = srx + 1; sx < Block.BLOCK_NUM_X; sx++)
									{
										BlockControl snextBlock = this.blocks[sx, y];
										if (snextBlock.step == Block.STEP.FALL || snextBlock.next_step == Block.STEP.FALL)
										{
											break; // 루프 탈출.
										}

										if (snextBlock.step == Block.STEP.SLIDE ||
										    snextBlock.next_step == Block.STEP.SLIDE)
										{
											break; // 루프 탈출.
										}

										if (nextBlock.color == Block.COLOR.PINK)
										{
											pinkcount++;

										}

										if (nextBlock.color == Block.COLOR.BLUE)
										{
											bluecount++;

										}
										if (nextBlock.color == Block.COLOR.GREEN)
										{
											greencount++;

										}
										if (nextBlock.color == Block.COLOR.YELLOW)
										{
											yellowcount++;

										}
										if (nextBlock.color == Block.COLOR.SPBLOCK02)
										{
											foreach (Enemy enemy in EnemySpawner.Instance.enemies)
											{
												enemy.currentHp -= 15;
							
											}
											foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
											{
												enemy.currentHp -= 15;
							
											}
										}

										srx = sx;
									}
									for (int x = slx; x <= srx; x++)
									{
										// 완성된 블록을 발화 상태로.
										this.blocks[x, y].toVanishing();
									}
								}

								uy = y;
							}

							for (int x = lx; x <= rx; x++)
							{
								// 완성된 블록을 발화 상태로.
								this.blocks[x, block.i_pos.y].toVanishing();
							}

							for (int y = dy; y <= uy; y++)
							{
								this.blocks[block.i_pos.x, y].toVanishing();
							}
							
							enemys.TakeDamage((greencount+bluecount+yellowcount)*DAMAGE);
							player.Heal(pinkcount*2);
						}

						// 처리 중인 블록을 grabbed_block에 등록.
						
						this.grabbed_block = block;
						// 잡았을 때의 처리를 실행.
						this.grabbed_block.beginGrab();
						break;
					}
					
				}
			}
		} else { // 잡은 블록이 비어있지 않으면.


			do {
				// 슬라이드할 곳의 블록을 획득.
				BlockControl swap_target =
					this.getNextBlock(grabbed_block, grabbed_block.slide_dir);
				// 슬라이드할 곳의 블록이 비었으면.
				if(swap_target == null) {
					break; // 루프 탈출.
				}
				// 슬라이드할 곳의 블록이 잡을 수 있는 상태가 아니면.
				if(! swap_target.isGrabbable()) {
					break; // 루프 탈출.
				}
				// 현재 위치에서 슬라이드할 곳까지의 거리를 구한다.
				float offset = this.grabbed_block.calcDirOffset(
					mouse_position_xy, this.grabbed_block.slide_dir);
				// 이동 거리가 블록 크기의 절반보다 작으면.
				if(offset < Block.COLLISION_SIZE / 2.0f) {
					break; // 루프 탈출.
				}
				// 블록을 교체한다.
				this.swapBlock(
					grabbed_block, grabbed_block.slide_dir, swap_target);
				this.grabbed_block = null; // 지금은 블록을 잡고 있지 않다.
			} while(false);



			if(! Input.GetMouseButton(0)) { // 마우스 버튼이 눌려있지 않으면.
				this.grabbed_block.endGrab(); // 블록을 놓았을 때 처리를 실행.
				this.grabbed_block = null; // grabbed_block를 비어 있게 설정.
			}
		}

		// 낙하 중 또는 슬라이드 중이면.
		if(this.is_has_falling_block() || this.is_has_sliding_block()) {
			// 아무것도 하지 않는다.
			// 낙하 중도 슬라이드 중도 아니면.
		} else {
			/*int ignite_count = 0;

			// Iterate through all the blocks within the grid
			foreach (BlockControl block in this.blocks)
			{
				if (!block.isIdle())
				{
					continue;
				}

				// Check if the block is the one clicked with the left mouse button
				if (Input.GetMouseButtonDown(0))
				{
					// Check for connections if the block is the correct one
					if (this.checkConnection(block))
					{
						ignite_count++;
						if (block.color == Block.COLOR.SPBLOCK02)
						{
							audio.clip = bombSound;
							audio.Play();
						}
						else
						{
							audio.clip = sound;
							audio.Play();
						}
					}
				}
			}*/
			int ignite_count = 0;

			if (Input.GetMouseButtonDown(0))
			{

				Vector3 clickPosition;
				if (unprojectMousePosition(out clickPosition, Input.mousePosition))
				{
					
					// Iterate through all the blocks within the grid
					foreach (BlockControl block in this.blocks)
					{
						

						if (block.isContainedPosition(clickPosition))
						{
							
							if (checkConnection02(block))
							{
								
								ignite_count++;
								if (block.color == Block.COLOR.SPBLOCK02)
								{
									audio.clip = bombSound;
									audio.Play();
								}
								else
								{
									audio.clip = sound;
									audio.Play();
								}
							}
							
						}
						
					}
					
				}
				
			}


			if(ignite_count > 0) { // 점화 수가 0보다 크면.

				if(! this.is_vanishing_prev) {
					// 연속 점화가 아니라면, 점화 횟수를 리셋.
					this.score_counter.clearIgniteCount();
				}
				// 점화 횟수를 증가한다.
				this.score_counter.addIgniteCount(ignite_count);
				// 합계 점수 갱신.
				this.score_counter.updateTotalScore(); // * 여기 변경

				// = 한 군데라도 짝이 맞으면.
				int block_count = 0; 
				int blueCount = 0;
				int pinkCount = 0;
				int YellowCount = 0;
				int GreenCount = 0;// 발화 중인 블록 수(다음 장에서 사용한다).
				// 그리드 내의 모든 블록에 대해서 처리.
				foreach(BlockControl block in this.blocks) {
					if(block.isVanishing()) { // 발화 중(계속 타고 있는 중)이면.
						block.rewindVanishTimer(); // 재발화！.
						 // 발화 중인 블록 개수를 증가.  이 코드에서 foreach 쪽에다 넣어야해?
						if (EnemySpawner.Instance.enemies.Count > 0)
					{
						if (block.color == Block.COLOR.PINK)
						{
							pinkCount++;
							player.Heal(pinkCount);
							
						}
						else if (block.color == Block.COLOR.BLUE && enemys.status == Enemy.Status.Greenstat)
						{
							blueCount++;
							if (Reward.isadditionalblow == false)
							{
								if (Reward.ismachinegun == true)
								{
									
									enemys.TakeAll(blueCount*DAMAGE);
								}
								else
								{
									enemys.TakeDamage(blueCount * 2*DAMAGE);

								}
							}
							else
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(blueCount*DAMAGE);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeAll(blueCount*DAMAGE *Reward.additionalcount); 
									}

								}
								else
								{
									enemys.TakeDamage(blueCount *DAMAGE*2);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeDamage(blueCount* DAMAGE*2 *Reward.additionalcount); 
									}

								}
							}


						}
						else if (block.color == Block.COLOR.YELLOW && enemys.status == Enemy.Status.Bluestat)
						{
							YellowCount++;
							if (Reward.isadditionalblow == false)
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(YellowCount *DAMAGE);
								}
								else
								{
									enemys.TakeDamage(YellowCount *DAMAGE*2);

								}
							}
							else
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(YellowCount *DAMAGE);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeAll(YellowCount* DAMAGE*2 *Reward.additionalcount); 
									}

								}
								else
								{
									enemys.TakeDamage(YellowCount *DAMAGE*2);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeDamage(YellowCount*DAMAGE*2 *Reward.additionalcount); 
									}

								}
							}

						}
						else if (block.color == Block.COLOR.GREEN && enemys.status == Enemy.Status.Yellowstat)
						{
							GreenCount++;
							if (Reward.isadditionalblow == false)
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(GreenCount*DAMAGE);
								}
								else
								{
									enemys.TakeDamage(GreenCount*DAMAGE*2);

								}
							}
							else
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(GreenCount *DAMAGE);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeAll(GreenCount *DAMAGE *Reward.additionalcount); 
									}

								}
								else
								{
									enemys.TakeDamage(GreenCount*DAMAGE*2);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeDamage(GreenCount *DAMAGE*2 *Reward.additionalcount); 
									}

								}
							}


						}
						else if (block.color == Block.COLOR.SPBLOCK02)
						{
							foreach (Enemy enemy in EnemySpawner.Instance.enemies)
							{
								enemy.currentHp -= 15;
							}
							foreach (BossEnemy enemy in EnemySpawner.Instance.SummonedEnemy)
							{
								enemy.currentHp -= 15;
								
							}
						}
						else
						{
							block_count++;
							if (Reward.isadditionalblow == false)
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(block_count);
								}
								else
								{
									enemys.TakeDamage(block_count);

								}
							}
							else
							{
								if (Reward.ismachinegun == true)
								{
									enemys.TakeAll(block_count);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeAll(block_count*Reward.additionalcount); 
									}

								}
								else
								{
									enemys.TakeDamage(block_count*2);
									if (UnityEngine.Random.Range(0, 10) < 3)
									{
										enemys.TakeDamage(block_count* 2 *Reward.additionalcount); 
									}

								}
							}


						}
					}
					}
				}
			}
		}

		// 하나라도 연소 중인 블록이 있는가?.
		bool is_vanishing = this.is_has_vanishing_block();
		// 조건이 충족되면 블록을 떨어뜨리고 싶다.
		do {
			if(is_vanishing) { // 연소 중인 블록이 있으면.
				break; // 낙하 처리 하지 않는다.
			}
			if(this.is_has_sliding_block()) { // 교체 중인 블록이 있으면.
				break; // 낙하 처리는 하지 않는다.
			}
			for(int x = 0; x < Block.BLOCK_NUM_X; x++) {
				// 열에 교체 중인 블록이 있으면 그 열은 처리하지 않고 다음 열로 넘어간다.
				if(this.is_has_sliding_block_in_column(x)) {
					continue;
				}
				// 그 열에 있는 블록을 위에서부터 검사.
				for(int y = 0; y < Block.BLOCK_NUM_Y - 1; y++) {
					// 지정 중인 블록이 비표시면 다음 블록으로.
					if(! this.blocks[x, y].isVacant()) {
						continue;
					}
					// 지정 중인 블록 아래에 있는 블록을 검사.
					for(int y1 = y + 1; y1 < Block.BLOCK_NUM_Y; y1++) {
						// 아래에 있는 블록이 비표시면 다음 블록으로.
						if(this.blocks[x, y1].isVacant()) {
							continue;
						}
						// 블록을 교체한다.
						this.fallBlock(this.blocks[x, y], Block.DIR4.UP,
						               this.blocks[x, y1]);
						break;
					}
				}
			}
			// 보충 처리.
			for(int x = 0; x < Block.BLOCK_NUM_X; x++) {
				int fall_start_y = Block.BLOCK_NUM_Y;
				for(int y = 0; y < Block.BLOCK_NUM_Y; y++) {
					// 비표시 블록이 아니면 다음 블록으로.
					if(! this.blocks[x, y].isVacant()) {
						continue;
					}
					this.blocks[x, y].beginRespawn(fall_start_y); // 블록 부활.
					fall_start_y++;
				}
			}
		} while(false);
		this.is_vanishing_prev = is_vanishing;
	}






	public void initialSetUp()
	{
		this.blocks =
			new BlockControl [Block.BLOCK_NUM_X, Block.BLOCK_NUM_Y];
		// 블록의 색 번호.
		int color_index = 0;
		for(int y = 0; y < Block.BLOCK_NUM_Y; y++) { // 시작행부터 마지막행까지.
			for(int x = 0; x < Block.BLOCK_NUM_X; x++) {// 왼쪽 끝에서 오른쪽 끝까지.
				// BlockPrefab의 인스턴스를 씬 상에 만든다.
				GameObject game_object =
					Instantiate(this.BlockPrefab) as GameObject;
				// 위에서 만든 블록의 BlockControl 클래스를 얻는다.
				BlockControl block = game_object.GetComponent<BlockControl>();
				// 블록을 칸에 저장.
				this.blocks[x, y] = block;
				// 블록의 위치 정보(그리드 좌표)를 설정.
				block.i_pos.x = x;
				block.i_pos.y = y;
				// 각 BlockControl이 연계하는 GameRoot는 자신이라고 설정.
				block.block_root = this;
				// 그리드 좌표를 실제 위치(씬 상의 좌표)로 변환.
				Vector3 position = BlockRoot.calcBlockPosition(block.i_pos);
				// 씬 상의 블록 위치를 이동.
				block.transform.position = position;
				// 낮은확률로 SPBlock 생성
				if (Random.Range(0, 100) < 3) {
					block.setColor(Block.COLOR.SPBLOCK01);
				}
				else if (Random.Range(0, 100) < 10) {
					block.setColor(Block.COLOR.SPBLOCK02);
				}
				else {
					// 블록의 색을 변경.
					block.setColor((Block.COLOR)color_index);
					// 모든 종류의 색 중에서 랜덤하게 한 색을 선택.
					color_index = Random.Range(0, (int)Block.COLOR.NORMAL_COLOR_NUM);
				}
				// 블록의 이름을 설정(후술).
				block.name = "block(" + block.i_pos.x.ToString() +
					"," + block.i_pos.y.ToString() + ")";
				// 모든 종류의 색 중에서 랜덤하게 한 색을 선택.
				color_index =
					Random.Range(0, (int)Block.COLOR.NORMAL_COLOR_NUM);
			}
		}
	}


	// 지정된 그리드 좌표에서 씬 상의 좌표를 구한다.
	public static Vector3 calcBlockPosition(Block.iPosition i_pos) {
		// 배치할 왼쪽 상단 모서리 위치를 초깃값으로 설정한다.
		Vector3 position = new Vector3(-(Block.BLOCK_NUM_X / 2.0f - 0.5f),
		                               -(Block.BLOCK_NUM_Y / 2.0f - 0.5f), 0.0f);
		// 초깃값＋그리드 좌표×블록 크기.
		position.x += (float)i_pos.x * Block.COLLISION_SIZE;
		position.y += (float)i_pos.y * Block.COLLISION_SIZE;
		return(position); // 씬 상의 좌표를 반환한다.
	}


	public bool unprojectMousePosition(	out Vector3 world_position, Vector3 mouse_position)
	{
		bool ret;
		// 판을 생성. 이 판은 카메라에 대해서 뒤쪽 방향(Vector3.back)에서.
		// 블록의 절반크기만큼 앞에 둔다.
		Plane plane = new Plane(Vector3.back, new Vector3(
			0.0f, 0.0f, -Block.COLLISION_SIZE / 2.0f));
		// 카메라와 마우스를 통과하는 광선을 작성.
		Ray ray = this.main_camera.GetComponent<Camera>().ScreenPointToRay(
			mouse_position);
		float depth;
		// 광선(ray）이 판（plane）에 닿았다면.
		if(plane.Raycast(ray, out depth)) {
			// 인수 world_position을 마우스 위치로 덮어쓴다.
			world_position = ray.origin + ray.direction * depth;
			ret = true;
			// 닿지 않았다면.
		} else {
			// 인수 world_position을 제로인 벡터로 덮어쓴다.
			world_position = Vector3.zero;
			ret = false;
		}
		return(ret);
	}




	public BlockControl getNextBlock(
		BlockControl block, Block.DIR4 dir)
	{
		// 슬라이드할 곳의 블록을 이곳에 저장.
		BlockControl next_block = null;
		switch(dir) {
		case Block.DIR4.RIGHT:
			if(block.i_pos.x < Block.BLOCK_NUM_X - 1) {
			// 그리드 내라면.
			next_block = this.blocks[block.i_pos.x + 1, block.i_pos.y];
			}
			break;

		case Block.DIR4.LEFT:
			if(block.i_pos.x > 0) { // 그리드 내라면.
				next_block = this.blocks[block.i_pos.x - 1, block.i_pos.y];
			}
			break;
		case Block.DIR4.UP:
			if(block.i_pos.y < Block.BLOCK_NUM_Y - 1) { // 그리드 내라면.
				next_block = this.blocks[block.i_pos.x, block.i_pos.y + 1];
			}
			break;
		case Block.DIR4.DOWN:
			if(block.i_pos.y > 0) { // 그리드 내라면.
				next_block = this.blocks[block.i_pos.x, block.i_pos.y - 1];
			}
			break;
		}
		return(next_block);
	}

	public static Vector3 getDirVector(Block.DIR4 dir)
	{
		Vector3 v = Vector3.zero;
		switch(dir) {
		case Block.DIR4.RIGHT: v = Vector3.right; break; // 오른쪽으로 1단위 움직인다.
		case Block.DIR4.LEFT: v = Vector3.left; break; // 왼쪽으로 1단위 움직인다.
		case Block.DIR4.UP: v = Vector3.up; break; // 위로 1단위 움직인다.
		case Block.DIR4.DOWN: v = Vector3.down; break; // 아래로 1단위 움직인다.
		}
		v *= Block.COLLISION_SIZE; // 블록의 크기를 곱한다.
		return(v);
	}

	public static Block.DIR4 getOppositDir(Block.DIR4 dir)
	{
		Block.DIR4 opposit = dir;
		switch(dir) {
		case Block.DIR4.RIGHT: opposit = Block.DIR4.LEFT; break;
		case Block.DIR4.LEFT: opposit = Block.DIR4.RIGHT; break;
		case Block.DIR4.UP: opposit = Block.DIR4.DOWN; break;
		case Block.DIR4.DOWN: opposit = Block.DIR4.UP; break;
		}
		return(opposit);
	}



	public void swapBlock(BlockControl block0, Block.DIR4 dir, BlockControl block1)
	{
		// 각 블록 색을 기억해 둔다.
		Block.COLOR color0 = block0.color;
		Block.COLOR color1 = block1.color;
		// 각 블록의.
		// 확대율을 기억해 둔다.
		Vector3 scale0 =
			block0.transform.localScale;
		Vector3 scale1 =
			block1.transform.localScale;
		// 각 블럭의 '소멸시간'을 기억해 둔다.
		float vanish_timer0 = block0.vanish_timer;
		float vanish_timer1 = block1.vanish_timer;
		// 각 블록의 이동처를 구한다..
		Vector3 offset0 = BlockRoot.getDirVector(dir);
		Vector3 offset1 = BlockRoot.getDirVector(BlockRoot.getOppositDir(dir));
		block0.setColor(color1); // 색을 교체한다.
		block1.setColor(color0);
		block0.transform.localScale = scale1; // 확대율을 교체한다.
		block1.transform.localScale = scale0;
		block0.vanish_timer = vanish_timer1; // 「소멸시간」을 교체한다.
		block1.vanish_timer = vanish_timer0;
		block0.beginSlide(offset0); // 원래 블록의 이동을 시작.
		block1.beginSlide(offset1); // 이동할 곳의 블록의 이동을 시작.
	}

	public bool checkConnection(BlockControl start)
	{
		bool ret = false;
		int normal_block_num = 0;
		// 인수의 블록이 발화 후가 아니라면.
		if (!start.isVanishing())
		{
			normal_block_num = 1;
		}

		// 그리드 좌표를 기억해 둔다.
		int rx = start.i_pos.x;
		int lx = start.i_pos.x;
		// 블록의 왼쪽을 체크.


		for (int x = lx - 1; x > 0; x--)
		{
			BlockControl next_block = this.blocks[x, start.i_pos.y];
			if (next_block.color != start.color)
			{
				break;
			}

			if (next_block.step == Block.STEP.FALL || // 낙하 중이면.
			    next_block.next_step == Block.STEP.FALL)
			{
				break; // 루프 탈출.
			}

			if (next_block.step == Block.STEP.SLIDE || // 슬라이드 중이면.
			    next_block.next_step == Block.STEP.SLIDE)
			{
				break; // 루프 탈출.
			}

			if (!next_block.isVanishing())
			{
				// 발화 중이 아니라면.
				normal_block_num++; // 검사용 카운터를 증가.
			}

			lx = x;
		}

		// 블록의 오른쪽을 체크.
		for (int x = rx + 1; x < Block.BLOCK_NUM_X; x++)
		{
			BlockControl next_block = this.blocks[x, start.i_pos.y];
			if (next_block.color != start.color)
			{

				break;
			}

			if (next_block.step == Block.STEP.FALL ||
			    next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE ||
			    next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			rx = x;
		}

		do
		{
			// 오른쪽 블록의 그리드 번호 - 왼쪽 블록의 그리드 번호＋.
			// 중앙 블록（1）을 더한 수가 3미만이면.
			if (rx - lx + 1 < 3)
			{
				break; // 루프 탈출.
			}

			if (normal_block_num == 0)
			{
				// 발화 중이 아닌 블록이 하나도 없으면.
				break; // 루프 탈출.
			}

			for (int x = lx; x < rx + 1; x++)
			{
				// 완성된 같은 색 블록을 발화 상태로.
				this.blocks[x, start.i_pos.y].toVanishing();
				ret = true;
			}
		} while (false);

		normal_block_num = 0;
		if (!start.isVanishing())
		{
			normal_block_num = 1;
		}

		int uy = start.i_pos.y;
		int dy = start.i_pos.y;
		// 블록의 위쪽을 검사.
		for (int y = dy - 1; y > 0; y--)
		{
			BlockControl next_block = this.blocks[start.i_pos.x, y];
			if (next_block.color != start.color)
			{

				break;
			}

			if (next_block.step == Block.STEP.FALL ||
			    next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE ||
			    next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			dy = y;
		}

		// 블록의 아래쪽을 검사.
		for (int y = uy + 1; y < Block.BLOCK_NUM_Y; y++)
		{
			BlockControl next_block = this.blocks[start.i_pos.x, y];
			if (next_block.color != start.color)
			{

				break;
			}

			if (next_block.step == Block.STEP.FALL ||
			    next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE ||
			    next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			uy = y;
		}

		do
		{
			if (uy - dy + 1 < 3)
			{
				break;
			}

			if (normal_block_num == 0)
			{
				break;
			}

			for (int y = dy; y < uy + 1; y++)
			{
				this.blocks[start.i_pos.x, y].toVanishing();
				ret = true;
			}
		} while (false);


		return (ret);
	}

	public bool checkConnection02(BlockControl start)
	{
		bool ret = false;
		int normal_block_num = 0; // 인수의 블록이 발화 후가 아니라면.
		if (!start.isVanishing())
		{
			normal_block_num = 1;
		}

		// 그리드 좌표를 기억해 둔다.
		int rx = start.i_pos.x;
		int lx = start.i_pos.x;
		int uy = start.i_pos.y;
		int dy = start.i_pos.y;

		// 블록의 왼쪽을 체크.
		for (int x = lx - 1; x >= 0; x--)
		{
			BlockControl next_block = this.blocks[x, start.i_pos.y];
			if (next_block.color != start.color)
			{
				break;
			}

			if (next_block.step == Block.STEP.FALL || next_block.next_step == Block.STEP.FALL)
			{
				break; // 루프 탈출.
			}

			if (next_block.step == Block.STEP.SLIDE || next_block.next_step == Block.STEP.SLIDE)
			{
				break; // 루프 탈출.
			}

			if (!next_block.isVanishing())
			{
				// 발화 중이 아니라면.
				normal_block_num++; // 검사용 카운터를 증가.
			}

			lx = x;
		}

		// 블록의 오른쪽을 체크.
		for (int x = rx + 1; x < Block.BLOCK_NUM_X; x++)
		{
			BlockControl next_block = this.blocks[x, start.i_pos.y];
			if (next_block.color != start.color)
			{
				break;
			}

			if (next_block.step == Block.STEP.FALL || next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE || next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			rx = x;
		}

		// 블록의 위쪽을 검사.
		for (int y = dy - 1; y >= 0; y--)
		{
			BlockControl next_block = this.blocks[start.i_pos.x, y];
			if (next_block.color != start.color)
			{
				break;
			}

			if (next_block.step == Block.STEP.FALL || next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE || next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			dy = y;
		}

		// 블록의 아래쪽을 검사.
		for (int y = uy + 1; y < Block.BLOCK_NUM_Y; y++)
		{
			BlockControl next_block = this.blocks[start.i_pos.x, y];
			if (next_block.color != start.color)
			{
				break;
			}

			if (next_block.step == Block.STEP.FALL || next_block.next_step == Block.STEP.FALL)
			{
				break;
			}

			if (next_block.step == Block.STEP.SLIDE || next_block.next_step == Block.STEP.SLIDE)
			{
				break;
			}

			if (!next_block.isVanishing())
			{
				normal_block_num++;
			}

			uy = y;
		}

		if (rx - lx + 1 >= 3 || uy - dy + 1 >= 3)
		{
			if (normal_block_num > 0)
			{
				for (int x = lx; x <= rx; x++)
				{
					// 완성된 같은 색 블록을 발화 상태로.
					this.blocks[x, start.i_pos.y].toVanishing();
					ret = true;
				}

				for (int y = dy; y <= uy; y++)
				{
					this.blocks[start.i_pos.x, y].toVanishing();
					ret = true;
				}
				
			}
		}

		return ret;
	}


	private bool is_has_vanishing_block()
	{
		bool ret = false;
		foreach(BlockControl block in this.blocks) {
			if(block.vanish_timer > 0.0f) {
				ret = true;
				break;
			}
		}
		return(ret);
	}

	private bool is_has_sliding_block()
	{
		bool ret = false;
		foreach(BlockControl block in this.blocks) {
			if(block.step == Block.STEP.SLIDE) {
				ret = true;
				break;
			}
		}
		return(ret);
	}

	private bool is_has_falling_block()
	{
		bool ret = false;
		foreach(BlockControl block in this.blocks) {
			if(block.step == Block.STEP.FALL) {
				ret = true;
				break;
			}
		}
		return(ret);
	}

	

	public void fallBlock(
		BlockControl block0, Block.DIR4 dir, BlockControl block1)
	{
		// block0과 block1의 색, 크기, 소멸 시간, 표시/비표시, 상태를 기록.
		Block.COLOR color0 = block0.color;
		Block.COLOR color1 = block1.color;
		Vector3 scale0 = block0.transform.localScale;
		Vector3 scale1 = block1.transform.localScale;
		float vanish_timer0 = block0.vanish_timer;
		float vanish_timer1 = block1.vanish_timer;
		bool visible0 = block0.isVisible();
		bool visible1 = block1.isVisible();
		Block.STEP step0 = block0.step;
		Block.STEP step1 = block1.step;
		// block0과 block1의 각 속성을 교체한다.
		block0.setColor(color1);
		block1.setColor(color0);
		block0.transform.localScale = scale1;
		block1.transform.localScale = scale0;
		block0.vanish_timer = vanish_timer1;
		block1.vanish_timer = vanish_timer0;
		block0.setVisible(visible1);
		block1.setVisible(visible0);
		block0.step = step1;
		block1.step = step0;
		block0.beginFall(block1);
	}


	private bool is_has_sliding_block_in_column(int x)
	{
		bool ret = false;
		for(int y = 0; y < Block.BLOCK_NUM_Y; y++) {
			if(this.blocks[x, y].isSliding()) { // 슬라이드 중인 블록이 있으면.
				ret = true; // true를 반환한다.
				break;
			}
		}
		return(ret);
	}

	
}