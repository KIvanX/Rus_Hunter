using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf_controller : MonoBehaviour
{
    
    private UnityEngine.AI.NavMeshAgent AI_Agent;
    public GameObject Player;
    private Animator animator;
    public Image image;
    private bool is_attack = false;
    private float timeLeft = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        AI_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        image = GameObject.FindGameObjectWithTag("HP_bar").GetComponent<Image>();
    }
    
    void FixedUpdate()
    {
        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (Dist_Player > 1 && Dist_Player < 10) {
            AI_Agent.SetDestination(Player.transform.position);
            animator.SetFloat("speed", 1f);
        } else animator.SetFloat("speed", 0f);

        if (Dist_Player > 1) is_attack = false;
        else is_attack = true;

        animator.SetBool("Attack", is_attack);
        
        if (is_attack) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 1.3f;
                float playerHP = image.rectTransform.sizeDelta.x - 10f;
                image.rectTransform.sizeDelta = new Vector2(playerHP, 15);
                
                if (playerHP <= 10) Application.Quit();
            }
        }
    }

}
