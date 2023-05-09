using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static float complexity = 0.5f;
    public static float day_speed = 0.9f;

    public static void set_complexity(float new_value) 
    {
        complexity = new_value;
    }

    public static void set_day_speed(float new_value) 
    {
        day_speed = new_value;
    }

}
