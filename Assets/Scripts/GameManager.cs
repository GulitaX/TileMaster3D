using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


//Global Singleton Object
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<LevelSO> levelLists;
    public LevelSO selectedLevel;
    public bool isPause;

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        isPause = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPause = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPause = false;
    }
}
