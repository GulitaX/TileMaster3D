using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<LevelSO> levelLists;
    public Button buttonPrefab;
    public GameObject gridPanel;


    void Start()
    {
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
            levelLists = gameManager.levelLists;
        }

        for (int i = 0; i < levelLists.Count; i++) {

            Button newButton = Instantiate(buttonPrefab);
            string levelName = levelLists[i].levelName;

            newButton.transform.SetParent(gridPanel.transform);

            TextMeshProUGUI btnText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = levelName;
            print(btnText.text.ToString());

        }
    }
}
