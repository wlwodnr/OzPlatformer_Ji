using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("X Position Settings")]
    [SerializeField] private float fixedWorldX = 0f; // 고정할 월드 X 좌표 값

    [Header("Height Limits")]
    [SerializeField] private float maxY = 20f; // 카메라가 올라갈 수 있는 최대 Y 값

    private void LateUpdate()
    {
        // 1. 현재 카메라의 전역(World) 위치를 가져옵니다.
        Vector3 currentPosition = transform.position;

        // 2. X 좌표는 플레이어를 따라가지 못하도록 무조건 지정된 값으로 고정합니다.
        currentPosition.x = fixedWorldX;

        // 3. Y 좌표가 설정한 최대값(Max Y)을 넘지 않도록 제한(Clamp)합니다.
        if (currentPosition.y > maxY)
        {
            currentPosition.y = maxY;
        }

        // 4. 최종 계산된 위치를 카메라의 월드 좌표에 다시 적용합니다.
        transform.position = currentPosition;
    }
}