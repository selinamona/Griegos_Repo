using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RetryManager : MonoBehaviour
{
    public GameObject resultPanel;
    public TMP_Text resultText;

    public void ShowResult(string result)
    {
        resultPanel.SetActive(true);

        if (result == "Success")
        {
            resultText.text = "You're a great cook <3";
        }
        else if (result == "Mediocre")
        {
            resultText.text = ". . . ( - 0 - ) ";
        }
        else if (result == "Fail")
        {
            resultText.text = "YOU SUCK o-o";
        }
    }

    public void Retry()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}