using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Status { WaitingToStart, Spawning, SpawnFinished, GettingReward };

    public static Status gameStatus;

    [SerializeField] private MoneyManager money;
    [SerializeField] BaseUpgradeButton moneyUpgradeButton;
    [SerializeField] BaseUpgradeButton towerUpgradeButton;
    [SerializeField] EnemyQueue queue;

    private void Awake()
    {
        gameStatus = Status.WaitingToStart;
    }

    public void ShotFirst(float damage)
    {
        queue.doDamageToFirst(damage * towerUpgradeButton.GetCurrentBonus());
    }

    public void Die(int ammount)
    {
        money.AddMoney(Mathf.RoundToInt(ammount * moneyUpgradeButton.GetCurrentBonus()));
    }

    public static void GetReward()
    {
        SceneManager.LoadScene(2);
    }

}
