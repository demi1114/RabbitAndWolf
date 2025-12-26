using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public interface IDestructible
{
    void DestroyObject();
}
public class PlayerDestroy : MonoBehaviour
{
    [Header("Stage Tilemaps")]
    [SerializeField] private Tilemap[] stageTilemaps;
    [SerializeField] private int currentStageIndex = 0;

    [Header("Destroy")]
    [SerializeField] private float playerZ = -2f;

    private Vector3Int facingDirection = Vector3Int.up;
    private PlayerStateController state;

    private Tilemap CurrentTilemap => stageTilemaps[currentStageIndex];

    private InputAction destroyAction;

    void Awake()
    {
        state = GetComponent<PlayerStateController>();

        destroyAction = new InputAction(
            "Destroy",
           InputActionType.Button,
            "<Mouse>/leftButton");

        destroyAction.performed += _ => TryDestroy();
    }

    void OnEnable() => destroyAction.Enable();
    void OnDisable() => destroyAction.Disable();

    // PlayerMove から向きを受け取る
    public void SetFacingDirection(Vector3Int dir)
    {
        if (dir != Vector3Int.zero)
            facingDirection = dir;
    }

    void TryDestroy()
    {
        if (!state.CanMove) return;

        Tilemap tilemap = CurrentTilemap;

        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + facingDirection;

        Vector3 worldPos = tilemap.GetCellCenterWorld(targetCell);

        Collider2D hit = Physics2D.OverlapBox(
            worldPos,
            Vector2.one * 0.8f,
            0f
        );

        if (!hit) return;

        IDestructible destructible = hit.GetComponent<IDestructible>();
        if (destructible != null)
        {
            destructible.DestroyObject();
        }
    }

    // ステージ切り替え
    public void SetStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageTilemaps.Length)
            return;

        currentStageIndex = stageIndex;
    }

    public void ResetDestroy()
    {
        facingDirection = Vector3Int.up;

        if (state != null)
            state.ResetState();
    }

}
