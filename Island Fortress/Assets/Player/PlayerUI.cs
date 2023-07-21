using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;


    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void ShowInteractText(string promptMessage)
    {
        promptText.text = promptMessage;
        promptText.enabled = true;
    }

    public void HideInteractText()
    {
        promptText.enabled = false;
    }


}
