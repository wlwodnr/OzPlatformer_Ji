using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private GameObject treasurePrefab; // 보물상자 '원본 프리팹'을 연결
    [SerializeField] private Transform treasureSpawnTransform; // 자식인 TreasureTransform을 연결

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null && GameManager.Instance.CurrentBoxInstance == null)
            {
                // 지정된 위치에 상자 생성
                GameObject newBox = Instantiate(treasurePrefab, treasureSpawnTransform.position, treasureSpawnTransform.rotation);

                GameManager.Instance.CurrentBoxInstance = newBox;
            }

            GetComponent<Collider2D>().enabled = false;
        }
    }
}