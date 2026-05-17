using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("X Position Settings")]
    [SerializeField] private float fixedWorldX = 0f; 
    [Header("Height Limits")]
    [SerializeField] private float maxY = 20f; 

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = fixedWorldX;

        if (currentPosition.y > maxY)
        {
            currentPosition.y = maxY;
        }

        transform.position = currentPosition;
    }
}