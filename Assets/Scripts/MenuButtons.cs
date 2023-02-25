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

    [SerializeField] private Animator _menuAnimator;

    public static UnityEvent OnLearn = new UnityEvent();
    public static UnityEvent OnPlay = new UnityEvent();

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayClick);
        _learnButton.onClick.AddListener(OnLearnClick);
    }

    private void OnLearnClick()
    {
        BaseClick();
        OnLearn.Invoke();
    }

    private void OnPlayClick()
    {
        BaseClick();
        OnPlay.Invoke();
    }

    private void BaseClick()
    {
        _menuAnimator.SetTrigger("Hide");
    }
}
