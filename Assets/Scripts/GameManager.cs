using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] ghostPrefabs;
    public Transform[] ghostSpawns;

    private int ghostNo = 5;
    public int ghostHit = 0;

    private Transform ghostOrganizer;

    public event Action<string> OnTimerUpdated;


    private float timerUpdateRate = 1f;
    private float timeSinceLastTimerUpdate = 0f;
    private float currentTime = 0f;
    private bool stopTimer = false;

    public AudioClip finishedRoundAudio;
    private AudioSource audioSource;
    void Start()
    {
        ghostOrganizer = GameObject.FindGameObjectWithTag("GhostContainer").transform;
        audioSource = GetComponent<AudioSource>();

        SpawnGhosts();

    }
    private void Update()
    {
        UpdateTimer();
    }
    private void UpdateTimer() {

        if (stopTimer)
            return;

        timeSinceLastTimerUpdate += Time.deltaTime;
        if (timeSinceLastTimerUpdate >= timerUpdateRate)
        {
            currentTime += timeSinceLastTimerUpdate;
            timeSinceLastTimerUpdate = 0f;

            OnTimerUpdated?.Invoke(FormatTimer(currentTime));
        }

    }

    public void ghostFired()
    {

        ghostHit++;

        if (ghostHit == ghostNo)
        {
            StartCoroutine(EndRound());
        }

    }

    IEnumerator EndRound()
    {
        print("Finished game");

        stopTimer = true;

        if (PlayerPrefs.HasKey("HighScore" + ghostNo))
        {
            string lastBestTime = PlayerPrefs.GetString("HighScore" + ghostNo);
            string currentTimeFormatted = FormatTimer(currentTime);

            TimeSpan span1 = TimeSpan.ParseExact(lastBestTime, "hh\\:mm", null);
            TimeSpan span2 = TimeSpan.ParseExact(currentTimeFormatted, "hh\\:mm", null);

            if (span2 < span1)
            {
                PlayerPrefs.SetString("HighScore" + ghostNo, currentTimeFormatted);
                PlayerPrefs.SetInt("NewBest", 1);
                print("New Best");
            }
            else
            {
                print("Not new Best");
                PlayerPrefs.SetInt("NewBest", 0);

            }

        }
        else
        {
            PlayerPrefs.SetString("HighScore"+ghostNo, FormatTimer(currentTime));
            PlayerPrefs.SetInt("NewBest", 1);
            print("New Best");
        }

        audioSource.PlayOneShot(finishedRoundAudio);

        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);

    }

    private void SpawnGhosts() {

        for (int i = 0; i < ghostNo; i++)
        {
            int ghostIndex = UnityEngine.Random.Range(0, ghostPrefabs.Length);
            int spawnIndex = UnityEngine.Random.Range(0, ghostSpawns.Length);

            GameObject ghost = Instantiate(ghostPrefabs[ghostIndex], ghostSpawns[spawnIndex].position, Quaternion.identity);

            ghost.transform.SetParent(ghostOrganizer);
        }
    }


    public int GetGhostNo() {
        ghostNo = PlayerPrefs.GetInt("ghostNo");

        return ghostNo;
    }

    private string FormatTimer(float currentTime)
    {

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
