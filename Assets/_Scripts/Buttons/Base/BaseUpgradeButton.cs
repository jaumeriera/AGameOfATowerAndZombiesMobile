using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgradeButton : MonoBehaviour
{
    [SerializeField] protected Text costText;
    [SerializeField] protected Text levelText;
    [SerializeField] protected MoneyManager money;
    [SerializeField] protected BaseUpgradeButtonScriptable _settings;

    public string currentCostKey;
    public string currentLevelKey;
    public string currentBonusKey;

    public bool isFree;

    protected bool clickable = false;

    public virtual void Start()
    {
        // if keys are not generated create from settings, else take player prefs values
        if(!PlayerPrefs.HasKey(currentCostKey) || !PlayerPrefs.HasKey(currentLevelKey) || !PlayerPrefs.HasKey(currentBonusKey))
        {
            PlayerPrefs.SetInt(currentCostKey, _settings.startCost);
            PlayerPrefs.SetInt(currentLevelKey, _settings.startLevel);
            PlayerPrefs.SetFloat(currentBonusKey, _settings.startBonus);
        }
        if (isFree)
        {
            costText.text = "Free";
        } else
        {
            costText.text = PlayerPrefs.GetInt(currentCostKey).ToString();
        }
        levelText.text = (PlayerPrefs.GetInt(currentLevelKey)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        clickable = isFree || money.HaveEnoughMoney(PlayerPrefs.GetInt(currentCostKey));
        // TODO change sprite if not active
    }

    public virtual void Ugrade()
    {
        if (!clickable)
        {
            return;
        }
        UpgradeBonus();
    }

    protected virtual void UpgradeBonus()
    {
        int currentCost;
        PlayerPrefs.SetFloat(currentBonusKey, PlayerPrefs.GetFloat(currentBonusKey) * _settings.bonusIncrement);
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

    public float GetCurrentBonus()
    {
        return PlayerPrefs.GetFloat(currentBonusKey);
    }


}
