using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnPanel : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        LetterTemplateViewController.OnStartLearn.AddListener(OnStartLearn);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnStartLearn()
    {
        _animator.enabled = true;
    }
}
