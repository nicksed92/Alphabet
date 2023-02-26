using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button _button;
    private Animator _animator;

    public static UnityEvent OnClicked = new UnityEvent();

    private void Awake()
    {
        MenuButtons.OnLearn.AddListener(ShowButton);
        MenuButtons.OnPlay.AddListener(ShowButton);
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _animator = GetComponent<Animator>();
    }

    private void OnClick()
    {
        _animator.SetTrigger("Hide");
        OnClicked.Invoke();
    }

    private void ShowButton()
    {
        if (_animator.enabled == false)
            _animator.enabled = true;
        else
            _animator.SetTrigger("Show");
    }
}
