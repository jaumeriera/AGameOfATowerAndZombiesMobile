using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private string moneyKey = "money";
    [SerializeField] Text moneyText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(moneyKey))
        {
            PlayerPrefs.SetInt(moneyKey, 0);
        }

        moneyText.text = PlayerPrefs.GetInt(moneyKey).ToString();
    }

    public void AddMoney(int ammount)
    {
        PlayerPrefs.SetInt(moneyKey, PlayerPrefs.GetInt(moneyKey) + ammount);
        UpdateMoneyText();
    }

    public void MinusMoney(int ammount)
    {
        PlayerPrefs.SetInt(moneyKey, PlayerPrefs.GetInt(moneyKey) - ammount);
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = PlayerPrefs.GetInt(moneyKey).ToString();
    }

    public bool HaveEnoughMoney(int cost)
    {
        return cost <= PlayerPrefs.GetInt(moneyKey);
    }
}
