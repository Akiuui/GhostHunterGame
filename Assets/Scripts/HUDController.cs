using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    //UI elements
    public Slider slider;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI ghostCounterTxt;
    public GameObject pausePanel;

    //Scripts
    public Weapon weaponScript;
    private GameManager gameManagerScript;
    public LaserCollision LaserCollisionScript;

    private bool isPaused = false;

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        slider.maxValue = weaponScript.chargeMax;
        ghostCounterTxt.text = $"0/{gameManagerScript.GetGhostNo()}";

        weaponScript.OnChargeChanged += UpdateSliderUI;
        gameManagerScript.OnTimerUpdated += UpdateTimerText;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Esc clicekd");
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);

        }
    }
    private void UpdateSliderUI(float newCharge)
    {
        print("Slider updated");
        slider.value = newCharge;
    }

    public void UpdateGhostCounterUI()
    {
        print("Ghost counter event fired");

        ghostCounterTxt.text = $"{gameManagerScript.ghostHit}/{gameManagerScript.GetGhostNo()}";
    }
    private void UpdateTimerText(string formattedTimer)
    {
        timerTxt.text = formattedTimer;
    }

    public void TogglePause() {
        print("Clicked");
        isPaused = !isPaused;

        if (isPaused)
        {
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


    }
    public void ExitRound()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

}
