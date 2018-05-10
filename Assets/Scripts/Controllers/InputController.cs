using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    MouseMode currentMode = MouseMode.Select;

    public void StartBuildMode()
    {
        currentMode = MouseMode.Build;
    }
}
