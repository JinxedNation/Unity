using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAttempt : MonoBehaviour
{
    public SaveMetrics save;
    public GameObject level;
    public RunMetrics run;
    public StabilityManager stabilityManager;

    // Start is called before the first frame update
    void Start()
    {
        run = level.GetComponent<RunMetrics>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        save.SaveRunMetrics();
        float score = run.levelScore;
        stabilityManager.AddToStabilityScore(score);
    }
}
