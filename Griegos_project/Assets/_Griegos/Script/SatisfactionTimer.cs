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

        // Count down normally
        currentTime -= Time.deltaTime;

        // Prevent going below zero
        if (currentTime <= 0)
        {
            currentTime = 0;
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

        Debug.Log("Satisfaction timer started!");
    }

    public void RemoveTime(float amount)
    {
        currentTime -= amount;

        if (currentTime < 0)
            currentTime = 0;

        UpdateBar();

        Debug.Log("Lost " + amount + " seconds!");
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

        // We can connect your game-ending result here later.
    }
}