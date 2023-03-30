using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HordeScriptable", menuName = "Scriptables/HordeScriptable", order = 5)]
public class HordeScriptable : ScriptableObject
{
    public float bonusNextHordeSpeed;
    public float bonusNextHordeLife;
    public float bonusNextHordeValue;
    public float bonusNextHordeQuantity;
    public float decrementSpawnTime;
    public float minSpawnTime;

    public float startSpawnTime;
    public float startSpeed;
    public float startLife;
    public float startValue;
    public int startQuantity;
}
