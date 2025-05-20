using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour
{
    public GameObject buttonPrefab;                // 캐릭터 선택 버튼 프리팹
    public Transform buttonContainer;              // 버튼이 들어갈 Content 부모
    public Button loadButton;                      // Load 버튼 (선택 안 됐을 때 비활성화 가능)

    private string saveDirectory;
    private Dictionary<string, PlayerData> loadedData = new Dictionary<string, PlayerData>();
    private PlayerData selectedPlayer;             // 현재 선택된 플레이어

    private void Awake()
    {
        saveDirectory = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "My Games", "Pado");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        // 선택 안 됐을 경우 Load 버튼 비활성화 (선택사항)
        if (loadButton != null)
            loadButton.interactable = false;
    }

    private void OnEnable()
    {
        ReloadButtons();
    }

    public void ReloadButtons()
    {
        Debug.Log("🔁 ReloadButtons() 호출됨");

        // 기존 버튼 제거
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        loadedData.Clear();
        selectedPlayer = null;

        if (loadButton != null)
            loadButton.interactable = false;

        string[] files = Directory.GetFiles(saveDirectory, "*.json");

        foreach (string filePath in files)
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            loadedData[data.playerName] = data;

            GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
            btnObj.GetComponentInChildren<TMP_Text>().text = $"{data.playerName} (Lv.{data.level})";

            btnObj.GetComponent<Button>().onClick.AddListener(() => OnCharacterSelected(data.playerName));
        }
    }

    public void OnCharacterSelected(string playerName)
    {
        if (!loadedData.ContainsKey(playerName))
        {
            Debug.LogError($"선택한 캐릭터 {playerName} 이(가) 로딩되지 않았습니다.");
            return;
        }

        selectedPlayer = loadedData[playerName];
        Debug.Log($"캐릭터 선택됨: {selectedPlayer.playerName}");

        if (loadButton != null)
            loadButton.interactable = true;
    }

    public void LoadSelectedCharacter()
    {
        if (selectedPlayer == null)
        {
            Debug.LogWarning("선택된 캐릭터가 없습니다!");
            return;
        }

        CurrentUser.player = selectedPlayer;

        Debug.Log($"[✔] Load 버튼 클릭됨 → 씬 전환: {selectedPlayer.playerName}");

        SceneManager.LoadScene("Village");
    }
}
