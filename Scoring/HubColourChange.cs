using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class HubColourChange
 * @brief Changes particle system colouring in line with environmental stability score.
 *
 * @author Michael John
 * @version 01
 * @date 31/03/2022
 *
 */
public class HubColourChange : MonoBehaviour
{

    private GameObject hubObj;
    private HubManager hubManager;

    private GameObject stabSystem;
    private EnvironmentalDecay decaySystem;

    [Tooltip("Hub particles show this colour on low stability score")]
    public Color lowStabilityColor = Color.red;
    [Tooltip("Hub particles show this colour on high stability score")]
    public Color highStabilityColor = Color.green;
    private Color lerpedColor = Color.white;
    public float t;

    // Start is called before the first frame update
    void Start()
    {

        hubObj = GameObject.Find("HubManager");
        hubManager = hubObj.GetComponent<HubManager>();
        stabSystem = GameObject.Find("StabilitySystem");
        decaySystem = stabSystem.GetComponent<EnvironmentalDecay>();
    }

    // Update is called once per frame
    void Update()
    {
        t = decaySystem.stabScore;

        lerpedColor = Color.Lerp(lowStabilityColor, highStabilityColor, t);

        hubManager.setColour(lerpedColor); // (orange-ish), can use Color.red, Color.green, Color.yellow, etc..
    }
}
