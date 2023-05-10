using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InterfaceManagement : MonoBehaviour
{
    public UnityEvent OnInventoryInteraction;
    public UnityEvent OnMenuInteraction;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            OnMenuInteraction.Invoke();

        if (Input.GetButtonDown("Inventory"))
            OnInventoryInteraction.Invoke();
    }

    public void Pause() => Time.timeScale = 0;
    
    public void UnPause() => Time.timeScale = 1.0f;
}
