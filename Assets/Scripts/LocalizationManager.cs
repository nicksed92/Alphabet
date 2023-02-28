using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField] private Languages.DefaultLanguages _defaultLanguage = Languages.DefaultLanguages.en;

    private Dictionary<string, string> texts = new Dictionary<string, string>();

    public static UnityEvent OnLanguageChanged = new UnityEvent();
    public static UnityEvent OnLocalizationLoaded = new UnityEvent();

    public string CurrentLanguage { get; private set; }

    public void SetRuLanguage()
    {
        Languages.DefaultLanguages newLanguage = Languages.DefaultLanguages.ru;

        SetLanguage(newLanguage);
    }

    public void SetEnLanguage()
    {
        Languages.DefaultLanguages newLanguage = Languages.DefaultLanguages.en;

        SetLanguage(newLanguage);
    }

    public void SetLanguage(Languages.DefaultLanguages newLanguage)
    {
        if (newLanguage.ToString() != CurrentLanguage)
        {
            CurrentLanguage = newLanguage.ToString();
            LoadLocalization();
            OnLanguageChanged.Invoke();
        }
        else
        {
            throw new Exception($"Localization Error!: \"{newLanguage}\" already enabled!");
        }
    }

    public string GetText(string key)
    {
        if (!texts.ContainsKey(key))
            throw new Exception($"Localization Error!: \"{key}\" not found in keys!");

        return texts[key];
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

        YandexSDK.OnLanguageRecived.AddListener(OnLanguageRecived);
    }

    private void Start()
    {
        YandexSDK.Instance.GetLanguage();
        //OnLanguageRecived(_defaultLanguage.ToString());
    }

    private void OnLanguageRecived(string language)
    {
        if (language == null)
            CurrentLanguage = _defaultLanguage.ToString();
        else
            CurrentLanguage = language;

        LoadLocalization();
    }

    private void LoadLocalization()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Localization/" + CurrentLanguage);

        if (textAsset == null)
            throw new Exception($"Localization Error!: \"{CurrentLanguage}.json\" not found in \"Localization/\" folder!");

        texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(textAsset.text);

        Debug.Log("Localization loaded successfully!");

        OnLocalizationLoaded.Invoke();
    }
}