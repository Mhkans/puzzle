using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHp;
    public int currentHp;
    [SerializeField] private Slider hpbar;
    public static int attCoefficient = 1; // 공격력 계수
    public static int healCoefficient = 1; // 회복력 계수
    public static int hpCoefficient = 1; //체력 계수
    private bool isDead = false; 

    void Start()
    {
        MaxHp = 100 * hpCoefficient;
        currentHp = MaxHp;
        hpbar.value = (float)currentHp / (float)MaxHp;

    }

    // Update is called once per frame
    void Update()
    {
        handleHP();
        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHp -= 10;
        }
    }

    public void handleHP()
    {
        hpbar.value = (float)currentHp / (float)MaxHp;

    }

    public void TakeDamage(int damage)
    {
        
        currentHp -= damage * attCoefficient;
    
        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }
    

    public bool IsDead()
    {
        return currentHp <= 0;
    }
    private void Die()
    {
        isDead = true;

    }

    public void Heal(int count)
    {
        // 체력을 증가시킨다.
        currentHp += count * healCoefficient;

        // 체력이 최대 체력을 넘지 않도록 조정한다.
        if (currentHp > MaxHp)
        {
            currentHp = MaxHp;
        }
        
    }
}

