using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanel : MonoBehaviour
{
    private List<Objective> objectives = new List<Objective>();
    public Animator animator;
    public Text ObjectiveText;

    public void AddObjective(Objective objective)
    {
        objectives.Add(objective);
    }

    public void ShowObjectives()
    {
        foreach (Objective objective in objectives)
        {
            if (ObjectiveText.text.Contains(objective.ObjectiveText))
            {
                continue;
            }
            StopAllCoroutines();
            StartCoroutine(TypeSentence(objective.ObjectiveText));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            animator.SetBool("Open", true);
            ShowObjectives();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            animator.SetBool("Open", false);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        ObjectiveText.text += sentence + "\n";
        yield return null;
    }

}
