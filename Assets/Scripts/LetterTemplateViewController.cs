using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LetterTemplateViewController : MonoBehaviour
{
    [SerializeField] private LettersController _lettersController;
    [SerializeField] private LetterTemplateView _letterTemplateView;
    [SerializeField] private Transform _container;
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Color> _lettersColor = new List<Color>();

    public static UnityEvent OnStartLearn = new UnityEvent();

    public int ChoisenLetterID { get; private set; }

    private bool _isPanelOpen = false;

    public Color GetRandomColor()
    {
        return _lettersColor[Random.Range(0, _lettersColor.Count)];
    }

    private void Awake()
    {
        MenuButtons.OnLearn.AddListener(OnLearnChoise);
        BackButton.OnClicked.AddListener(HidePanel);
        LocalizationManager.OnLanguageChanged.AddListener(CreateAlphabet);
    }

    private void Start()
    {
        CreateAlphabet();
    }

    private void CreateAlphabet()
    {
        foreach (Transform t in _container)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < _lettersController.CurrentLetters.Count; i++)
        {
            var clone = Instantiate(_letterTemplateView, _container);
            clone.Initialize(_lettersController.CurrentLetters[i].Name, _lettersController.CurrentLetters[i].Icon, i, GetRandomColor());
            var button = clone.GetButton();
            button.onClick.AddListener(delegate { OnLetterClick(clone.ID); });
        }
    }

    private void OnLetterClick(int buttonID)
    {
        ChoisenLetterID = buttonID;
        HidePanel();
        OnStartLearn.Invoke();
    }

    private void OnLearnChoise()
    {
        if (_isPanelOpen)
            return;

        if (_animator.enabled == false)
            _animator.enabled = true;
        else
            _animator.SetTrigger("Show");

        _isPanelOpen = true;
    }

    private void HidePanel()
    {
        if (_isPanelOpen == false)
            return;

        _animator.SetTrigger("Hide");
        _isPanelOpen = false;
    }
}
