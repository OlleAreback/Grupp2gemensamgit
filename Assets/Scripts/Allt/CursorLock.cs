using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorLock : MonoBehaviour
{
    public void CursorLockEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorLockDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}