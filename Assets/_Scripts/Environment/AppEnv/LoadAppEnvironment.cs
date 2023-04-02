using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAppEnvironment : MonoBehaviour
{
    [SerializeField]
    TextAsset jsonFile;
    public AppEnvironment LoadAppEnvironmentFromJson() {
        return JsonUtility.FromJson<AppEnvironment>(jsonFile.text);
    }
}
