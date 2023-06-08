using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Enemy: MonoBehaviour
{
    public enum Status
    {
        Bluestat,Yellowstat,Greenstat,nomal
    }


    public Text enemyTEXT;
    public Slider enemyHP;
    public float MaxHp;
    public float currentHp;
    public Status status;
    private float attackTime;
    public float term;
    public int damage; // 5초마다 player에게 주는 데미지
    public GameObject statusObject = null;
    public virtual void Start()
    {
        MaxHp = 200;
        currentHp = MaxHp;
        attackTime = 0.0f;
        term = 5.0f;
        damage = 5;

    }
   
   void Update() 
    {
       
        if (Time.time > attackTime + term)
        {
            Attack();
            attackTime = Time.time;
        }

        handleHP();
        if (currentHp < 1)
        {
            Die();
        }
        
    }
    Status GetRandomStatus()
    {
        List<Enemy.Status> statuses = new List<Enemy.Status>() { Enemy.Status.Greenstat, Enemy.Status.Yellowstat, Enemy.Status.Bluestat };
        int randomIndex = Random.Range(0, statuses.Count);
        return statuses[randomIndex];
    }
    
    public void handleHP()
    {
       enemyHP.value = (float)currentHp / (float)MaxHp;
    }
   
   
    public void TakeDamage(float att)
    {
        Enemy enemy = null;
        
        if (EnemySpawner.Instance.SummonedEnemy.Count > 0) // 리스트에 적이 있어야 함
        {
            if (EnemySpawner.targetEnemy)
            {
                enemy = EnemySpawner.targetEnemy;
                
            }
            else
            {
                enemy = EnemySpawner.Instance.SummonedEnemy[0];
            }

            enemy.currentHp -= att * Player.attCoefficient;
           
        }
        else
        {
            if (EnemySpawner.Instance.enemies.Count > 0) // 리스트에 적이 있어야 함
            {
                if (EnemySpawner.targetEnemy)
                {
                    enemy = EnemySpawner.targetEnemy;
                    
                }
                else
                {
                    enemy = EnemySpawner.Instance.enemies[0];
                } 

                enemy.currentHp -= att * Player.attCoefficient;
            }
        }
    }
    public void TakeAll(float att)
    {
        if (EnemySpawner.Instance.SummonedEnemy.Count > 0)
        {
            foreach (Enemy enemy in EnemySpawner.Instance.SummonedEnemy)
            {
                if (enemy.currentHp > 0)
                {
                    enemy.currentHp -= att * Player.attCoefficient;
                }
            }
        }
        else
        {
            foreach (Enemy enemy in EnemySpawner.Instance.enemies)
            {
                if (enemy.currentHp > 0)
                {
                    enemy.currentHp -= att * Player.attCoefficient;
                }
            }
        }

        
        
    }
    public virtual void Die()
    {
        Destroy(gameObject);
        EnemySpawner.Instance.EnemyDestroyed(this);
        EnemySpawner.Instance.BossEnemyDestored(this);
        Destroy(enemyHP.gameObject);
        Destroy(statusObject);
    }
    public virtual void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        // 적의 위치를 저장
        Vector3 startPosition = transform.position;

        // 플레이어 위치로 이동
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Vector3 endPosition = playerObject.transform.position;
            float attackDistance = 1f; // 공격 거리
            endPosition = Vector3.MoveTowards(startPosition, endPosition, attackDistance);

            float attackDuration = 0.5f; // 공격 동작 시간
            float elapsedTime = 0f;

            while (elapsedTime < attackDuration)
            {
                // 선형보간 함수를 사용해 적을 이동시킴
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / attackDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 플레이어 공격
            Player player = playerObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // 플레이어 위치에서 다시 적 위치로 되돌아옴
            float returnDuration = 0.5f; // 돌아오는 동작 시간
            elapsedTime = 0f;

            while (elapsedTime < returnDuration)
            {
                // 선형보간 함수를 사용해 적을 이동시킴
                transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / returnDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
    public bool isContainedPosition(Vector2 position)
    {
        bool ret = false;
        Vector3 center = this.transform.position;
        float h = Block.COLLISION_SIZE / 2.0f;
        do {
            // X좌표가 자신에게 겹쳐있지 않다면 break로 루프를 빠져나온다.
            if(position.x < center.x - h || center.x + h < position.x) {
                break;
            }
            // Y좌표가 자신에게 겹쳐있지 않다면 break로 루프를 빠져나온다.
            if(position.y < center.y - h || center.y + h < position.y) {
                break;
            }
            // X좌표, Y좌표 양쪽이 겹쳐있다면 true를 반환한다.
            ret = true;
        } while(false);
        return(ret);
    }

    
}