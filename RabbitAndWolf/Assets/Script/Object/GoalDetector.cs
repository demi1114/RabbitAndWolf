using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GoalDetector : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    private bool hasReached;

    void Update()
    {
        if (hasReached) return;

        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return;

        Vector3Int goalCell = tilemap.WorldToCell(transform.position);
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);

        if (goalCell == playerCell)
        {
            hasReached = true;
            OnReachGoal();
        }
    }

    void OnReachGoal()
    {
        // 現在のステージ番号取得
        int stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        // ステージクリア保存
        PlayerPrefs.SetInt($"StageClear_{stageIndex}", 1);
        PlayerPrefs.Save();

        // ステージ10（index 9）のみ FullClearScene
        if (stageIndex == 9)
        {
            SceneManager.LoadScene("FullClearScene");
        }
        else
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
