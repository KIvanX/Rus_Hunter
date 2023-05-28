using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHolder : MonoBehaviour
{
    public static float complexity { get; set; } = 0.5f;
    public static float day_speed { get; set; } = 0.5f;
    public static int num_wolfs { get; set; } = 0;
    public static int num_resurces { get; set; } = 0;
    public static bool is_night { get; set; } = false;
    public static int coins { get; set; } = 0;
    public static int evolution_wolfs = 0;
    public static int[] evolution_levels = new int[10];

    public static void update_coins(int new_value) {
        coins = new_value;
        GameObject coins_text = GameObject.Find("Coins_text");
        coins_text.GetComponent<TMPro.TextMeshProUGUI>().text = new_value.ToString();
    }

}
