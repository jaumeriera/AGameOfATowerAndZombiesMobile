using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireScriptable", menuName = "Scriptables/FireScriptable", order = 7)]
public class FireScriptable : ScriptableObject
{
    public float baseDamage;
    public float burningTime;
}
