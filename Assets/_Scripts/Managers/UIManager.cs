using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 0 - MainMenu
    // 1 - GamePlay
    // 2 - RewardScene
    // 3 - HowToPlay
    // 4 - GammeOver
    // 5 - Upgrades

    public void PlayGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void NewHorde()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(3);
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(4);
    }

    public static void Upgrades()
    {
        SceneManager.LoadScene(5);
    }
}
