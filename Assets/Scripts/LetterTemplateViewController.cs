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

    public Color GetRandomColor()
    {
        return _lettersColor[Random.Range(0, _lettersColor.Count)];
    }

    private void Awake()
    {
        MenuButtons.OnLearn.AddListener(OnLearnChoise);
    }

    private void Start()
    {
        CreateAlphabet();
    }

    private void CreateAlphabet()
    {
        for (int i = 0; i < _lettersController.LettersRU.Count; i++)
        {
            var clone = Instantiate(_letterTemplateView, _container);
            clone.Initialize(_lettersController.LettersRU[i].Name, _lettersController.LettersRU[i].Icon, i, GetRandomColor());
            var button = clone.GetButton();
            button.onClick.AddListener(delegate { OnLetterClick(clone.ID); });
        }
    }

    private void OnLetterClick(int buttonID)
    {
        ChoisenLetterID = buttonID;
        _animator.SetTrigger("Hide");
        OnStartLearn.Invoke();
    }

    private void OnLearnChoise()
    {
        _animator.enabled = true;
    }

 
}
