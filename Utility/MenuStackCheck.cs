using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStackCheck : MonoBehaviour
{
    public MenuManager pause;

    public MenuManager dialogue;

    public void OpenPauseMenu()
    {
        if (dialogue.IsOpen)
        {
            pause.Open(dialogue);
        }
        else
        {
            pause.Open();
        }
    }

    public void ClosePauseMenu()
    {
        pause.Close();
        Invoke(nameof(PauseCheck), 0.1f);
    }

    private void PauseCheck()
    {
        if (dialogue.IsOpen)
            TimeUtility.PauseTime();
    }
}