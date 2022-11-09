using UnityEngine;
using UnityEngine.Events;

// Create custom event when some arguments needs to be passed with it
// Skip this if argument passing is not required and use UnityEvent instead
[System.Serializable]
/*
public class MyEvent : UnityEvent<string>
{
}*/

public class MyEvent : UnityEvent<int>
{
}

public class UnityEventRaiseBehaviour : MonoBehaviour
{
    // Define event
    //public MyEvent TestEvent;
    public MyEvent ScoreEvent;
    public int pointsValue;

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // invoke event
            //TestEvent.Invoke("Some Value");
        }
        */
    }

    private void OnMouseDown()
    {


        Debug.Log("Mouse down");
        // score 5 points
        ScoreEvent.Invoke(pointsValue);

        //gameManager.UpdateScore(pointValue);
    }
}
