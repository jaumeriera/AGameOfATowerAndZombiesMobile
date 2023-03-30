using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : MonoBehaviour
{
    public void HealPlayer()
    {
        PlayerPrefs.SetInt(HealthManager.mustHealPlayerKey, 1);
    }
}
