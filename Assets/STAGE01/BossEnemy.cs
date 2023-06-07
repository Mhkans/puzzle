using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    // Start is called before the first frame update
   public override void Start()
   {
       MaxHp = 50;
       currentHp = MaxHp;
       status = Status.nomal;
   }

    // Update is called once per frame
    void Update()
    {
        handleHP();
        if (currentHp < 1)
        {
            Die();
        }
    }
    
    public override void Attack()
    {
       
    }
    
}
