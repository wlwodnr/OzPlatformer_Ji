using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Prefabs")]
    [SerializeField] private GameObject gameClearUIPrefab; 
    [SerializeField] private GameObject gameOverUIPrefab;

    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 30f; 

    private float timeRemaining;
    private bool isGameActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timeRemaining = timeLimit;
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

        Debug.Log("게임 클리어!");

        if (gameClearUIPrefab != null)
        {
            Instantiate(gameClearUIPrefab);
        }
    }

    public void GameOver()
    {
        if (!isGameActive) return;
        isGameActive = false;

        Debug.Log("게임 오버!");

        if (gameOverUIPrefab != null)
        {
            Instantiate(gameOverUIPrefab);
        }
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}