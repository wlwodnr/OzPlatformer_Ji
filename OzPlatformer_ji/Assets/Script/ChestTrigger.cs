using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;

public class ChestTrigger : MonoBehaviour
{
    private Chest chest;
    private bool hasOpened = false;

    void Start()
    {
        chest = GetComponent<Chest>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasOpened)
        {
            if (chest != null)
            {
                chest.Open();
                hasOpened = true;

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameClear();
                }
            }
        }
    }
}