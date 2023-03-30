using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] FireScriptable _settings;
    public float baseDamage;
    public float burningTime;

    private void Start()
    {
        baseDamage = _settings.baseDamage;
        burningTime = _settings.burningTime;
    }
}
