using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HordeManager : MonoBehaviour
{
    [SerializeField] Text hordeLevel;
    [SerializeField] HordeScriptable _settings;
    [SerializeField] SpawnerManager spawner;

    public string bonusSpeedKey;
    public string bonusLifeKey;
    public string bonusValueKey;
    public string spawnTimeKey;
    public string quantityKey;
    public string hordeLevelKey;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(bonusSpeedKey) || !PlayerPrefs.HasKey(bonusLifeKey) || 
            !PlayerPrefs.HasKey(bonusValueKey) || !PlayerPrefs.HasKey(spawnTimeKey) ||
            !PlayerPrefs.HasKey(quantityKey) || !PlayerPrefs.HasKey(hordeLevelKey))
        {
            PlayerPrefs.SetFloat(bonusSpeedKey, _settings.startSpeed);
            PlayerPrefs.SetFloat(bonusLifeKey, _settings.startLife);
            PlayerPrefs.SetFloat(bonusValueKey, _settings.startValue);
            PlayerPrefs.SetFloat(spawnTimeKey, _settings.startSpawnTime);
            PlayerPrefs.SetFloat(quantityKey, _settings.startQuantity);
            PlayerPrefs.SetInt(hordeLevelKey, 0);
        }
        hordeLevel.text = PlayerPrefs.GetInt(hordeLevelKey).ToString();
    }

    private void Update()
    {
        if(GameManager.gameStatus == GameManager.Status.WaitingToStart)
        {
            spawner.StartHorde(
                PlayerPrefs.GetFloat(bonusSpeedKey),
                PlayerPrefs.GetFloat(bonusLifeKey),
                PlayerPrefs.GetFloat(bonusValueKey),
                PlayerPrefs.GetFloat(spawnTimeKey),
                Mathf.RoundToInt(PlayerPrefs.GetFloat(quantityKey))
            );
            updateBonusesForNextHorde();
            hordeLevel.text = PlayerPrefs.GetInt(hordeLevelKey).ToString();
        }
    }

    private void updateBonusesForNextHorde()
    {
        PlayerPrefs.SetFloat(bonusSpeedKey, PlayerPrefs.GetFloat(bonusSpeedKey) * _settings.bonusNextHordeSpeed);
        PlayerPrefs.SetFloat(bonusLifeKey, PlayerPrefs.GetFloat(bonusLifeKey) * _settings.bonusNextHordeLife);
        PlayerPrefs.SetFloat(bonusValueKey, PlayerPrefs.GetFloat(bonusValueKey) * _settings.bonusNextHordeValue);
        PlayerPrefs.SetFloat(quantityKey, _settings.bonusNextHordeQuantity * PlayerPrefs.GetFloat(quantityKey));
        if (PlayerPrefs.GetFloat(spawnTimeKey) > _settings.minSpawnTime)
        {
            PlayerPrefs.SetFloat(spawnTimeKey, PlayerPrefs.GetFloat(spawnTimeKey) - _settings.decrementSpawnTime);
        }
        PlayerPrefs.SetInt(hordeLevelKey, PlayerPrefs.GetInt(hordeLevelKey) + 1);
    }
}
