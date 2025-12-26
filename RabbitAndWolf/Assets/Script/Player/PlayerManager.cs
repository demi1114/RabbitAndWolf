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

        // ★ 現在位置を保存
        Vector3 sharedPosition = current.transform.position;
        Quaternion sharedRotation = current.transform.rotation;

        current.SetActive(false);

        currentIndex = (currentIndex + 1) % players.Length;

        GameObject next = players[currentIndex];

        // ★ 位置・向きを完全コピー
        next.transform.position = sharedPosition;
        next.transform.rotation = sharedRotation;

        next.SetActive(true);
    }
}
