using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveManager : MonoBehaviour
{
    public Animator animator;
    public Text dialogueText;
    public void StartObjective(Dialogue objective)
    {
        animator.SetBool("Open", true);
        StartCoroutine(TypeSentence(objective.sentences[0]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }



}
