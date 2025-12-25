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

        // š ƒQ[ƒ€ŠJn‚Ì Active §Œä
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
        players[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % players.Length;
        players[currentIndex].SetActive(true);
    }
}
