using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    
    private bool healing = false;
    private GameObject Player;
    private Controller playerController;
    public GameObject sale_loot;

    private void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<Controller>();
    }

    void Update()
    {
        if (healing) {
            playerController.TakeHeal(0.1f);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "healing_place") healing = true;
        if (other.gameObject.name == "door") 
        {
            sale_loot.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "healing_place") healing = false;
        if (other.gameObject.name == "door") {
            sale_loot.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
