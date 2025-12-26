using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("Players")]
    [SerializeField] private GameObject[] players;

    private int currentIndex = 0;

    public GameObject CurrentPlayer => players[currentIndex];

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePlayers();
    }

    void InitializePlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(i == currentIndex);
        }
    }

    public void SwitchPlayer()
    {
        GameObject current = players[currentIndex];

        Vector3 sharedPosition = current.transform.position;
        Quaternion sharedRotation = current.transform.rotation;

        // ★ 現在プレイヤーを完全リセット
        ResetPlayer(current);

        current.SetActive(false);

        currentIndex = (currentIndex + 1) % players.Length;

        GameObject next = players[currentIndex];

        next.transform.position = sharedPosition;
        next.transform.rotation = sharedRotation;

        // ★ 次プレイヤーもリセット
        ResetPlayer(next);

        next.SetActive(true);
    }

    void ResetPlayer(GameObject player)
    {
        player.GetComponent<PlayerMove>()?.ResetMove();
        player.GetComponent<PlayerJump>()?.ResetJump();
        player.GetComponent<PlayerDestroy>()?.ResetDestroy();
    }

}
