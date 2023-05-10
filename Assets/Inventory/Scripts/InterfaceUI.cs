using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUI : MonoBehaviour
{
    public void Interact()
    {
        if (gameObject.activeSelf)
            Close();
        else
            Open();
    }

    private void Open()
    {
        gameObject.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Close()
    {
        gameObject.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
