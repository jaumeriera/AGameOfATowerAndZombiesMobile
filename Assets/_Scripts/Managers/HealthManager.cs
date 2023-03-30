using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private string healthKey;
    private float maxHealth;
    public float currentHealth;

    [SerializeField] GameObject lifeBar;

    public delegate void NoHealthAction();
    public event NoHealthAction NoHealth;

    public static string mustHealPlayerKey = "MustHealPlayer";


    public void SetUp(float health, string key=null, bool firstIni=true)
    {
        float mh = health;
        if (key != null)
        {
            if (firstIni)
            {
                PlayerPrefs.SetFloat("start" + key, health);
            }
            else
            {
                mh = PlayerPrefs.GetFloat("start" + key);
            }
            if (PlayerPrefs.GetInt(mustHealPlayerKey) == 1)
            {
                Heal();
                PlayerPrefs.SetInt(mustHealPlayerKey, 0);
            } else
            {
                healthKey = key;
                PlayerPrefs.SetFloat(key, health);
            }
        }
        maxHealth = mh;
        currentHealth = health;
        lifeBar.transform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        lifeBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        if (currentHealth == 0 && NoHealth != null)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal()
    {
        currentHealth = maxHealth;
        if(healthKey != null)
        {
            PlayerPrefs.SetFloat(healthKey, maxHealth);
        }
    }

    public bool CanHeal()
    {
        return currentHealth < maxHealth;
    }
}
