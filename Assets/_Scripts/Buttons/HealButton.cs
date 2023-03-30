using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : MonoBehaviour
{
    public void HealPlayer()
    {
        PlayerPrefs.SetInt("MustHealPlayer", 1);
        int foo = PlayerPrefs.GetInt("MustHealPlayer");
    }
}
