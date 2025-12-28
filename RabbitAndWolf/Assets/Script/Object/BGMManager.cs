using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource bgmSource;

    [Header("BGM Clips")]
    [SerializeField] private AudioClip titleBGM;
    [SerializeField] private AudioClip mainBGM;
    [SerializeField] private AudioClip resultBGM;
    [SerializeField] private AudioClip clearBGM;

    private AudioClip currentClip;

    private void Awake()
    {
        // ÉVÉìÉOÉãÉgÉì
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMByScene(scene.name);
    }

    private void PlayBGMByScene(string sceneName)
    {
        AudioClip nextClip = null;

        switch (sceneName)
        {
            case "TitleScene":
            case "SettingScene":
            case "SelectScene":
                nextClip = titleBGM;
                break;

            case "MainScene":
                nextClip = mainBGM;
                break;

            case "ResultScene":
            case "ClearScene":
                nextClip = resultBGM;
                break;

            case "FullClearScene":
                nextClip = clearBGM;
                break;
        }

        // ìØÇ∂BGMÇ»ÇÁâΩÇ‡ÇµÇ»Ç¢ÅiìrêÿÇÍñhé~Åj
        if (currentClip == nextClip || nextClip == null)
            return;

        currentClip = nextClip;
        bgmSource.clip = currentClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
