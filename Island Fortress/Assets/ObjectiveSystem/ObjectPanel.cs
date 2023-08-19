using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPanel : MonoBehaviour
{
    private List<Objective> objectives = new List<Objective>();
    private List<Objective> completedObjectives = new List<Objective>();

    public Animator animator;
    public Text ObjectiveText;

    public void AddObjective(Objective objective)
    {
        objectives.Add(objective);
    }

    public Objective[] GetObjectives()
    {
        return objectives.ToArray();
    }

    public void ObjectiveCompleted(Objective objective)
    {
        if (!objectives.Contains(objective))
        {
            return;
        }
        completedObjectives.Add(objective);
        objectives.Remove(objective);
    }

    public void ShowObjectives()
    {
        Objective[] objectives = GetObjectives();
        string objectiveText = "";
        if (objectives.Length == 0)
        {
            objectiveText = "No Objectives";
        }
        foreach (Objective objective in objectives)
        {
            objectiveText += objective.ObjectiveText + "\n";
        }
        ObjectiveText.text = objectiveText;
    }
    private void Update()
    {
        if (completedObjectives.Count > objectives.Count)
        {
            ObjectiveText.text = "All Objectives Completed" + "\n";
            ObjectiveText.text += "Go to the boat to finish the game";
        }
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

}
