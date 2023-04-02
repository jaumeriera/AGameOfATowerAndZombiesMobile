using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseInterstitialAd : MonoBehaviour
{
    IntertitalAdd ad;
    void Start()
    {
        ad = GameObject.FindGameObjectWithTag("InterstitialAdd").GetComponent<IntertitalAdd>();
        ad.ShowInterstitial();
    }

}
