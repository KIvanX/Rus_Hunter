using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceUI : MonoBehaviour
{
    public void UpdateUI()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
