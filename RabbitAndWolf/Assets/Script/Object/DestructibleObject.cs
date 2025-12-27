using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDestructible
{
    [Header("Block Move")]
    [SerializeField] private bool blockMovement = true;

    public bool IsBlocking => blockMovement;

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}