using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf_controller : MonoBehaviour, IEnemy
{
    public float HP { get; set; }
    public GameObject Meat;
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

        HP = Random.Range(45, 75);
    }

    void FixedUpdate()
    {
        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        float dist = 8 + DataHolder.complexity * 10;
        if (Dist_Player > 1 && Dist_Player < (DataHolder.is_night ? dist * 1.5 : dist))
        {
            AI_Agent.speed = 3 + DataHolder.complexity * 3;
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
                float damage = 5 + DataHolder.complexity * 10;
                playerController.TakeDamage(DataHolder.is_night ? damage * 2 : damage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0) {
            GameObject wolf_meat = Meat;
            int amount = (int)(DataHolder.complexity * 3) + 1;
            wolf_meat.GetComponent<CollectableItem>().amount = DataHolder.is_night ? amount * 2 : amount;
            Instantiate(Meat, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            DataHolder.num_wolfs--;
        }   
    }
}
