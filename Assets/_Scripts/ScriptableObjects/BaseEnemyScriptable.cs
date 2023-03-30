using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemyScriptable", menuName = "Scriptables/BaseEnemyScriptable", order = 4)]
public class BaseEnemyScriptable : ScriptableObject
{
    public int baseValue;

    public float baseSpeed;

    public float baseHealth;

    public float attackCooldown;

    public float enemyDamage;
}
