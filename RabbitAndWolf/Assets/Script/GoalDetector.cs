using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GoalHitDetector : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    private bool hasReached;

    void Update()
    {
        if (hasReached) return;

        // 敵と同じ：現在操作中のプレイヤーのみ判定
        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return;

        // タイルマップのセル座標で比較
        Vector3Int goalCell = tilemap.WorldToCell(transform.position);
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);

        if (goalCell == playerCell)
        {
            hasReached = true;
            OnReachGoal(player);
        }
    }

    void OnReachGoal(GameObject player)
    {
        SceneManager.LoadScene("SelectScene");
    }
}
