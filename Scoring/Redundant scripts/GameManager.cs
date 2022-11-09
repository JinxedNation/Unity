using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score;
    public List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //scoreText.text = "Score: " + score;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    //function ongoodclick UpdateScore(5) 

    //function onbadclick UpdateScore(-5) 
}

