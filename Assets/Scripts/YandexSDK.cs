using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class YandexSDK : MonoBehaviour
{
    public static YandexSDK Instance { get; private set; }

    public static UnityEvent<string> OnLanguageRecived = new UnityEvent<string>();
    public static UnityEvent OnVideoAdvRewarded = new UnityEvent();

    [DllImport("__Internal")]
    private static extern void GetLanguageExtern();

    [DllImport("__Internal")]
    private static extern void ShowVideoAdvExtern();

    [DllImport("__Internal")]
    private static extern void ShowFeedbackExtern();


    public void ShowVideoAdv()
    {
        //SoundManager.Instance.MuteMusic();
        ShowVideoAdvExtern();
    }

    public void ShowVideoAdvCallBack()
    {
        OnVideoAdvRewarded.Invoke();
        //SoundManager.Instance.UnMuteMusic();
    }

    public void GetLanguageExternCallBack(string language)
    {
        OnLanguageRecived.Invoke(language);
    }

    public void ShowFeedback()
    {
        ShowFeedbackExtern();
    }

    public void ShowFeedbackCallBack()
    {
        //SaveSystem.SavePlayerPrefs(SaveSystem.IsFeedbackShowenKey, "true");
    }

    public void GetLanguage()
    {
        GetLanguageExtern();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}