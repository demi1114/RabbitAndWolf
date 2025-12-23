using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    [Header("Obstacle Tile")]
    [SerializeField] private RuleTile redRuleTile;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float playerZ = -2f;

    private bool isJumping = false;

    // Input System
    private InputAction jumpAction;

    // 向き
    private Vector3Int facingDirection = Vector3Int.up;

    public void SetFacingDirection(Vector3Int dir)
    {
        if (dir != Vector3Int.zero)
            facingDirection = dir;
    }

    void Awake()
    {
        // 左クリック専用 InputAction を作成
        jumpAction = new InputAction(
            name: "Jump",
            type: InputActionType.Button,
            binding: "<Mouse>/leftButton"
        );

        jumpAction.performed += OnJumpPerformed;
    }

    void OnEnable()
    {
        jumpAction.Enable();
    }

    void OnDisable()
    {
        jumpAction.Disable();
    }

    void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        // ★ ここで完全に弾く
        if (isJumping)
            return;

        TryJump();
    }

    void TryJump()
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int jumpTargetCell = currentCell + facingDirection * 2;

        TileBase targetTile = tilemap.GetTile(jumpTargetCell);

        if (targetTile == null) return;
        if (targetTile == redRuleTile) return;

        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(jumpTargetCell);
        targetWorldPos.z = playerZ;

        StartCoroutine(JumpCoroutine(targetWorldPos));
    }

    IEnumerator JumpCoroutine(Vector3 targetPos)
    {
        isJumping = true;

        // ★ 入力を完全停止
        jumpAction.Disable();

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                jumpSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPos;

        isJumping = false;

        // ★ 処理終了後に入力再開
        jumpAction.Enable();
    }
}
