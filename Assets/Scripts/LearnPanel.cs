using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnPanel : MonoBehaviour
{
    [SerializeField] private LetterTemplateViewController _letterTemplateViewController;
    [SerializeField] private LettersController _lettersController;
    [SerializeField] private Text _letter;
    [SerializeField] private Text _word;
    [SerializeField] private Image _image;
    [SerializeField] private Button _leftArrow;
    [SerializeField] private Button _rightArrow;
    [SerializeField] private Button _imageButton;

    private Animator _animator;
    private AudioSource _audioSource;
    private int _currentLetterId;

    private bool _isStartLearn = false;

    private void Awake()
    {
        LetterTemplateViewController.OnStartLearn.AddListener(OnStartLearn);
        _leftArrow.onClick.AddListener(OnLeftArrowClick);
        _rightArrow.onClick.AddListener(OnRightArrowClick);
        _imageButton.onClick.AddListener(OnImageClick);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        TryHideArrows();

    }

    private void Update()
    {
        if (_isStartLearn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (_currentLetterId < 1)
                    return;

                OnLeftArrowClick();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (_currentLetterId > _lettersController.LettersRU.Count - 2)
                    return;

                OnRightArrowClick();
            }
        }
    }

    private void OnStartLearn()
    {
        _isStartLearn = true;
        _animator.enabled = true;
        _currentLetterId = _letterTemplateViewController.ChoisenLetterID;
        ShowLetterInfo();
        StartCoroutine(SayAll());
    }

    private void ShowLetterInfo()
    {
        _letter.text = _lettersController.LettersRU[_currentLetterId].Name;
        _word.text = _lettersController.LettersRU[_currentLetterId].Word;
        _image.sprite = _lettersController.LettersRU[_currentLetterId].Icon;

        SetRandomColor();
        TryHideArrows();
    }

    private void OnLeftArrowClick()
    {
        _currentLetterId--;
        BaseClick();
    }

    private void OnRightArrowClick()
    {
        _currentLetterId++;
        BaseClick();
    }

    private void BaseClick()
    {
        StopAllCoroutines();
        ShowLetterInfo();
        StartCoroutine(SayAll());
    }

    private void SetRandomColor()
    {
        _letter.color = _letterTemplateViewController.GetRandomColor();
    }

    private void TryHideArrows()
    {
        if (_currentLetterId < 1)
            _leftArrow.gameObject.SetActive(false);
        else
            _leftArrow.gameObject.SetActive(true);

        if (_currentLetterId > _lettersController.LettersRU.Count - 2)
            _rightArrow.gameObject.SetActive(false);
        else
            _rightArrow.gameObject.SetActive(true);
    }

    private IEnumerator SayAll()
    {
        yield return new WaitForSeconds(1f);
        PlayLetter();

        yield return new WaitForSeconds(1f);
        PlayWord();
    }

    private IEnumerator SayWord()
    {
        yield return new WaitForSeconds(1f);
        PlayWord();
    }

    private void PlayLetter()
    {
        _audioSource.clip = _lettersController.LettersRU[_currentLetterId].SoundFull;
        _audioSource.Play();
    }

    private void PlayWord()
    {
        _audioSource.clip = _lettersController.LettersRU[_currentLetterId].SoundWord;
        _audioSource.Play();
    }

    private void OnImageClick()
    {
        StopAllCoroutines();
        PlayLetter();
        StartCoroutine(SayWord());
    }
}
