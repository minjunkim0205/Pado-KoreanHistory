using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject characterCreationPanel;
    public GameObject loadGamePanel;
    public GameObject settingsPanel;

    private void SetActivePanel(GameObject target)
    {
        mainPanel.SetActive(false);
        characterCreationPanel.SetActive(false);
        loadGamePanel.SetActive(false);
        settingsPanel.SetActive(false);
        target.SetActive(true);
    }
    
    public void ShowMain()
    {
        SetActivePanel(mainPanel);
    }

    public void ShowCharacterCreation()
    {
        SetActivePanel(characterCreationPanel);
    }

    public void ShowLoadGame()
    {
        SetActivePanel(loadGamePanel);
    }

    public void ShowSettings()
    {
        SetActivePanel(settingsPanel);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
