using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_controller : MonoBehaviour
{
    
    private UnityEngine.AI.NavMeshAgent AI_Agent;
    public GameObject Player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        AI_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (Dist_Player > 1 && Dist_Player < 10) {
            AI_Agent.SetDestination(Player.transform.position);
            animator.SetFloat("speed", 1f);
        } else animator.SetFloat("speed", 0f);

        if (Dist_Player > 1) animator.SetBool("Attack", false);
        else animator.SetBool("Attack", true);
    }

}
