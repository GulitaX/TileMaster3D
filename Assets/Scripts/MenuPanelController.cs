using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;
    public GameObject MainMenuPanel;
    public GameObject LevelPanel;
    
    void Start()
    {
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
            audioManager = gameManager.gameObject.transform.GetChild(0).GetComponent<AudioManager>();
        }
    }

    public void OnSelectLevelButtonClick()
    {
        audioManager.PlaySoundUI("ui-eventUp02");
    }

    public void OnSelectBackButtonClick() 
    {
        audioManager.PlaySoundUI("ui-eventUp03");
    }
}
