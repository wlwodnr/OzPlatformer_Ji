using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Object Settings")]
    [SerializeField] private GameObject gameClearUIObject;
    [SerializeField] private GameObject gameOverUIObject;

    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 30f;

    [Header("Reset Settings (No Scene Load)")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Collider2D spawnZoneCollider; 

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;

    private float timeRemaining;
    private bool isGameActive = true;

    private Vector3 playerStartPosition;
    private Vector3 cameraStartPosition;

    public GameObject CurrentBoxInstance { get; set; }
    public bool IsGameActive => isGameActive;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        timeRemaining = timeLimit;

        if (playerTransform != null) playerStartPosition = playerTransform.position;
        if (cameraTransform != null) cameraStartPosition = cameraTransform.position;

        if (gameClearUIObject != null) gameClearUIObject.SetActive(false);
        if (gameOverUIObject != null) gameOverUIObject.SetActive(false);
    }

    private void Update()
    {
        if (!isGameActive) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    public void GameClear()
    {
        if (!isGameActive) return;
        isGameActive = false;
        if (gameClearUIObject != null) gameClearUIObject.SetActive(true);
    }

    public void GameOver()
    {
        if (!isGameActive) return;
        isGameActive = false;
        if (gameOverUIObject != null) gameOverUIObject.SetActive(true);
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }

    public void RestartGame()
    {
        Debug.Log("게임 재시작 및 카메라/물리 정밀 초기화");

        // 1. UI 비활성화
        if (gameClearUIObject != null) gameClearUIObject.SetActive(false);
        if (gameOverUIObject != null) gameOverUIObject.SetActive(false);

        if (playerTransform != null)
        {
            Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }
            playerTransform.position = playerStartPosition;
        }

        if (cameraTransform != null)
        {
            cameraTransform.position = cameraStartPosition;

            MonoBehaviour cameraScript = cameraTransform.GetComponent<MonoBehaviour>();
            if (cameraScript != null)
            {
                cameraScript.enabled = false;
                cameraScript.enabled = true;
            }
        }

        if (CurrentBoxInstance != null)
        {
            Destroy(CurrentBoxInstance);
            CurrentBoxInstance = null;
        }

        if (spawnZoneCollider != null)
        {
            spawnZoneCollider.enabled = true;
        }

        timeRemaining = timeLimit;
        isGameActive = true;
    }
}