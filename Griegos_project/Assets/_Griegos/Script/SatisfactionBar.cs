using UnityEngine;
using UnityEngine.UI;

public class SatisfactionBar : MonoBehaviour
{
    public Image satisfactionBar;

    public float totalTime = 60f;

    private float timer;

    void Start()
    {
        timer = totalTime;
        satisfactionBar.fillAmount = 1f;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
        }

        UpdateBar();
    }

    void UpdateBar()
    {
        if (timer > 45f)
        {
            satisfactionBar.fillAmount = 1f;
        }
        else if (timer > 30f)
        {
            satisfactionBar.fillAmount = 0.75f;
        }
        else if (timer > 15f)
        {
            satisfactionBar.fillAmount = 0.5f;
        }
        else if (timer > 0f)
        {
            satisfactionBar.fillAmount = 0.25f;
        }
        else
        {
            satisfactionBar.fillAmount = 0f;
        }
    }
}