using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        MaxHp = 300;
        currentHp = MaxHp;
        term = 9.5f;
        damage = 1;
    }

    // Update is called once per frame
    
        
}
