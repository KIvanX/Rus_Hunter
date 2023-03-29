using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int speed = 4, jump_power = 10;
    public bool on_ground = false, run = false;
    private Animator animator;
    private Rigidbody rigid_body;
    public Transform start_ray;
    public LayerMask not_player_mask;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("speed", Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"));

        if (Input.GetKey(KeyCode.W)) 
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) 
            transform.localPosition += -transform.forward * speed * Time.deltaTime / 2;
        if (Input.GetKey(KeyCode.A)) 
            transform.localPosition += -transform.right * speed * Time.deltaTime / 2;
        if (Input.GetKey(KeyCode.D)) 
            transform.localPosition += transform.right * speed * Time.deltaTime / 2;
        
        if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask)) animator.SetBool("Falling", false);
        else animator.SetBool("Falling", true);
            
        if (Input.GetKeyDown(KeyCode.Space))
            if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask)) {
                animator.SetTrigger("Jump");
                rigid_body.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
            }
                
    }

}
