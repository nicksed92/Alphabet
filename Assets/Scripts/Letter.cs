using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "ScriptableObjects/Alphabet", order = 1)]
public class Letter : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _word;
    [SerializeField] private AudioClip _soundFull;
    [SerializeField] private AudioClip _soundWord;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public string Word => _word;
    public AudioClip SoundFull => _soundFull;
    public AudioClip SoundWord => _soundWord;
    public Sprite Icon => _icon;

}
