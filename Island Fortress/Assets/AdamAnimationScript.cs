using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdamAnimationScript : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetFloat("speed", 1);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("speed", 0);
        }
        // left shift
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 2);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("speed", -1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("speed", 0);
        }

    }
}
