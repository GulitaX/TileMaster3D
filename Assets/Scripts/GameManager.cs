using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;


//Global Singleton Object
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<LevelSO> levelLists;
    public LevelSO selectedLevel;
    public bool isPause;

    public bool isCountdownRunning;
    public float timer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            isPause = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, bool sceneHasCountdown)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        isCountdownRunning = false;
        isPause = false;
        timer = 0;

        if (sceneHasCountdown )
        {
            isCountdownRunning = true;
            timer = selectedLevel.countDown;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isCountdownRunning = false;
        isPause = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isCountdownRunning = true;
        isPause = false;
    }

    public void Update()
    {
        if(timer > 0f && isCountdownRunning)
        {
            timer -= Time.deltaTime * Time.timeScale;
            Debug.Log("Time remaining: " + timer.ToString("0"));
        }
    }

}
