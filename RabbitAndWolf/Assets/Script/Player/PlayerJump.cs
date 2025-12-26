using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    [Header("Stage Tilemaps (10 stages)")]
    [SerializeField] private Tilemap[] stageTilemaps;
    [SerializeField] private int currentStageIndex = 0;

    [Header("Obstacle Tiles")]
    [SerializeField] private RuleTile[] obstacleTiles;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float playerZ = -2f;

    private InputAction jumpAction;
    private Vector3Int facingDirection = Vector3Int.up;
    private PlayerStateController state;

    private Tilemap CurrentTilemap => stageTilemaps[currentStageIndex];

    public bool IsJumping => state.CurrentState == PlayerState.Jumping;

    public void SetFacingDirection(Vector3Int dir)
    {
        if (dir != Vector3Int.zero)
            facingDirection = dir;
    }

    void Awake()
    {
        state = GetComponent<PlayerStateController>();

        jumpAction = new InputAction(
            "Jump",
            InputActionType.Button,
            "<Mouse>/leftButton");

        jumpAction.performed += _ => TryJump();
    }

    void OnEnable() => jumpAction.Enable();
    void OnDisable() => jumpAction.Disable();

    void TryJump()
    {
        if (!state.CanJump) return;
        Tilemap tilemap = CurrentTilemap;

        Vector3Int current = tilemap.WorldToCell(transform.position);
        Vector3Int target = current + facingDirection * 2;

        TileBase tile = tilemap.GetTile(target);
        if (!IsJumpable(tilemap, target)) return;

        Vector3 worldPos = tilemap.GetCellCenterWorld(target);
        worldPos.z = playerZ;

        StartCoroutine(JumpCoroutine(worldPos));
    }

    bool IsJumpable(Tilemap tilemap, Vector3Int cell)
    {
        // Tile が無い場所は不可
        TileBase tile = tilemap.GetTile(cell);
        if (tile == null) return false;

        // Tile 自体が障害物
        foreach (var obstacle in obstacleTiles)
        {
            if (tile == obstacle)
                return false;
        }

        // ★ 破壊可能オブジェクトがあればジャンプ不可
        Vector3 worldPos = tilemap.GetCellCenterWorld(cell);

        Collider2D hit = Physics2D.OverlapBox(
            worldPos,
            Vector2.one * 0.8f,
            0f
        );

        if (hit)
        {
            IDestructible destructible = hit.GetComponent<IDestructible>();
            if (destructible != null)
                return false;
        }

        return true;
    }

    IEnumerator JumpCoroutine(Vector3 targetPos)
    {
        state.SetState(PlayerState.Jumping);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                jumpSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        state.SetState(PlayerState.Idle);
    }

    // ★ ステージ切り替え用（外部から呼ぶ）
    public void SetStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageTilemaps.Length)
            return;

        currentStageIndex = stageIndex;
    }
}
