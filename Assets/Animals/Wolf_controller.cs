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
    private float timeLeft = 1.5f;
    private bool attacked = false;
    public int level = 1;
    private Controller playerController;

    void Start()
    {
        animator = GetComponent<Animator>();
        AI_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerController = Player.GetComponent<Controller>();

        HP = Random.Range(20, 50) + 5 * level;
    }

    void FixedUpdate()
    {
        if (HP <= 0) return;
        float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        float dist = 10 + DataHolder.complexity * 5;
        if (Dist_Player > 1 && (Dist_Player < (DataHolder.is_night ? dist * 1.5 : dist) || attacked))
        {
            AI_Agent.speed = DataHolder.is_night ? 4 + DataHolder.complexity * 3 : 7 + DataHolder.complexity * 3;
            AI_Agent.SetDestination(Player.transform.position);
            animator.SetFloat("speed", 1f);
        }
        else 
            animator.SetFloat("speed", 0f);

        is_attack = Dist_Player > 1.2 ? false : true;
        animator.SetBool("Attack", is_attack);
        
        if (is_attack) 
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = 1.5f;
                float damage = 10 + DataHolder.complexity * 5 + level;
                playerController.TakeDamage(DataHolder.is_night ? damage * 1.5f : damage);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        attacked = true;
        HP -= damage;
        if (HP <= 0) {
            GameObject wolf_meat = Meat;
            int amount = (int)(DataHolder.complexity * 3) + 1;
            wolf_meat.GetComponent<CollectableItem>().amount = DataHolder.is_night ? amount * 2 : amount;
            Instantiate(Meat, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 3.0f);
            AI_Agent.speed = 0;
            animator.Play("deathAnim");
            DataHolder.num_wolfs--;
            DataHolder.evolution_levels[DataHolder.evolution_wolfs] = level < 30 ? level + 1: level;
            DataHolder.evolution_wolfs++;
        }   
    }
}
