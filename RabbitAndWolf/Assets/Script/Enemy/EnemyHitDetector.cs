using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyHitDetector : MonoBehaviour
{
    [Header("Tilemap")]
    [SerializeField] private Tilemap tilemap;

    private bool hasHit;

    void Update()
    {
        if (hasHit) return;

        GameObject player = PlayerManager.Instance.CurrentPlayer;
        if (player == null) return;

        Vector3Int enemyCell = tilemap.WorldToCell(transform.position);
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);

        if (enemyCell == playerCell)
        {
            hasHit = true;
            OnHitPlayer(player);
        }
    }

    void OnHitPlayer(GameObject player)
    {
        Debug.Log("Hit Current Player");

        player.GetComponent<IDamageable>()?.TakeDamage(1);
    }
}
