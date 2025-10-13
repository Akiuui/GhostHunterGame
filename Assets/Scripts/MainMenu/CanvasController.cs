using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    private int ghostNoDefault = 5;
    public TextMeshProUGUI textNo5;
    public TextMeshProUGUI textNo10;
    public TextMeshProUGUI textNo15;
    public GameObject newBestPanel;

    private void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        PlayerPrefs.SetInt("ghostNo", ghostNoDefault);

        if (PlayerPrefs.HasKey("NewBest"))
        {
            
            if(PlayerPrefs.GetInt("NewBest") == 0)
            {
                newBestPanel.SetActive(false);
            }
            else
            {
                newBestPanel.SetActive(true);
                PlayerPrefs.SetInt("NewBest", 0);

            }
        }

        if (PlayerPrefs.HasKey("HighScore5"))
        {
            string highScore = PlayerPrefs.GetString("HighScore5");
            textNo5.text = highScore;
        }

        if (PlayerPrefs.HasKey("HighScore10"))
        {
            string highScore = PlayerPrefs.GetString("HighScore10");
            textNo10.text = highScore;
        
        }

        if (PlayerPrefs.HasKey("HighScore15"))
        {
            string highScore = PlayerPrefs.GetString("HighScore15");
            textNo15.text = highScore;

        }
    }
    public void StartGame() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    public void ExitGame()
    {
        print("Exiting the game");
        Application.Quit();
    }

    public void SelectedGhostNo(int selectedNo)
    {
        PlayerPrefs.SetInt("ghostNo", selectedNo);
        print(selectedNo);
    }

}

