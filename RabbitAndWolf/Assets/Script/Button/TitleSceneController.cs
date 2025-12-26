using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    // SelectScene へ
    public void GoToSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    // SettingScene へ
   /* public void GoToSettingScene()
    {
        SceneManager.LoadScene("SettingScene");
    }*/

    // ゲーム終了
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();

#if UNITY_EDITOR
        // エディタ上でも終了確認できるように
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
