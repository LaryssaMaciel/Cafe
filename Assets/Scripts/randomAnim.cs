using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomAnim : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("dog", 0, Random.value);
    }
}
