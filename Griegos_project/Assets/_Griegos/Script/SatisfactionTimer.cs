
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SatisfactionTimer : MonoBehaviour
{
    public float maxTime = 120f;
    public float currentTime;
    public GameObject retryButton;

    public Image satisfactionBar;

    private bool timerRunning = false;

    void Start()
    {
        currentTime = maxTime;
        UpdateBar();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;
            TimerFinished();
        }

        UpdateBar();
    }

    public void StartTimer()
    {
        currentTime = maxTime;
        timerRunning = true;
        UpdateBar();
    }

    public void RemoveTime(float amount)
    {
        ChangeTime(-amount);
    }

    public void ChangeTime(float amount)
    {
        currentTime += amount;

        currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

        UpdateBar();

        if (currentTime <= 0f)
        {
            timerRunning = false;
            TimerFinished();
        }
    }

    // Athena's food preferences
    public void ApplyAthenaPreference(string dishName)
    {
        if (dishName == "Salad")
        {
            ChangeTime(20f);
            Debug.Log("Athena loves the salad! +20 satisfaction");
        }
        else if (dishName == "Meatballs" || dishName == "Fish")
        {
            ChangeTime(-20f);
            Debug.Log("Athena dislikes this dish! -20 satisfaction");
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    void UpdateBar()
    {
        if (satisfactionBar != null)
        {
            satisfactionBar.fillAmount = currentTime / maxTime;
        }
    }

    void TimerFinished()
    {
        Debug.Log("Satisfaction reached zero!");

        if (retryButton != null)
        {
            retryButton.SetActive(true);
        }
    }

    // Restart the current scene
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}