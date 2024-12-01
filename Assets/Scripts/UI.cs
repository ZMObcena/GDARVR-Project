using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void OnExitButtonClicked()
    {
        Debug.Log("Exit Button Pressed");
        Application.Quit();
    }
}
