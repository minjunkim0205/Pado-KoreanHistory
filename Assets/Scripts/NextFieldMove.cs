using UnityEngine;
using UnityEngine.SceneManagement;

public class NextFieldMove : MonoBehaviour
{
    public string targetScene;              // 이동할 씬 이름

    [Header("마차 스프라이트 설정")]
    public Sprite normalSprite;             // 기본 스프라이트
    public Sprite highlightSprite;          // 강조 스프라이트

    private SpriteRenderer sr;
    private bool playerInRange = false;
    private GameObject player;
    private GameObject eIcon;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null && normalSprite != null)
            sr.sprite = normalSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInRange = true;

            // E 아이콘 표시
            eIcon = player.transform.Find("E_Icon")?.gameObject;
            if (eIcon != null)
                eIcon.SetActive(true);

            // 마차 강조
            if (sr != null && highlightSprite != null)
                sr.sprite = highlightSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // E 아이콘 숨김
            if (eIcon != null)
                eIcon.SetActive(false);

            // 마차 원래 스프라이트 복원
            if (sr != null && normalSprite != null)
                sr.sprite = normalSprite;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("▶ E 키 눌림, 씬 전환!");
            SceneManager.LoadScene(targetScene);
        }
    }
}
