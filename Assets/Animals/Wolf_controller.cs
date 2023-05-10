using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf_controller : MonoBehaviour, IEnemy
{
    public float HP { get; set; } = 50;
    private UnityEngine.AI.NavMeshAgent AI_Agent;
    private GameObject Player;
    private Animator animator;
    private bool is_attack = false;
    private float timeLeft = 0f;
    private Controller playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        AI_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<Controller>();
    }

    void FixedUpdate()
    {
        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (Dist_Player > 1 && Dist_Player < 10)
        {
            AI_Agent.SetDestination(Player.transform.position);
            animator.SetFloat("speed", 1f);
        }
        else 
            animator.SetFloat("speed", 0f);

        if (Dist_Player > 1)
            is_attack = false;
        else 
            is_attack = true;
        animator.SetBool("Attack", is_attack);
        
        if (is_attack) 
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 1.3f;
                playerController.TakeDamage(10f);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
            Destroy(gameObject);
    }
}
