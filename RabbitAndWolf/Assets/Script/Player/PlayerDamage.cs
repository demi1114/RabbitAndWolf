using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public class PlayerDamage : MonoBehaviour, IDamageable
{
    [Header("HP")]
    [SerializeField] private int maxHp = 3;
    private int currentHp;

    private bool isInvincible;

    void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHp -= damage;
        Debug.Log($"{gameObject.name} HP: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleCoroutine());
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} Dead");
        // キャラ切り替え or ゲームオーバー処理
    }

    IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }
}
