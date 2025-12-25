using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [Header("Stage Tilemaps (10 stages)")]
    [SerializeField] private Tilemap[] stageTilemaps;
    [SerializeField] private int currentStageIndex = 0;

    [Header("Obstacle Tiles")]
    [SerializeField] private RuleTile[] obstacleTiles;

    [Header("Move")]
    [SerializeField] private float moveInterval = 0.12f;
    [SerializeField] private float moveSpeed = 15f;

    [Header("Z Position")]
    [SerializeField] private float playerZ = -2f;

    private float moveTimer;
    private PlayerStateController state;

    private Tilemap CurrentTilemap => stageTilemaps[currentStageIndex];

    void Awake()
    {
        state = GetComponent<PlayerStateController>();
    }

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = playerZ;
        transform.position = pos;
    }

    void Update()
    {
        if (!state.CanMove) return;

        moveTimer -= Time.deltaTime;
        if (moveTimer > 0f) return;

        Vector2 input = GetInputDirection();
        if (input == Vector2.zero) return;

        TryMove(new Vector3Int((int)input.x, (int)input.y, 0));
        moveTimer = moveInterval;
    }

    Vector2 GetInputDirection()
    {
        if (Keyboard.current.wKey.isPressed) return Vector2.up;
        if (Keyboard.current.sKey.isPressed) return Vector2.down;
        if (Keyboard.current.aKey.isPressed) return Vector2.left;
        if (Keyboard.current.dKey.isPressed) return Vector2.right;
        return Vector2.zero;
    }

    void TryMove(Vector3Int dir)
    {
        GetComponent<PlayerJump>()?.SetFacingDirection(dir);

        Tilemap tilemap = CurrentTilemap;

        Vector3Int current = tilemap.WorldToCell(transform.position);
        Vector3Int target = current + dir;

        if (!IsMovable(tilemap, target)) return;

        Vector3 worldPos = tilemap.GetCellCenterWorld(target);
        worldPos.z = playerZ;

        StartCoroutine(MoveCoroutine(worldPos));
    }

    bool IsMovable(Tilemap tilemap, Vector3Int cell)
    {
        TileBase tile = tilemap.GetTile(cell);
        if (tile == null) return false;

        foreach (var obstacle in obstacleTiles)
        {
            if (tile == obstacle)
                return false;
        }
        return true;
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        state.SetState(PlayerState.Moving);

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        state.SetState(PlayerState.Idle);
    }

    // ★ ステージ切り替え用（PlayerJump と同じ index を使う）
    public void SetStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageTilemaps.Length)
            return;

        currentStageIndex = stageIndex;
    }
}
