using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseUpgradeButtonScriptable", menuName = "Scriptables/BaseUpgradeButtonScriptable", order = 2)]
public class BaseUpgradeButtonScriptable : ScriptableObject
{
    public int startCost;

    public int startLevel;

    public float startBonus;

    public float costIncrement;

    public float bonusIncrement;

}