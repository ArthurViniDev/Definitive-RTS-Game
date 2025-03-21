using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float edgeThreshold = 50f;

    [SerializeField] private GameObject cameraLocked;

    public bool screenLock;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ScreenLock();
        }

        cameraLocked.SetActive(screenLock);

        if (screenLock) return;

        Vector3 moveDirection = Vector3.zero;
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mousePosition.x >= screenWidth - edgeThreshold)
            moveDirection.x = 1;
        // Movimento para a esquerda
        else if (mousePosition.x <= edgeThreshold)
            moveDirection.x = -1;
        // Movimento para cima
        if (mousePosition.y >= screenHeight - edgeThreshold)
            moveDirection.y = 1;
        // Movimento para baixo
        else if (mousePosition.y <= edgeThreshold)
            moveDirection.y = -1;

        if (moveDirection.y + moveDirection.x == 0)
        {
            moveDirection.y /= 2;
            moveDirection.x /= 2;
        }
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void ScreenLock() => screenLock = !screenLock;
}