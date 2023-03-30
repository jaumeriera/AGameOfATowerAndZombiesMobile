using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUpgradeSpawnableButton : BaseUpgradeButton
{
    [SerializeField] BaseUpgradeButtonSpawnableScriptable _extendedSettings;
    public string currentItemsKey;
    public GameObject[] items;


    public override void Start()
    {
        base.Start();
        if (!PlayerPrefs.HasKey(currentItemsKey))
        {
            PlayerPrefs.SetInt(currentItemsKey, _extendedSettings.startItems);
        }
        if (!isFree)
        {
            for (int i = 0; i < PlayerPrefs.GetInt(currentItemsKey); i++)
            {
                items[i].SetActive(true);
            }

        }
    }

    protected override void UpgradeBonus()
    {
        int currentCost;

        if (_extendedSettings.maxAtTime > PlayerPrefs.GetInt(currentItemsKey) && PlayerPrefs.GetInt(currentLevelKey) % _extendedSettings.spawnEvery == 0)
        {
            SpawnNewItem();
            PlayerPrefs.SetInt(currentItemsKey, PlayerPrefs.GetInt(currentItemsKey) + 1);
        }
        else
        {
            PlayerPrefs.SetFloat(currentBonusKey, PlayerPrefs.GetFloat(currentBonusKey) * _settings.bonusIncrement);
        }
        PlayerPrefs.SetInt(currentLevelKey, PlayerPrefs.GetInt(currentLevelKey) + 1);
        if (!isFree)
        {
            money.MinusMoney(PlayerPrefs.GetInt(currentCostKey));
        }
        currentCost = Mathf.RoundToInt(_settings.costIncrement * PlayerPrefs.GetInt(currentCostKey));
        PlayerPrefs.SetInt(currentCostKey, currentCost);
        costText.text = currentCost.ToString();
        levelText.text = (PlayerPrefs.GetInt(currentLevelKey)).ToString();

    }

    private void SpawnNewItem()
    {
        if (!isFree)
        {
            items[PlayerPrefs.GetInt(currentItemsKey)].gameObject.SetActive(true);
        }
    }
}
