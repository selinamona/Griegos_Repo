
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using Ink.UnityIntegration;
using System.Collections;

public class InkDialogueManager : MonoBehaviour
{
    public InkFile inkFile;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    public float typingSpeed = 0.04f;

    private Story story;
    private Coroutine typingCoroutine;
    private bool isTyping;

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
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.maxVisibleCharacters = int.MaxValue;
            isTyping = false;
            return;
        }

        if (story.canContinue)
        {
            string line = story.Continue();

            dialogueText.text = line;
            dialogueText.maxVisibleCharacters = 0;

            typingCoroutine = StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;

        for (int i = 0; i <= dialogueText.text.Length; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
