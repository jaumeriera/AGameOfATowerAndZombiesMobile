using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQueue : MonoBehaviour
{
    private Queue<BaseEnemy> enemyQueue;

    private void Start()
    {
        enemyQueue = new Queue<BaseEnemy>();
    }

    public void AddEnemy(BaseEnemy enemy)
    {
        enemyQueue.Enqueue(enemy);
    }

    public void doDamageToFirst(float damage)
    {
        // check if set active is false what does return the queue
        if (QueueEmpty()) { return; } //Todo Check
        BaseEnemy enemy = enemyQueue.Peek();
        // Find for some enemy, maybe some enemies die due fire so check for real first
        // enemy in queue
        while(enemy != null && enemy.gameObject.active == false)
        {
            enemyQueue.Dequeue();
            if (!QueueEmpty())
            {
                enemy = enemyQueue.Peek();
            } else
            {
                enemy = null;
            }
        }
        
        // if we dont found any enemy in the queue skip
        if (enemy==null || enemy.gameObject.active == false) { return; }

        enemy.GetComponent<BaseEnemy>().TakeDamage(damage);
        if (enemy.gameObject.active == false)
        {
            enemyQueue.Dequeue();
        }
    }

    public bool QueueEmpty()
    {
        return enemyQueue.Count == 0;
    }

    private void Update()
    {

        if (GameManager.gameStatus == GameManager.Status.SpawnFinished)
        {
            if (!QueueEmpty())
            {
                BaseEnemy enemy = enemyQueue.Peek();
                if (enemy.gameObject.active == false)
                {
                    enemyQueue.Dequeue();
                }
            } else
            {
                GameManager.gameStatus = GameManager.Status.GettingReward;
                GameManager.GetReward();
            }
        }
    }


}
