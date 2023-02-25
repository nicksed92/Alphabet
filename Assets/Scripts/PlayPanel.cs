using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel : MonoBehaviour
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;
    [SerializeField] private Button _button3;
    [SerializeField] private Button _button4;
    [SerializeField] private Image _image;
    [SerializeField] private Text _letter;
    [SerializeField] private LettersController _lettersController;

    private Animator _animator;

    private void Awake()
    {
        MenuButtons.OnPlay.AddListener(OnPlay);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnPlay()
    {
        _animator.enabled = true;
    }

   /* private void ChoiseRandomGame()
    {
        int value = 0;

        value = Random.Range(0, 2);

        if (value == 0)
        {
            int correctIndex = Random.Range(0, _lettersController.LettersRU.Count);
            _image.sprite = _lettersController.LettersRU[correctIndex].Icon;

            for (int i = 0; i < 3; i++)
            {
                while()
            }
        }
    }

    private void GetRandomLetterIndex()
    {

    }*/
}
