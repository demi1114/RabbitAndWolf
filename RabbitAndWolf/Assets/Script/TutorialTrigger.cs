using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TutorialObjectChecker : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap targetTilemap;

    [Header("UI")]
    [SerializeField] private Image tutorialImage;

    private Vector3Int tutorialCell;

    void Start()
    {
        // Tutorialオブジェクトが配置されているセル
        tutorialCell = targetTilemap.WorldToCell(transform.position);

        if (tutorialImage != null)
            tutorialImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (IsCurrentPlayerOnSameCell())
        {
            if (!tutorialImage.gameObject.activeSelf)
                tutorialImage.gameObject.SetActive(true);
        }
        else
        {
            if (tutorialImage.gameObject.activeSelf)
                tutorialImage.gameObject.SetActive(false);
        }
    }

    bool IsCurrentPlayerOnSameCell()
    {
        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return false;

        Vector3Int playerCell =
            targetTilemap.WorldToCell(player.transform.position);

        return playerCell == tutorialCell;
    }
}
