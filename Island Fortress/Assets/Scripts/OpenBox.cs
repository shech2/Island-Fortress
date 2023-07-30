using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.enabled = true;
        }
    }
}
