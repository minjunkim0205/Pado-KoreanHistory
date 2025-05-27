using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // 따라갈 플레이어
    public float smoothSpeed = 0.125f;  // 부드러운 이동
    public Vector2 minPosition;         // 카메라 최소 위치 (왼쪽 한계)
    public Vector2 maxPosition;         // 카메라 최대 위치 (오른쪽 한계)

    void LateUpdate()
    {
        if (target == null)
            return;

        // 따라갈 목표 위치
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // 부드럽게 따라가기
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Clamp (카메라 이동 범위 제한)
        float clampedX = Mathf.Clamp(smoothPosition.x, minPosition.x, maxPosition.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
