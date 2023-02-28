using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _languageRuButton;
    [SerializeField] private Button _languageEnButton;
    [SerializeField] private Button _ratingButton;
    [SerializeField] private Text _title;

    [SerializeField] private Animator _menuAnimator;

    public static UnityEvent OnLearn = new UnityEvent();
    public static UnityEvent OnPlay = new UnityEvent();

    private void Awake()
    {
        BackButton.OnClicked.AddListener(ShowMenu);
        LocalizationManager.OnLanguageChanged.AddListener(SetText);
        _playButton.onClick.AddListener(OnPlayClick);
        _learnButton.onClick.AddListener(OnLearnClick);
        _ratingButton.onClick.AddListener(OnRateClick);
        _languageRuButton.onClick.AddListener(SetLangEn);
        _languageEnButton.onClick.AddListener(SetLangRu);
    }

    private void Start()
    {
        SetText();
        SetLangButtonVisible();
    }

    private void OnLearnClick()
    {
        BaseClick();
        OnLearn.Invoke();
    }

    private void SetLangEn()
    {
        LocalizationManager.Instance.SetEnLanguage();
        SoundManager.Instance.PlaySound("Click");
    }

    private void SetLangRu()
    {
        LocalizationManager.Instance.SetRuLanguage();
        SoundManager.Instance.PlaySound("Click");
    }

    private void ShowMenu()
    {
        if (_menuAnimator.enabled == false)
            _menuAnimator.enabled = true;
        else
            _menuAnimator.SetTrigger("Show");
    }

    private void SetLangButtonVisible()
    {
        _languageRuButton.gameObject.SetActive(false);
        _languageEnButton.gameObject.SetActive(false);

        if (LocalizationManager.Instance.CurrentLanguage == Languages.DefaultLanguages.ru.ToString())
            _languageRuButton.gameObject.SetActive(true);
        else
            _languageEnButton.gameObject.SetActive(true);
    }

    private void OnPlayClick()
    {
        BaseClick();
        OnPlay.Invoke();
    }

    private void OnRateClick()
    {
        SoundManager.Instance.PlaySound("Click");
        YandexSDK.Instance.ShowFeedback();
    }

    private void BaseClick()
    {
        _menuAnimator.SetTrigger("Hide");
        SoundManager.Instance.PlaySound("Click");
    }

    private void SetText()
    {
        _playButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LocalizationManager.Instance.GetText("play");
        _learnButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LocalizationManager.Instance.GetText("learn");
        _languageRuButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LocalizationManager.Instance.GetText("language");
        _languageEnButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LocalizationManager.Instance.GetText("language");
        _ratingButton.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = LocalizationManager.Instance.GetText("rate");
        _title.GetComponent<Text>().text = LocalizationManager.Instance.GetText("app_name");
    }
}
