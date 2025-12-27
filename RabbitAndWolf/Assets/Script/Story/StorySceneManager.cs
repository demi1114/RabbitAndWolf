using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorySceneManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private Button nextButton;

    [Header("Story Data")]
    [SerializeField] private StoryData[] stories;

    private StoryData currentStory;
    private int index;

    public void StartStory(int storyId)
    {
        if (!StoryUnlockManager.Instance.IsUnlocked(storyId))
            return;

        currentStory = stories[storyId];
        index = 0;
        Show();
    }

    public void Next()
    {
        index += 2;

        if (index >= currentStory.lines.Length)
        {
            nextButton.interactable = false;
            return;
        }

        Show();
    }

    private void Show()
    {
        string text = currentStory.lines[index];

        if (index + 1 < currentStory.lines.Length)
        {
            text += "\n" + currentStory.lines[index + 1];
        }

        storyText.text = text;
        nextButton.interactable = true;
    }
}
