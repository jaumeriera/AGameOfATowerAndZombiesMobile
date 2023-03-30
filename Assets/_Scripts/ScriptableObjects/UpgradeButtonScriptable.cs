using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeButtonScriptable", menuName = "Scriptables/UpgradeButtonScriptable", order = 1)]
public class UpgradeButtonScriptable : ScriptableObject
{
    [Range(-360, 360)]
    public float maxRotation;

    [Range(-360, 360)]
    public float minRotation;
}
