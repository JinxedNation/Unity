using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class Timer
 * @brief Controls the timer seperately from scoring
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class Timer : MonoBehaviour
{
    [Tooltip("Level elapsed run time")]
    [SerializeField] private float levelRunTime = 0f;

    public GameObject TimerTextBox;

    // Start is called before the first frame update
    void Start()
    {
        levelRunTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        levelRunTime += Time.deltaTime;
        var timeSpan = System.TimeSpan.FromSeconds(levelRunTime);
        //TimerTextBox.GetComponent<Text>().text = timeSpan.ToString("'m'm 's's'");
        TimerTextBox.GetComponent<Text>().text = timeSpan.ToString("mm':'ss");
    }
}
