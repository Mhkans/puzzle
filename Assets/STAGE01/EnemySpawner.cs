using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject main_camera = null; // 메인 카메라.
    public Slider enemySliderPrefab;
    public static EnemySpawner Instance;
    public GameObject BenemyPrefab = null;
    public GameObject GenemyPrefab = null;
    public GameObject YenemyPrefab = null;
    public GameObject BbossPrefab = null;
    public GameObject GbossPrefab = null;
    public GameObject YbossPrefab = null;
    public int MaxEnemyNum = 3; // 만드는 ENEMY의 수, 임시값
    public Vector3 spawnPosition = Vector3.zero; // 스폰 위치
    public Enemy targetEnemy = null;
    public List<Enemy> enemies = new List<Enemy>();
    public List<Enemy.Status> StatusList = new List<Enemy.Status>();
    public Canvas canvas; // 새로운 Canvas
    public static int stagecode = 3;

    public void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // Canvas 찾기
        Instance = this;
        this.main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        switch (stagecode)
        {
            case 1:
                MaxEnemyNum = 1;
                break;
            case 2:
                MaxEnemyNum = 3;
                break;
            case 3:
                SpawnBoss();
                MaxEnemyNum = 2;
                break;
        }

        STAGECONTROL(stagecode);
       
    }

    public void Update()
    {


        clickEnemy();

        foreach (Enemy enemy in enemies)
        {
            if (enemy.enemyHP != null)
            {
                enemy.enemyHP.transform.position =
                    Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0));
            }
        }

    }
    Enemy.Status GetRandomStatus()
    {
        List<Enemy.Status> statuses = new List<Enemy.Status>() { Enemy.Status.Greenstat, Enemy.Status.Yellowstat, Enemy.Status.Bluestat };
        int randomIndex = Random.Range(0, statuses.Count);
        return statuses[randomIndex];
    }
    
    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    
    private void SpawnEnemy(int a)
    {
        if (GetEnemyCount() >= MaxEnemyNum)
        {
            return;
        }

        for (int i = 0; i < MaxEnemyNum; i++)
        {
            Vector3 offset = Vector3.zero;

            switch (a)
            {
                case 1:
                    offset = new Vector3(0, 5.5f, 0);
                    break;
                case 2:
                    offset = new Vector3((2.0f * i) - 2.0f, 5.5f, 0);
                    break;
                case 3:
                    offset = new Vector3((4.0f * i) - 2.0f, 5.5f, 0);
                    break;
            }

            Enemy.Status enemyStatus = GetRandomStatus();
            GameObject newEnemyObject = null;
            switch (enemyStatus)
            {
                case Enemy.Status.Bluestat:
                    newEnemyObject = Instantiate(BenemyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
                case Enemy.Status.Yellowstat:
                    newEnemyObject = Instantiate(YenemyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
                case Enemy.Status.Greenstat:
                    newEnemyObject = Instantiate(GenemyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
            }

            Enemy enemy = newEnemyObject.GetComponent<Enemy>();
            enemy.status = enemyStatus; // Assign the random status to the enemy
            enemies.Add(enemy);
            Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
            enemy.enemyHP = enemySlider;
        }
    }

    public void SpawnBoss()
    {
        
        Vector3 offset = new Vector3(0, 6.5f, 0);
        GameObject newBossObject = null;
        Enemy.Status enemyAttribute = GetRandomStatus();

        switch (enemyAttribute)
        {
            case Enemy.Status.Bluestat:
                newBossObject = Instantiate(BbossPrefab, spawnPosition + offset, Quaternion.identity);
                break;
            case Enemy.Status.Yellowstat:
                newBossObject = Instantiate(YbossPrefab, spawnPosition + offset, Quaternion.identity);
                break;
            case Enemy.Status.Greenstat:
                newBossObject = Instantiate(GbossPrefab, spawnPosition + offset, Quaternion.identity);
                break;
        }

        BossMonster bossMonster = newBossObject.GetComponent<BossMonster>();
        bossMonster.status = enemyAttribute;
        if (bossMonster != null && !enemies.Contains(bossMonster))
        {
            enemies.Add(bossMonster);
        }

        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        bossMonster.enemyHP = enemySlider;
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        enemies.Remove(enemy);
        MaxEnemyNum = 0;
        
    }

    public void clickEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition;
            // 마우스 클릭 위치를 월드 좌표로 변환합니다.
            if (unprojectMousePosition(out clickPosition, Input.mousePosition))
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy.isContainedPosition(clickPosition))
                    {

                        if (targetEnemy != null)
                        {
                            // 이전에 선택된 적의 크기 원상복귀
                            targetEnemy.transform.localScale /= 1.2f;
                        }

                        targetEnemy = enemy;
                        // 선택된 적의 크기를 20% 증가
                        targetEnemy.transform.localScale *= 1.2f;

                        enemies.Remove(enemy);
                        enemies.Insert(0, enemy);


                        break;
                    }
                }
            }
        }
    }

    public bool unprojectMousePosition(out Vector3 world_position, Vector3 mouse_position)
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
        if (plane.Raycast(ray, out depth))
        {
            // 인수 world_position을 마우스 위치로 덮어쓴다.
            world_position = ray.origin + ray.direction * depth;
            ret = true;
            // 닿지 않았다면.
        }
        else
        {
            // 인수 world_position을 제로인 벡터로 덮어쓴다.
            world_position = Vector3.zero;
            ret = false;
        }
        return (ret);
    }

    private void STAGECONTROL(int a) // 스테이지코드 스위치문
    {
        switch (a)
        {
            case 1:
                BossMonster.isdead = false;
                SpawnEnemy(1);
                break;
            case 2:
                BossMonster.isdead = false; 
                SpawnEnemy(2);
                break;
            case 3:
                SpawnEnemy(3);
                break;

        }
    }


}