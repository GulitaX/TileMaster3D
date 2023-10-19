
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonLevelHandler : MonoBehaviour
{
    public MenuPanelController menuPanelController;
    void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        menuPanelController = FindObjectOfType<MenuPanelController>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClicked(levelManager, button));
    }

    private void OnButtonClicked(LevelManager levelManager, Button button)
    {
        
        TextMeshProUGUI btnText = button.GetComponentInChildren<TextMeshProUGUI>();
        LevelSO selectedLevel = levelManager.levelLists.Find(level => level.name == btnText.text);

        if (selectedLevel != null)
        {
            menuPanelController.audioManager.PlaySoundUI("ui-eventUp01");
            Debug.Log("Selected level: " + selectedLevel.name);
            GameManager.instance.selectedLevel = selectedLevel;
            GameManager.instance.LoadScene("PlayScene", true);
        }
        else
        {
            Debug.LogError("No matching ScriptableObject found for button name: " + btnText.text);
        }

    }

}
