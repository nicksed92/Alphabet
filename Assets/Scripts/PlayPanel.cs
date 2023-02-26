using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private List<Button> _letterButtons;
    [SerializeField] private List<Button> _imageButtons;
    [SerializeField] private Image _image;
    [SerializeField] private Text _letter;
    [SerializeField] private LettersController _lettersController;
    [SerializeField] private LetterTemplateViewController _letterTemplateViewController;

    private Animator _animator;
    private AudioSource _audioSource;
    private List<Letter> randomLetters = new List<Letter>();
    private System.Random _random = new System.Random();

    private int _correctButtonIndex;
    private int _correctLetterIndex;
    private bool _isPanelOpen = false;

    private void Awake()
    {
        MenuButtons.OnPlay.AddListener(OnPlay);
        BackButton.OnClicked.AddListener(OnBackButtonClicked);

        for (int i = 0; i < _letterButtons.Count; i++)
        {
            _letterButtons[i].onClick.AddListener(OnAnswerClick);
        }

        for (int i = 0; i < _imageButtons.Count; i++)
        {
            _imageButtons[i].onClick.AddListener(OnAnswerClick);
        }

        _image.GetComponent<Button>().onClick.AddListener(() =>
        {
            StopAllCoroutines();
            SayWord();
        });

        _letter.GetComponent<Button>().onClick.AddListener(() =>
        {
            StopAllCoroutines();
            SayLetter();
        });
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnPlay()
    {
        if (_isPanelOpen)
            return;

        //Invoke(nameof(ChoiseRandomGame), 1f);
        ChoiseRandomGame();

        if (_animator.enabled == false)
            _animator.enabled = true;
        else
            _animator.SetTrigger("Show");

        _isPanelOpen = true;
    }

    private void OnBackButtonClicked()
    {
        if (_isPanelOpen == false)
            return;

        _animator.SetTrigger("Hide");
        StopAllCoroutines();

        _isPanelOpen = false;
    }

    private void ChoiseRandomGame()
    {
        int value = _random.Next(0, 2);
        randomLetters.Clear();

        _correctLetterIndex = _random.Next(0, _lettersController.CurrentLetters.Count);
        _image.sprite = _lettersController.CurrentLetters[_correctLetterIndex].Icon;
        randomLetters.Add(_lettersController.CurrentLetters[_correctLetterIndex]);
        _letter.text = _lettersController.CurrentLetters[_correctLetterIndex].Name;
        _letter.color = _letterTemplateViewController.GetRandomColor();

        for (int i = 0; i < 4; i++)
        {
            var randomElement = _lettersController.CurrentLetters.Where(x => !randomLetters.Contains(x)).OrderBy(x => _random.Next()).FirstOrDefault();
            randomLetters.Add(randomElement);
        }

        foreach (var button in _imageButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (var button in _letterButtons)
        {
            button.gameObject.SetActive(false);
        }

        _image.enabled = false;
        _letter.enabled = false;

        _correctButtonIndex = _random.Next(0, _letterButtons.Count);

        SetButtonsText(randomLetters);
        SetButtonsImage(randomLetters);

        if (value == 0)
        {
            _letter.enabled = true;

            foreach (var button in _imageButtons)
            {
                button.gameObject.SetActive(true);
            }

            if (_isPanelOpen == false)
                StartCoroutine(SayCorrectLetter(2f));
            else
                StartCoroutine(SayCorrectLetter(1f));

        }
        else
        {
            _image.enabled = true;

            foreach (var button in _letterButtons)
            {
                button.gameObject.SetActive(true);
            }

            if (_isPanelOpen == false)
                StartCoroutine(SayCorrectWord(2f));
            else
                StartCoroutine(SayCorrectWord(1f));
        }
    }

    private void SetButtonsText(List<Letter> letters)
    {
        for (int i = 0; i < _letterButtons.Count; i++)
        {
            var text = _letterButtons[i].transform.GetChild(1).GetComponent<Text>();
            text.color = _letterTemplateViewController.GetRandomColor();
            text.text = letters[i + 1].Name;
        }

        _letterButtons[_correctButtonIndex].transform.GetChild(1).GetComponent<Text>().text = letters[0].Name;
    }

    private void SetButtonsImage(List<Letter> letters)
    {
        for (int i = 0; i < _imageButtons.Count; i++)
        {
            _imageButtons[i].GetComponent<Image>().sprite = letters[i + 1].Icon;
        }

        _imageButtons[_correctButtonIndex].GetComponent<Image>().sprite = letters[0].Icon;
    }

    private IEnumerator SayCorrectLetter(float delay)
    {
        yield return new WaitForSeconds(delay);
        SayLetter();
    }

    private IEnumerator SayCorrectWord(float delay)
    {
        yield return new WaitForSeconds(delay);
        SayWord();
    }

    private void SayLetter()
    {
        _audioSource.clip = randomLetters[0].SoundFull;
        _audioSource.Play();
    }

    private void SayWord()
    {
        _audioSource.clip = randomLetters[0].SoundWord;
        _audioSource.Play();
    }

    private void OnAnswerClick()
    {
        StopAllCoroutines();

        string name = EventSystem.current.currentSelectedGameObject.name;
        int value = Int32.Parse(name);

        if (_correctButtonIndex == value)
        {
            int index = _random.Next(0, _lettersController.CurrentCorrectPhrases.Count);
            _audioSource.clip = _lettersController.CurrentCorrectPhrases[index];
            _audioSource.Play();
            Invoke(nameof(ChoiseRandomGame), 1f);
        }
        else
        {
            int index = _random.Next(0, _lettersController.CurrentInCorrectPhrases.Count);
            _audioSource.clip = _lettersController.CurrentInCorrectPhrases[index];
            _audioSource.Play();
        }

    }
}