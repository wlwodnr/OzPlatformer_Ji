using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private GameObject treasureObject;
    [SerializeField]
    private Transform treasureSpanwTranform;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (treasureSpanwTranform != null)
            {
                Instantiate(treasureObject, treasureSpanwTranform.position, treasureSpanwTranform.rotation);
            }
            else
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}
