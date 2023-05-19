using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public GameObject main;

    public Slider complexitySlider;
    public Slider daySpeedSlider;

    public UnityEvent OnMenuOpen;
    public UnityEvent OnMenuClose;

    public void Interact()
    {
        if (menu.activeSelf)
            Continue_game();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        menu.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        OnMenuOpen.Invoke();
    }

    public void Continue_game()
    {
        menu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        OnMenuClose.Invoke();
    }

    public void New_game()
    {
        Continue_game();
        SceneManager.LoadScene(0);
    }

    public void Settings()
    {
        settings.SetActive(true);
        complexitySlider.value = DataHolder.complexity;
        daySpeedSlider.value = DataHolder.day_speed;
        main.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void update_complexity() 
    {
        DataHolder.complexity = complexitySlider.value;
    }

    public void update_day_speed() 
    {
        DataHolder.day_speed = daySpeedSlider.value;
    }
}
