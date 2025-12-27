using UnityEngine;
using UnityEngine.UI;

public class StorySelectButton : MonoBehaviour
{
    [SerializeField] private int storyId;
    [SerializeField] private Button button;
    [SerializeField] private StorySceneManager storySceneManager;

    private void Start()
    {
        bool unlocked = StoryUnlockManager.Instance.IsUnlocked(storyId);
        button.interactable = unlocked;

        if (unlocked)
        {
            button.onClick.AddListener(() =>
            {
                storySceneManager.StartStory(storyId);
            });
        }
    }
}
