using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ArrowController : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 60);
    }

    void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(90, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")
            && !collision.gameObject.CompareTag("Arrow"))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(25);
        }
    }

    public static void Shoot(GameObject arrow, Vector3 direction, float force)
    {
        arrow.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
