using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneButton : MonoBehaviour
{
    [Header("Stage Index")]
    [SerializeField] private int stageIndex;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (stageIndex == 0)
        {
            button.interactable = true;
            return;
        }

        // 1つ前のステージがクリアされているか
        int prevClear = PlayerPrefs.GetInt($"StageClear_{stageIndex - 1}", 0);
        button.interactable = prevClear == 1;
    }

    public void GoToMainScene()
    {
        PlayerPrefs.SetInt("StageIndex", stageIndex);
        SceneManager.LoadScene("MainScene");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
