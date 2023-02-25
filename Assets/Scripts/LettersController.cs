using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersController : MonoBehaviour
{
    [SerializeField] private List<Letter> _lettersRu = new List<Letter>();
    [SerializeField] private List<Letter> _lettersEn = new List<Letter>();

    public List<Letter> LettersRU => _lettersRu;
    public List<Letter> LettersEn => _lettersEn;
}
