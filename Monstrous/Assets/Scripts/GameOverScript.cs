using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("InGame");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
