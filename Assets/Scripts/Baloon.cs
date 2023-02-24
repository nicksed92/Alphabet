using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    [SerializeField] private List<Animator> animators = new List<Animator>();

    public void EnableAnimator()
    {
        foreach (var animator in animators)
        {
            animator.GetComponent<Animator>().enabled = true;
        }
    }

    public void DisableAnimator()
    {
        foreach (var animator in animators)
        {
            animator.GetComponent<Animator>().enabled = false;
        }
    }

    private void Start()
    {
        foreach (var animator in animators)
        {
            animator.GetComponent<Animator>().enabled = false;
        }
    }

}
