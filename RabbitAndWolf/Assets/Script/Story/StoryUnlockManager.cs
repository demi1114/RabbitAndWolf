using UnityEngine;

public class StoryUnlockManager : MonoBehaviour
{
    public static StoryUnlockManager Instance { get; private set; }

    [SerializeField] private bool[] storyUnlocked = new bool[3];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Unlock(int storyId)
    {
        if (storyId < 0 || storyId >= storyUnlocked.Length) return;
        storyUnlocked[storyId] = true;
    }

    public bool IsUnlocked(int storyId)
    {
        return storyUnlocked[storyId];
    }
}
