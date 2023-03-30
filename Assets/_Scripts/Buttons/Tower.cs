using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] float baseDamage;

    public void DoDamanage()
    {
        gm.ShotFirst(baseDamage);
    }
}
