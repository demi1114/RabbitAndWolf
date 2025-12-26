using UnityEngine;
using UnityEngine.Tilemaps;

public class MainStageManager : MonoBehaviour
{
    [Header("Stage Tilemaps (10 stages)")]
    [SerializeField] private GameObject[] stageRoots;
    // ↑ Tilemap が入った親 GameObject を10個入れる

    [Header("Player")]
    [SerializeField] private PlayerMove[] playerMoves;
    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerDestroy playerDestroy;

    void Start()
    {
        int stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        ApplyStage(stageIndex);
    }

    void ApplyStage(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= stageRoots.Length)
            stageIndex = 0;

        // ステージ表示切り替え
        for (int i = 0; i < stageRoots.Length; i++)
        {
            stageRoots[i].SetActive(i == stageIndex);
        }

        // Player 各処理にステージ番号を同期
        foreach (var move in playerMoves)
        {
            if (move != null)
                move.SetStage(stageIndex);
        }
        playerJump.SetStage(stageIndex);
        playerDestroy.SetStage(stageIndex);
    }
}
