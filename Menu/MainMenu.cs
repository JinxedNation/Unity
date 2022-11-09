using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [Tooltip("The initial button to highlight when a gamepad is in use.")]
    [SerializeField] private GameObject initialButton;

    // Start is called before the first frame update
    void Start()
    {
        ControllerUtility.SelectGameObject(initialButton);
    }
}
