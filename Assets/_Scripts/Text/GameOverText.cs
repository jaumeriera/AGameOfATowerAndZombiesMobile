using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    [SerializeField] Text loseText;
    
    void Start()
    {
        loseText.text = "Your tower was destroyed you survive to " + PlayerPrefs.GetInt("HordeLevel").ToString() + " hordes";
    }

}
