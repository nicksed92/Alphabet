using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersController : MonoBehaviour
{
    [SerializeField] private List<Letter> _lettersRu = new List<Letter>();
    [SerializeField] private List<Letter> _lettersEn = new List<Letter>();
    [SerializeField] private List<AudioClip> _incorrectRu = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _correctRu = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _incorrectEn = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _correctEn = new List<AudioClip>();

    public List<Letter> CurrentLetters
    {
        get
        {
            if (LocalizationManager.Instance.CurrentLanguage == Languages.DefaultLanguages.ru.ToString())
                return _lettersRu;
            else
                return _lettersEn;
        }
    }

    public List<AudioClip> CurrentCorrectPhrases
    {
        get
        {
            if (LocalizationManager.Instance.CurrentLanguage == Languages.DefaultLanguages.ru.ToString())
                return _correctRu;
            else
                return _correctEn;
        }
    }

    public List<AudioClip> CurrentInCorrectPhrases
    {
        get
        {
            if (LocalizationManager.Instance.CurrentLanguage == Languages.DefaultLanguages.ru.ToString())
                return _incorrectRu;
            else
                return _incorrectEn;
        }
    }
}
