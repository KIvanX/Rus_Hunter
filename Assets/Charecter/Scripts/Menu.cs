using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;

    public Slider complexitySlider;
    public Slider daySpeedSlider;

    public void Continue_game()
    {
        menu.SetActive(false);
    }

    public void New_game()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void update_complexity() 
    {
        DataHolder.set_complexity(complexitySlider.value);
    }

    public void update_day_speed() 
    {
        DataHolder.set_day_speed(daySpeedSlider.value);
    }
}
