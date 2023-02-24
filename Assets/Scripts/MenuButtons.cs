using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _learnButton;
    [SerializeField] private Button _languageRuButton;
    [SerializeField] private Button _languageEnButton;
    [SerializeField] private Button _ratingButton;

    [SerializeField] private Animator _menuAnimator;

    private void Awake()
    {
        _playButton.onClick.AddListener(BaseClick);
        _learnButton.onClick.AddListener(BaseClick);
    }

    private void BaseClick()
    {
        _menuAnimator.SetTrigger("Hide");
    }
}
