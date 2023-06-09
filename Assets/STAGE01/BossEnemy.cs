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
       
   }

    // Update is called once per frame
    public void Update()
    {
        handleHP();
        handleText();
        if (currentHp < 1)
        {
            Die();
        }
    }
    
    public override void Attack()
    {
       
    }
    
}
