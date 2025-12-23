using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    [Header("RuleTiles")]
    [SerializeField] private RuleTile redRuleTile;
    [SerializeField] private RuleTile blueRuleTile;
    [SerializeField] private RuleTile orangeRuleTile;
    [SerializeField] private RuleTile purpleRuleTile;

    [Header("Move")]
    [SerializeField] private float moveInterval = 0.12f; // Åö à⁄ìÆä‘äuÅiè¨Ç≥Ç¢ÇŸÇ«ë¨Ç¢Åj
    [SerializeField] private float moveSpeed = 15f;

    [Header("Z Position")]
    [SerializeField] private float playerZ = -2f;

    private bool isMoving = false;
    private float moveTimer = 0f;

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = playerZ;
        transform.position = pos;
    }

    void Update()
    {
        if (isMoving) return;

        moveTimer -= Time.deltaTime;
        if (moveTimer > 0f) return;

        Vector2 input = GetInputDirection();
        if (input == Vector2.zero) return;

        TryMove(new Vector3Int((int)input.x, (int)input.y, 0));
        moveTimer = moveInterval;
    }

    Vector2 GetInputDirection()
    {
        // ècâ°Ç«ÇøÇÁÇ©àÍï˚å¸ÇÃÇ›ÅiéŒÇﬂñhé~Åj
        if (Keyboard.current.wKey.isPressed) return Vector2.up;
        if (Keyboard.current.sKey.isPressed) return Vector2.down;
        if (Keyboard.current.aKey.isPressed) return Vector2.left;
        if (Keyboard.current.dKey.isPressed) return Vector2.right;

        return Vector2.zero;
    }

    void TryMove(Vector3Int direction)
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + direction;

        TileBase targetTile = tilemap.GetTile(targetCell);

        if (targetTile == null) return;
        if (targetTile == redRuleTile) return;

        Vector3 targetWorldPos = tilemap.GetCellCenterWorld(targetCell);
        targetWorldPos.z = playerZ;
        GetComponent<PlayerJump>()?.SetFacingDirection(direction);

        StartCoroutine(MoveCoroutine(targetWorldPos));
    }

    IEnumerator MoveCoroutine(Vector3 targetPos)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
