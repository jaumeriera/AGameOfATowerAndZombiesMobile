using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseUpgradeButtonSpawnableScriptable", menuName = "Scriptables/BaseUpgradeButtonSpawnableScriptable", order = 3)]
public class BaseUpgradeButtonSpawnableScriptable : BaseUpgradeButtonScriptable
{
    public int maxAtTime;

    public int spawnEvery;

    public int startItems;
}
