using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealAvailable : MonoBehaviour
{
    [SerializeField]
    Button rewardVideo;

    private void Start() {
        if (PlayerPrefs.GetFloat("startHealthKey") == PlayerPrefs.GetFloat("HealthKey")) {
            rewardVideo.interactable = false;
        }
        else {
            rewardVideo.interactable = true;
        }
    }
}
