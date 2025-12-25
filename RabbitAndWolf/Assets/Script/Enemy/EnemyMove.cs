using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    [Header("Stage Tilemaps")]
    [SerializeField] private Tilemap[] stageTilemaps;
    [SerializeField] private int currentStageIndex = 0;

    [Header("Obstacle Tiles")]
    [SerializeField] private RuleTile[] obstacleTiles;

    [Header("Move")]
    [SerializeField] private float moveInterval = 0.5f;
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

        TryRandomMove();
        moveTimer = moveInterval;
    }

    void TryRandomMove()
    {
        Tilemap tilemap = CurrentTilemap;
        Vector3Int current = tilemap.WorldToCell(transform.position);

        // ランダム順に方向を試す
        foreach (var dir in ShuffleDirections())
        {
            Vector3Int target = current + dir;

            if (!IsMovable(tilemap, target))
                continue;

            Vector3 worldPos = tilemap.GetCellCenterWorld(target);
            worldPos.z = enemyZ;

            StartCoroutine(MoveCoroutine(worldPos));
            break;
        }
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

    // ★ 移動方向をランダム順にする
    Vector3Int[] ShuffleDirections()
    {
        Vector3Int[] result = (Vector3Int[])directions.Clone();

        for (int i = 0; i < result.Length; i++)
        {
            int rnd = Random.Range(i, result.Length);
            (result[i], result[rnd]) = (result[rnd], result[i]);
        }

        return result;
    }

    // ★ ステージ切り替え対応
    public void SetStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageTilemaps.Length)
            return;

        currentStageIndex = stageIndex;
    }
}
