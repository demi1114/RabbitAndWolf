using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBackToTitleButton : MonoBehaviour
{
    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
