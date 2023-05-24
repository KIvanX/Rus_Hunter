using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class ArrowController : MonoBehaviour
{
    private Rigidbody _rb;
    private float _damage = 15;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 60);
    }

    void FixedUpdate()
    {
        if (_rb.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_rb.velocity) * Quaternion.Euler(90, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")
            && !collision.gameObject.CompareTag("Arrow"))
        {
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.parent = collision.gameObject.transform;
            collision.gameObject.GetComponent<IEnemy>().TakeDamage(_damage);
        }
    }

    public static void Shoot(GameObject arrow, Vector3 direction, float force, float damage)
    {
        arrow.GetComponent<ArrowController>()._damage = damage;
        arrow.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }
}
