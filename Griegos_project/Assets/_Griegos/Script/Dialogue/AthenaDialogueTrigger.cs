
using UnityEngine;

public class AthenaDialogueTrigger : MonoBehaviour
{
    public InkDialogueManager dialogueManager;

    void OnMouseDown()
    {
        dialogueManager.StartDialogue();
    }
}
