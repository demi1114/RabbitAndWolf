using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneButton : MonoBehaviour
{
    /// <summary>
    /// 次のステージへ進む
    /// </summary>
    public void GoToNextStage()
    {
        int currentStage = PlayerPrefs.GetInt("StageIndex", 0);
        int nextStage = currentStage + 1;

        // 次のステージ番号を保存
        PlayerPrefs.SetInt("StageIndex", nextStage);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainScene");
    }

    public void GoToSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void GoToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
