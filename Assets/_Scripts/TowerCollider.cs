using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class TowerCollider : MonoBehaviour
{
    [SerializeField] TowerHealthScriptable _settings;
    private HealthManager healthManager;

    public string healthKey;
    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(healthKey))
        {
            InitHealth(PlayerPrefs.GetFloat(healthKey), healthKey, false);
        }else
        {
            InitHealth(_settings.health, healthKey, true);
        }
    }

    private void InitHealth(float health, string healthKey, bool firstIni)
    {
        healthManager.SetUp(health, healthKey, firstIni);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        UIManager.GameOver();
    }

}
