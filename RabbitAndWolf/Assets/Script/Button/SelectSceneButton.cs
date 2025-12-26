using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButton : MonoBehaviour
{
    public void GoToMainScene(int stageIndex)
    {
        PlayerPrefs.SetInt("StageIndex", stageIndex);
        SceneManager.LoadScene("MainScene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
