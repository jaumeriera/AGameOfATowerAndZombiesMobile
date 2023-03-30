using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    // Pool list build based on provided GameObjects lists of spawners
    ObjectPool EnemyPool;
    [SerializeField] EnemyQueue queue;

    private float hordeBonusSpeed;
    private float hordeBonusLife;
    private float hordeBonusValue;
    private float hordeSpawnTime;
    private int hordeQuantity;
    private int spawnedEnemies;

    private Coroutine reference;
    void Awake()
    {
        EnemyPool = GetComponent<ObjectPool>();
    }

    public void StartHorde(float bonusSpeed, float bonusLife, float bonusValue, float spawnTime, int quantity)
    {
        if (reference != null) { return; }
        if(GameManager.gameStatus != GameManager.Status.WaitingToStart) { return; }
        hordeBonusSpeed = bonusSpeed;
        hordeBonusLife = bonusLife;
        hordeBonusValue = bonusValue;
        hordeSpawnTime = spawnTime;
        hordeQuantity = quantity;
        spawnedEnemies = 0;
        reference = StartCoroutine(SpawnHordeCoroutine());
    }

    private IEnumerator SpawnHordeCoroutine()
    {
        GameManager.gameStatus = GameManager.Status.Spawning;
        while (spawnedEnemies < hordeQuantity)
        {
            SpawnElement();
            spawnedEnemies += 1;
            yield return new WaitForSeconds(hordeSpawnTime);
        }
        reference = null;
        GameManager.gameStatus = GameManager.Status.SpawnFinished;
    }

    private void SpawnElement()
    {
        BaseEnemy element = (BaseEnemy)EnemyPool.GetNext();
        element.transform.position = this.transform.position;
        element.GetComponent<BaseEnemy>().InitEnemy(hordeBonusLife, hordeBonusSpeed, hordeBonusValue);
        element.gameObject.SetActive(true);
        queue.AddEnemy(element);
    }
}
