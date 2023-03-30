using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    [SerializeField] UpgradeButtonScriptable _upgrade;
    private AudioSource audio;

    private void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void ShakeOnClick()
    {
        float rotation = Random.Range(_upgrade.minRotation, _upgrade.maxRotation);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        audio.Play();
    }
}
