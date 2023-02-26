using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersController : MonoBehaviour
{
    [SerializeField] private List<Letter> _lettersRu = new List<Letter>();
    [SerializeField] private List<Letter> _lettersEn = new List<Letter>();
    [SerializeField] private List<AudioClip> _incorrectRu = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _correctRu = new List<AudioClip>();
    [SerializeField] private List<Letter> _incorrectEn = new List<Letter>();

    public List<Letter> LettersRU => _lettersRu;
    public List<Letter> LettersEn => _lettersEn;
    public List<AudioClip> IncorrectRu => _incorrectRu;
    public List<AudioClip> CorrectRu => _correctRu;

}
