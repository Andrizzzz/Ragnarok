using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelController : MonoBehaviour
{
    public GameObject panel;

    public void TogglePanel()
    {
        // Toggle the visibility of the panel
        panel.SetActive(!panel.activeSelf);
    }
}

