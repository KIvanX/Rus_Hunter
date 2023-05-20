using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static float complexity { get; set; } = 0.5f;
    public static float day_speed { get; set; } = 0.5f;
    public static int num_wolfs { get; set; } = 0;
    public static int num_resurces { get; set; } = 0;
    public static bool is_night { get; set; } = false;

}
