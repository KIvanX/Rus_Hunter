using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sale_loot : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void sale_meat() 
    {
        var inventory = player.GetComponent<Inventory>();
            
        if (inventory.UseItem(2)) 
        {
            DataHolder.update_coins(DataHolder.coins + 1);
        }
    }
}
