using UnityEngine;
using UnityEngine.Tilemaps;

public class StoryItemTrigger : MonoBehaviour
{
    [Header("Story")]
    [SerializeField] private int storyId;

    [Header("Tilemap (Reference Only)")]
    [SerializeField] private Tilemap tilemap;

    private Vector3Int itemTilePos;
    private bool obtained;

    private void Start()
    {
        // 自分が配置されているタイル座標を取得
        itemTilePos = tilemap.WorldToCell(transform.position);
    }

    private void Update()
    {
        if (obtained) return;

        // 現在操作中のプレイヤーを取得
        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return;

        // プレイヤー判定（保険）
        PlayerStateController state =
            player.GetComponent<PlayerStateController>();
        if (state == null) return;

        // プレイヤーのタイル座標
        Vector3Int playerTilePos =
            tilemap.WorldToCell(player.transform.position);

        // 同じタイルに入ったら取得
        if (playerTilePos == itemTilePos)
        {
            Obtain();
        }
    }

    private void Obtain()
    {
        obtained = true;

        // Story解放
        StoryUnlockManager.Instance.Unlock(storyId);

        // アイテムは完全に削除
        Destroy(gameObject);
    }
}
