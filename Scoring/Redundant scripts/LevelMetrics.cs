using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelMetrics : MonoBehaviour
{

    [Tooltip("Assign a Number to this level")]
    public int levelNumber;
    public int score { get; set; }

    private float timeLeft;
    [Tooltip("Timer counts down to 0 from this value. In seconds")]
    public float countDown;
    private float runTime;

    public UnityEvent timeOutEvent;


    // Start is called before the first frame update
    void Start()
    {
        if (timeOutEvent == null)
            timeOutEvent = new UnityEvent();


        timeLeft = countDown;
        runTime = 0.0f;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountDownTimer();
        UpdateRunTimer();
    }

    public void clearLevel()
    {
        Start();
    }

    void UpdateCountDownTimer()
    {

        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft < 0)
        {
            Debug.Log("Completed");
            timeOutEvent.Invoke();
        }
    }
    void UpdateRunTimer()
    {
        runTime += Time.deltaTime;
    }

    public string getTimeLeft()
    {
        double b = System.Math.Round(timeLeft, 2);
        return b.ToString();
    }
    public string getRunTime()
    {
        double b = System.Math.Round(runTime, 2);
        return b.ToString();
    }

}
