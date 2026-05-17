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
    [SerializeField] private Collider2D spawnZoneCollider; // 씬에 있는 TreasureSpawn의 콜라이더를 연결

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;

    private float timeRemaining;
    private bool isGameActive = true;

    private Vector3 playerStartPosition;
    private Vector3 cameraStartPosition;

    // 보물상자 주소를 외부(Treasure.cs)에서 등록하고 조율할 수 있도록 프로퍼티로 개방
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

        // 2. 물리 엔진 연산을 일시 정지하고 플레이어 좌표 강제 이동 (카메라 튐 방지)
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

        // 3. 카메라 위치 강제 강제 리셋 및 카메라 추적 스크립트 오작동 방지
        if (cameraTransform != null)
        {
            cameraTransform.position = cameraStartPosition;

            // 혹시 카메라에 부드러운 추적 로직(SmoothFollow 등)이 있다면 내부 변수를 리셋하기 위해 
            // 컴포넌트를 깜빡 껐다 켜주면 깨짐 현상이 완벽히 해결됩니다.
            MonoBehaviour cameraScript = cameraTransform.GetComponent<MonoBehaviour>();
            if (cameraScript != null)
            {
                cameraScript.enabled = false;
                cameraScript.enabled = true;
            }
        }

        // 4. 열려 있는 기존 상자 알맹이만 파괴하고 변수 비우기
        if (CurrentBoxInstance != null)
        {
            Destroy(CurrentBoxInstance);
            CurrentBoxInstance = null;
        }

        // 5. 꺼두었던 스폰 구역의 콜라이더를 다시 켜서 상자가 새로 생성될 수 있게 준비
        if (spawnZoneCollider != null)
        {
            spawnZoneCollider.enabled = true;
        }

        // 6. 데이터 리셋
        timeRemaining = timeLimit;
        isGameActive = true;
    }
}