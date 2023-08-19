using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ObjectiveManager : MonoBehaviour
{
    public Animator animator;
    public Text dialogueText;
    public ObjectPanel objectPanel;

    public float waitTime = 5f;

    public void StartObjective(Objective objective)
    {
        objectPanel.AddObjective(objective);
        animator.SetBool("Open", true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(objective.ObjectiveText));
    }

    public void CompleteObjective(Objective objective)
    {
        objectPanel.ObjectiveCompleted(objective);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(objective.ObjectiveText + " Completed!"));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("Open", false);
    }
}
