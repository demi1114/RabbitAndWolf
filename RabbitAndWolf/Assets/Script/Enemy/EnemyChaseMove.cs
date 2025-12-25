using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class EnemyChaseMove : MonoBehaviour
{
    [Header("Stage Tilemaps")]
    [SerializeField] private Tilemap[] stageTilemaps;
    [SerializeField] private int currentStageIndex = 0;

    [Header("Obstacle Tiles")]
    [SerializeField] private RuleTile[] obstacleTiles;

    [Header("Move")]
    [SerializeField] private float moveInterval = 0.4f;
    [SerializeField] private float moveSpeed = 10f;

    [Header("Z Position")]
    [SerializeField] private float enemyZ = -2f;

    private bool isMoving;
    private float moveTimer;

    private Tilemap CurrentTilemap => stageTilemaps[currentStageIndex];

    private readonly Vector3Int[] directions =
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = enemyZ;
        transform.position = pos;

        moveTimer = moveInterval;
    }

    void Update()
    {
        if (isMoving) return;

        moveTimer -= Time.deltaTime;
        if (moveTimer > 0f) return;

        TryChaseMove();
        moveTimer = moveInterval;
    }

    void TryChaseMove()
    {
        GameObject player = PlayerManager.Instance.CurrentPlayer;

        if (player == null) return;

        Tilemap tilemap = CurrentTilemap;

        Vector3Int enemyCell = tilemap.WorldToCell(transform.position);
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);

        // プレイヤーに近づく順に方向を並べる
        List<Vector3Int> sortedDirs = new List<Vector3Int>(directions);
        sortedDirs.Sort((a, b) =>
        {
            int distA = ManhattanDistance(enemyCell + a, playerCell);
            int distB = ManhattanDistance(enemyCell + b, playerCell);
            return distA.CompareTo(distB);
        });

        foreach (var dir in sortedDirs)
        {
            Vector3Int target = enemyCell + dir;

            if (!IsMovable(tilemap, target))
                continue;

            Vector3 worldPos = tilemap.GetCellCenterWorld(target);
            worldPos.z = enemyZ;

            StartCoroutine(MoveCoroutine(worldPos));
            break;
        }
    }

    int ManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
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
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    // ★ ステージ切り替え対応
    public void SetStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageTilemaps.Length)
            return;

        currentStageIndex = stageIndex;
    }
}
