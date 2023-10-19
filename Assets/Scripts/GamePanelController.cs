using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePanelController : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;
    public GameObject GameOverPanel;
    public GameObject CompletedPanel;
    public GameObject InGamePanel;

    [SerializeField]
    private TextMeshProUGUI timerText;
    public float remainTime { get; private set; }
    
    void Start ()
    {
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
            audioManager = gameManager.gameObject.transform.GetChild(0).GetComponent<AudioManager>();
            remainTime = gameManager.selectedLevel.countDown;
        }
    }

    public void OnPauseButtonClick()
    {
        gameManager.PauseGame();
        audioManager.PlaySoundUI("ui-eventUp03");
    }

    public void OnResumeButtonClick()
    {
        gameManager.ResumeGame();
        audioManager.PlaySoundUI("ui-eventUp06");
    }

    public void OnQuitbuttonClick()
    {
        gameManager.LoadScene("Menu", false);
        audioManager.ChangeBackGroundMusic("bg-1");
        audioManager.PlaySoundUI("ui-eventUp03");
    }

    public void OnGameOver()
    {
        gameManager.PauseGame();
        GameOverPanel.SetActive(true);
        InGamePanel.transform.Find("Pause Button").gameObject.SetActive(false);

    }

    public void OnLevelCompleted()
    {

        gameManager.PauseGame();
        CompletedPanel.SetActive(true);
        InGamePanel.transform.Find("Pause Button").gameObject.SetActive(false);
        audioManager.PlaySoundUI("ui-eventUp05");
        if (gameManager.selectedLevel.levelIndex == gameManager.levelLists.Count -1)
        {
            Debug.Log("this is the last level");
            GameObject nextLevelbtn = CompletedPanel.transform.Find("NextLevel Button").gameObject;
            nextLevelbtn.SetActive(false);
        }
    }

    public void OnNextLevel()
    {
        audioManager.PlaySoundUI("ui-eventUp06");

        if (gameManager.selectedLevel.levelIndex != gameManager.levelLists.Count - 1)
        {
            LevelSO nextLevel =  gameManager.levelLists[gameManager.selectedLevel.levelIndex + 1];
            gameManager.selectedLevel = nextLevel;

            GameManager.instance.LoadScene("PlayScene", true);

            gameManager.ResumeGame();
        }

    }

    private void Update()
    {
        remainTime = gameManager.timer;

        if(remainTime < 0)
        {
            remainTime = 0;
        }
        
        int seconds = Mathf.FloorToInt(remainTime % 60);
        int minutes = Mathf.FloorToInt(remainTime / 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }

}
