using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        MaxHp = 600;
        currentHp = MaxHp;
        term = 4.0f;
        damage = 10;
    }

    // Update is called once per frame
    public override void Attack()
    {
        StartCoroutine(BossAttackCoroutine());
    }
    IEnumerator BossAttackCoroutine()
    {
        
        Vector3 startPosition = transform.position;

       
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Vector3 endPosition = playerObject.transform.position;
            float attackDistance = 1.0f; 
            endPosition = Vector3.MoveTowards(startPosition, endPosition, attackDistance);

            float attackDuration = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < attackDuration)
            {
           
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

          
            float returnDuration = 1.0f; 
            elapsedTime = 0f;

            while (elapsedTime < returnDuration)
            {
                transform.position = Vector3.Lerp(endPosition, startPosition, elapsedTime / returnDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}
