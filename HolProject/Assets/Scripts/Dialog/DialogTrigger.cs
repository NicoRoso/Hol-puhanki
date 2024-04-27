using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogue;

    private void OnEnable()
    {
        DialogueChecker.OnTriggerd += TriggerDialogue;
    }

    private void OnDisable()
    {
        DialogueChecker.OnTriggerd -= TriggerDialogue;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);
        this.enabled = false;
    }
}
