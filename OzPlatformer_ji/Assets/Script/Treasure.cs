using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private GameObject treasurePrefab; 
    [SerializeField] private Transform treasureSpawnTransform; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance != null && GameManager.Instance.CurrentBoxInstance == null)
            {
                GameObject newBox = Instantiate(treasurePrefab, treasureSpawnTransform.position, treasureSpawnTransform.rotation);

                GameManager.Instance.CurrentBoxInstance = newBox;
            }

            GetComponent<Collider2D>().enabled = false;
        }
    }
}