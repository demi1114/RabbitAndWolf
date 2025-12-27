using UnityEngine;

public class CameraTileScroll : MonoBehaviour
{
    [Header("Move Amount (World Units)")]
    [SerializeField] private float moveX = 16f;
    [SerializeField] private float moveY = 9f;

    [Header("Camera")]
    [SerializeField] private Camera targetCamera;

    private bool isMoving;

    void LateUpdate()
    {
        if (isMoving) return;

        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return;

        Vector3 viewportPos =
            targetCamera.WorldToViewportPoint(player.transform.position);

        Vector3 moveDir = Vector3.zero;

        if (viewportPos.x > 1f) moveDir.x = moveX;
        else if (viewportPos.x < 0f) moveDir.x = -moveX;

        if (viewportPos.y > 1f) moveDir.y = moveY;
        else if (viewportPos.y < 0f) moveDir.y = -moveY;

        if (moveDir != Vector3.zero)
        {
            transform.position += moveDir;
        }
    }
}
