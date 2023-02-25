using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterTemplateView : MonoBehaviour
{
    private Text _letter;
    private Image _icon;
    private Button _button;

    private int _id;

    public int ID => _id;

    public void Initialize(string letter, Sprite sprite, int id, Color color)
    {
        _letter = transform.GetChild(1).GetComponent<Text>();
        _icon = GetComponent<Image>();
        _button = GetComponent<Button>();

        _letter.text = letter;
        _icon.sprite = sprite;
        _id = id;
        _letter.color = color;
    }

    public Button GetButton() { return _button; }
}
