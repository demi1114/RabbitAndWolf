using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneButton : MonoBehaviour
{
    public void RetryStage()
    {
        // StageIndex ÇÕï€éùÇµÇΩÇ‹Ç‹
        SceneManager.LoadScene("MainScene");
    }

    public void GoToSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
