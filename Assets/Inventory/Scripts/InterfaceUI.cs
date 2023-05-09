using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUI : MonoBehaviour
{
    public void OpenUI()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gameObject.SetActive(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
