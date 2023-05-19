using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    
    private bool healing = false;
    private GameObject Player;
    private Controller playerController;

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
        if (other.gameObject.name == "door");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "healing_place") healing = false;
    }
}
