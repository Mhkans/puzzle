using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject main_camera = null; // 메인 카메라.
    public BossMonster boss;
    public Slider enemySliderPrefab;
    public static EnemySpawner Instance;
    public GameObject enemyPrefab = null;
    public GameObject bossPrefab = null;
    public static int MaxEnemyNum = 2; // 만드는 ENEMY의 수, 임시값
    public Vector3 spawnPosition = Vector3.zero; // 스폰 위치
    public Enemy targetEnemy = null;
    public List<Enemy> enemies = new List<Enemy>();
    public Canvas canvas; // 새로운 Canvas
    public GameObject blueStatusPrefab = null;
    public GameObject greenStatusPrefab = null;
    public GameObject yellowStatusPrefab = null;
    private List<GameObject> statusPrefabs = new List<GameObject>();
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
                MaxEnemyNum = 3;
                break;
        }


    }

    public void Update()
    {
        STAGECONTROL(stagecode);
        clickEnemy();

        foreach (Enemy enemy in enemies)
        {
            if (enemy.enemyHP != null)
            {
                enemy.enemyHP.transform.position =
                    Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0));
            }

            if (enemy.status == Enemy.Status.Bluestat)
            {
                // blueStatusPrefab 생성
                GameObject blueStatus = Instantiate(blueStatusPrefab,
                    new Vector3(enemy.transform.position.x - 1.0f, enemy.transform.position.y,
                        enemy.transform.position.z), Quaternion.identity);

                // 에너미의 자식 오브젝트로 추가
                blueStatus.transform.parent = enemy.transform;

                // 생성된 속성프리팹을 리스트에 추가
                statusPrefabs.Add(blueStatus);
            }
            else if (enemy.status == Enemy.Status.Yellowstat)
            {
                // yellowStatusPrefab 생성
                GameObject yellowStatus = Instantiate(yellowStatusPrefab,
                    new Vector3(enemy.transform.position.x - 1.0f, enemy.transform.position.y,
                        enemy.transform.position.z), Quaternion.identity);

                // 에너미의 자식 오브젝트로 추가
                yellowStatus.transform.parent = enemy.transform;

                // 생성된 속성프리팹을 리스트에 추가
                statusPrefabs.Add(yellowStatus);
            }
            else if (enemy.status == Enemy.Status.Greenstat)
            {
                // greenStatusPrefab 생성
                GameObject greenStatus = Instantiate(greenStatusPrefab,
                    new Vector3(enemy.transform.position.x - 1.0f, enemy.transform.position.y,
                        enemy.transform.position.z), Quaternion.identity);

                // 에너미의 자식 오브젝트로 추가
                greenStatus.transform.parent = enemy.transform;

                // 생성된 속성프리팹을 리스트에 추가
                statusPrefabs.Add(greenStatus);
            }
        }

        for (int i = 10; i < statusPrefabs.Count; i++)
        {
            Destroy(statusPrefabs[i]);
        }
        Destroy(statusPrefabs[0]);

    }



    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    public void SpawnEnemy1()
    {
        if (GetEnemyCount() >= MaxEnemyNum) // * 보스전일때는 인원수 조정
        {
            return;
        }


        Vector3 offset = new Vector3(0, 5.5f, 0);
        GameObject newEnemyObject = Instantiate(enemyPrefab, spawnPosition + offset, Quaternion.identity);
        Enemy enemy = newEnemyObject.GetComponent<Enemy>();
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
            
        }
    
        
        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        enemy.enemyHP = enemySlider;

        enemySlider.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1.0f, 0));



    }
    public void SpawnEnemy2()
    {
        if (GetEnemyCount() >= MaxEnemyNum) // * 보스전일때는 인원수 조정
        {
            return;
        }


        Vector3 offset = new Vector3(2.0f * GetEnemyCount() - 2.0f, 5.5f, 0);
        GameObject newEnemyObject = Instantiate(enemyPrefab, spawnPosition + offset, Quaternion.identity);
        Enemy enemy = newEnemyObject.GetComponent<Enemy>();
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }

        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        enemy.enemyHP = enemySlider;

        enemySlider.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1.0f, 0));



    }
    public void SpawnEnemy3()
    {
        if (GetEnemyCount() >= MaxEnemyNum) // * 보스전일때는 인원수 조정
        {
            return;
        }


        Vector3 offset = new Vector3(4.0f * GetEnemyCount() - 6.0f, 5.5f, 0);
        GameObject newEnemyObject = Instantiate(enemyPrefab, spawnPosition + offset, Quaternion.identity);
        Enemy enemy = newEnemyObject.GetComponent<Enemy>();
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }

        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        enemy.enemyHP = enemySlider;

        enemySlider.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1.0f, 0));



    }
    public void SpawnBoss()
    {
        BossMonster.isdead = false;

        Vector3 offset = new Vector3(0, 6.5f, 0);
        GameObject newbossObject = Instantiate(bossPrefab, spawnPosition + offset, Quaternion.identity);
        BossMonster bossMonster = newbossObject.GetComponent<BossMonster>();
        enemies.Add(bossMonster);
        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        bossMonster.enemyHP = enemySlider;

        enemySlider.transform.position = Camera.main.WorldToScreenPoint(bossMonster.transform.position + new Vector3(0, 1.0f, 0));

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

    private void STAGECONTROL(int stagecode) // 스테이지코드 스위치문
    {
        switch (stagecode)
        {
            case 1:
                BossMonster.isdead = false;
                SpawnEnemy1();
                break;
            case 2:
                BossMonster.isdead = false; 
                SpawnEnemy2();
                break;
            case 3:
                SpawnEnemy3();
                break;

        }
    }


}