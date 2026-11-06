
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using Ink.UnityIntegration;

public class InkDialogueManager : MonoBehaviour
{
    public InkFile inkFile;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    private Story story;

    void Awake()
    {
        story = new Story(inkFile.storyJson);

        dialoguePanel.SetActive(false);

        continueButton.onClick.AddListener(ContinueDialogue);
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        story = new Story(inkFile.storyJson);

        ContinueDialogue();
    }

    void ContinueDialogue()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
