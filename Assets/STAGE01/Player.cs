using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static float MaxHp;
    public static float currentHp;
    [SerializeField] private Slider hpbar;
    public static float attCoefficient = 1.0f; // 공격력 계수
    public static float healCoefficient = 1.0f; // 회복력 계수
    public static float hpCoefficient = 1.0f; //체력 계수
    public static int shield = 0;
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
        
    }

    public void handleHP()
    {
        hpbar.value = (float)currentHp / (float)MaxHp;

    }

    public void TakeDamage(int damage)
    {
        
        currentHp = currentHp - (damage - shield);
    
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
        attCoefficient = 1.0f;
        healCoefficient = 1.0f;
        hpCoefficient = 1.0f;
        shield = 0;
        Reward.ismachinegun = false;
        Reward.isadditionalblow = false;
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

