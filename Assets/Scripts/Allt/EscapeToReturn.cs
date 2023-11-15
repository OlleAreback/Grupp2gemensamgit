using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToReturn : MonoBehaviour
{
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject SettingsCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsCanvas.activeSelf == true)
            {
                SettingsCanvas.SetActive(false);
                MenuCanvas.SetActive(true);
            }
        }
    }
}