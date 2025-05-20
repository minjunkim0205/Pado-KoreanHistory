using UnityEngine;
using System.IO;
using TMPro;

public class CharacterCreationManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public UserInterfaceManager userInterfaceManager;

    private string saveDirectory;

    void Start()
    {
        saveDirectory = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "My Games", "Pado");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }
    }

    public void CreateCharacter()
    {
        string name = nameInputField.text;

        if (string.IsNullOrWhiteSpace(name))
        {
            Debug.LogWarning("[Error] Check {NameInputField} !!");
            return;
        }

        PlayerData newPlayer = new PlayerData
        {
            playerName = name
        };

        string json = JsonUtility.ToJson(newPlayer, true);
        string filePath = Path.Combine(saveDirectory, $"{name}.json");

        File.WriteAllText(filePath, json);
        Debug.Log($"[Success] Character has been saved {filePath}");

        nameInputField.text = "";
        userInterfaceManager.ShowMain();
    }
}
