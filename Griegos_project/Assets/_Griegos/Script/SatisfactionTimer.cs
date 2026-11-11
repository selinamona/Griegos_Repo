using UnityEngine;
using UnityEngine.UI;

public class SatisfactionTimer : MonoBehaviour
{
    public float maxTime = 120f;
    public float currentTime;

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
        currentTime -= amount;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;
            TimerFinished();
        }

        UpdateBar();
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
        Debug.Log("Time ran out!");
    }

    public void ChangeTime(float amount)
    {
        currentTime += amount;

        // Keep the timer between 0 and maxTime
        currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

        UpdateBar();

        if (currentTime <= 0f)
        {
            timerRunning = false;
            TimerFinished();
        }
    }
}
