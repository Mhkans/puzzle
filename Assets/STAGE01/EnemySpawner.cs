using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip stageSound01;
    public AudioClip stageSound02;
    public AudioClip stageSound03;
    private GameObject main_camera = null; // 메인 카메라.
    public Slider enemySliderPrefab;
    public Text enemyText;
    public static EnemySpawner Instance;
    public GameObject BenemyPrefab = null;
    public GameObject GenemyPrefab = null;
    public GameObject YenemyPrefab = null;
    public GameObject BheavyPrefab = null;
    public GameObject GheavyPrefab = null;
    public GameObject YheavyPrefab = null;
    public GameObject BMiniPrefab = null;
    public GameObject GMiniPrefab = null;
    public GameObject YMiniPrefab = null;
    public GameObject BbossPrefab = null;
    public GameObject GbossPrefab = null;
    public GameObject YbossPrefab = null;
    public GameObject BossEnemy = null;
    public GameObject background0102 = null;
    public GameObject background03 = null;
    public GameObject background0405 = null;
    public GameObject background06 = null;
    private GameObject background;
    public int MaxEnemyNum; // 만드는 ENEMY의 수, 임시값
    public Vector3 spawnPosition = Vector3.zero; // 스폰 위치
    public static Enemy targetEnemy = null;
    public List<Enemy> enemies = new List<Enemy>();
    public List<Enemy> SummonedEnemy = new List<Enemy>();
    public static bool stage01clear = false; 
    public static bool stage02clear = false;
    public static bool stage03clear = false; 
    public static bool stage04clear = false;
    public static bool stage05clear = false; 
    public Canvas canvas; // 새로운 Canvas
    public static int stagecode = 3;
    private float elapsedTime = 0f; 
    public static int spawnInterval; 
    private int enemyCount = 0; 
    public bool canSpawn = false; 
    public void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
        spawnInterval = 10;
        canSpawn = true;
        canvas = FindObjectOfType<Canvas>(); // Canvas 찾기
        Instance = this;
        this.main_camera = GameObject.FindGameObjectWithTag("MainCamera");
        switch (stagecode)
        {
            case 1:
                background = Instantiate(background0102, spawnPosition + new Vector3(0,0,5.0f), Quaternion.identity);
                audio.clip = stageSound01;
                audio.Play();
                MaxEnemyNum = 1;
                break;
            case 2:
                background = Instantiate(background0102, spawnPosition + new Vector3(0,0,5.0f), Quaternion.identity);
                audio.clip = stageSound01;
                audio.Play();
                MaxEnemyNum = 2;
                break;
            case 3:
                background = Instantiate(background03, spawnPosition + new Vector3(0,0,5.0f), Quaternion.identity);
                audio.clip = stageSound02;
                audio.Play();
                SpawnMiniBoss();
                BossSpawnEnemy();
                BossSpawnEnemy();
                MaxEnemyNum = 0;
                break;
            case 4:
                background = Instantiate(background0405, spawnPosition + new Vector3(0,0,3.0f), Quaternion.identity);
                audio.clip = stageSound03;
                audio.loop = true;
                audio.Play();
                MaxEnemyNum = 2;
                break;
            case 5:
                background = Instantiate(background0405, spawnPosition + new Vector3(0,0,3.0f), Quaternion.identity);
                audio.clip = stageSound03;
                audio.loop = true;
                audio.Play();
                MaxEnemyNum = 3;
                break;
            case 6:
                background = Instantiate(background06, spawnPosition + new Vector3(0,0,5.0f), Quaternion.identity);
                audio.clip = stageSound02;
                audio.Play();
                SpawnBoss();
                BossSpawnEnemy();
                BossSpawnEnemy();
                MaxEnemyNum = 0;
                break;
        }

        STAGECONTROL(stagecode);

        enemies[0].transform.localScale *= 1.4f;

    }

    public void Update()
    {
        if (stagecode == 6)
        {
            elapsedTime += Time.deltaTime;
            if (canSpawn && enemyCount < 2 && elapsedTime >= spawnInterval)
            {
                BossSpawnEnemy();
                elapsedTime = 0f; // 경과 시간 초기화
            }


        }


        if (Instance.SummonedEnemy.Count > 0)
        {
            targetEnemy = SummonedEnemy[0];
            if (targetEnemy.currentHp <= 0)
            {
                if (Instance.SummonedEnemy.Count > 1)
                {
                    targetEnemy = SummonedEnemy[1];
                }
            }
        }
        else
        {
            targetEnemy = enemies[0];
            if (targetEnemy.currentHp <= 0)
            {
                if (Instance.enemies.Count > 1)
                {
                    targetEnemy = enemies[1];
                }
            }
        }

        switch (stagecode)
        {
            case 1:
                clickEnemy();
                break;
            case 2:
                clickEnemy();   
                break;
            case 4:
                clickEnemy();
                break;
            case 5:
                clickEnemy();
                break;
        }
        

        foreach (Enemy enemy in enemies)
        {
            if (enemy.enemyHP != null)
            {
                enemy.enemyHP.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0));
                enemy.enemyTEXT.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0.3f, 1.0f, 0));
            }
        }

        foreach (BossEnemy enemy in SummonedEnemy)
        {
            if (enemy.enemyHP != null)
            {
                enemy.enemyHP.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0, 1, 0));
                enemy.enemyTEXT.transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + new Vector3(0.3f, 1.0f, 0));

            }

            if (SummonedEnemy.IndexOf(enemy) == 0)
            {
                enemy.transform.position = new Vector3(-2.0f, 4.5f, 0);
            }

            if (SummonedEnemy.IndexOf(enemy) == 1)
            {
                enemy.transform.position = new Vector3(2.0f, 4.5f, 0);
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
    
    
    private void SpawnEnemy0102(int a)
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
                    offset = new Vector3((3.0f * i) - 1.5f, 5.5f, 0);
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
            Text enemytext = Instantiate(enemyText, canvas.transform);
            enemySlider.transform.SetSiblingIndex(1);
            enemytext.transform.SetSiblingIndex(2);
            enemy.enemyHP = enemySlider;
            enemy.enemyTEXT = enemytext;
        }
    }
    private void SpawnEnemy0405(int a)
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
                case 4:
                    offset = new Vector3((4.0f * i) - 2.0f, 5.5f, 0);
                    break;
                case 5:
                    offset = new Vector3((2.0f * i) - 2.0f, 5.5f, 0);
                    break;
            }
            HeavyEnemy.Status enemyStatus = GetRandomStatus();
            GameObject newEnemyObject = null;
            switch (enemyStatus)
            {
                case HeavyEnemy.Status.Bluestat:
                    newEnemyObject = Instantiate(BheavyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
                case HeavyEnemy.Status.Yellowstat:
                    newEnemyObject = Instantiate(YheavyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
                case HeavyEnemy.Status.Greenstat:
                    newEnemyObject = Instantiate(GheavyPrefab, spawnPosition + offset, Quaternion.identity);
                    break;
            }

            HeavyEnemy enemy = newEnemyObject.GetComponent<HeavyEnemy>();
            enemy.status = enemyStatus; // Assign the random status to the enemy
            enemies.Add(enemy);
            Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
            Text enemytext = Instantiate(enemyText, canvas.transform);
            enemySlider.transform.SetSiblingIndex(1);
            enemytext.transform.SetSiblingIndex(2);
            enemy.enemyHP = enemySlider;
            enemy.enemyTEXT = enemytext;
            
        }
    }

    private void BossSpawnEnemy()
    {
        Vector3 offset = new Vector3(0, 4.5f, 0);

       
        GameObject newEnemyObject = null;
        newEnemyObject = Instantiate(BossEnemy, spawnPosition + offset, Quaternion.identity);
        BossEnemy enemy = newEnemyObject.GetComponent<BossEnemy>();
        enemy.status = enemies[0].status;
        SummonedEnemy.Add(enemy);
        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        Text enemytext = Instantiate(enemyText, canvas.transform);
        enemySlider.transform.SetSiblingIndex(1);
        enemytext.transform.SetSiblingIndex(2);
        enemy.enemyHP = enemySlider;
        enemy.enemyTEXT = enemytext;

        enemyCount++; // 소환된 적 수 증가

        if (enemyCount >= 2) // 적이 2마리 이상 소환되었을 경우 소환 중지
        {
            canSpawn = false;
        }
    }
    public void SpawnMiniBoss()
    {
        Vector3 offset = new Vector3(0, 5.8f, 0);
        GameObject newBossObject = null;
        Enemy.Status enemyAttribute = GetRandomStatus();

        switch (enemyAttribute)
        {
            case Enemy.Status.Bluestat:
                newBossObject = Instantiate(BMiniPrefab, spawnPosition + offset, Quaternion.identity);
                break;
            case Enemy.Status.Yellowstat:
                newBossObject = Instantiate(YMiniPrefab, spawnPosition + offset, Quaternion.identity);
                break;
            case Enemy.Status.Greenstat:
                newBossObject = Instantiate(GMiniPrefab, spawnPosition + offset, Quaternion.identity);
                break;
        }

        MiniBoss bossMonster = newBossObject.GetComponent<MiniBoss>();
        bossMonster.status = enemyAttribute;
        if (bossMonster != null && !enemies.Contains(bossMonster))
        {
            enemies.Add(bossMonster);
        }

        Slider enemySlider = Instantiate(enemySliderPrefab, canvas.transform);
        enemySlider.transform.SetSiblingIndex(1);
        bossMonster.enemyHP = enemySlider;
        Text enemytext = Instantiate(enemyText, canvas.transform);
        enemytext.transform.SetSiblingIndex(2);
        bossMonster.enemyTEXT = enemytext;
    }
    public void SpawnBoss()
    {
        Vector3 offset = new Vector3(0, 6.3f, 0.5f);
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
        enemySlider.transform.SetSiblingIndex(1);
        bossMonster.enemyHP = enemySlider;
        Text enemytext = Instantiate(enemyText, canvas.transform);
        enemytext.transform.SetSiblingIndex(2);
        bossMonster.enemyTEXT = enemytext;
        
    }

    public void EnemyDestroyed(Enemy enemy)
    {
        enemies.Remove(enemy);
        MaxEnemyNum = 0;
        
    }

    public void BossEnemyDestored(Enemy enemy)
    {
        SummonedEnemy.Remove(enemy);
        enemyCount--;
        if (enemyCount < 2)
        {
            canSpawn = true;
        }
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
                            targetEnemy.transform.localScale /= 1.4f;
                        }
                       

                        targetEnemy = enemy;
                        // 선택된 적의 크기를 20% 증가
                        targetEnemy.transform.localScale *= 1.4f;
                        
                        enemies.Remove(enemy);
                        enemies.Insert(0, enemy);
                        if (targetEnemy != null)
                        
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
                SpawnEnemy0102(a);
                break;
            case 2:
                BossMonster.isdead = false; 
                SpawnEnemy0102(a);
                break;
            case 3:
                BossMonster.isdead = false; 
                break;
            case 4:
                BossMonster.isdead = false; 
                SpawnEnemy0405(a);
                break;
            case 5:
                BossMonster.isdead = false; 
                SpawnEnemy0405(a);
                break;
            case 6:
                BossMonster.isdead = false;
                break;

        }
    }
    


}