using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GridBrushBase;

public class Controller : MonoBehaviour
{
    public float speed = 2,
        jump_power = 10,
        rotationSpeed = 0.4f,
        force = 15;

    public CharacterInput characterInput;

    public bool on_ground = false;
    private Animator animator;
    private Rigidbody rigid_body;
    private Vector3 moveDirection;
    private Vector3 rotationDirection;
    public Transform start_ray;
    public LayerMask not_player_mask;

    public Transform cameraTransform;
    public CharacterStatus characterStatus;

    public Transform arrowInHand;
    public GameObject arrowPrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            characterStatus.isSprinting = true;
            speed *= 2;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            characterStatus.isSprinting = false;
            speed /= 2;
        }

        if (!characterStatus.isSprinting)
        {
            if (Input.GetKey(KeyCode.W))
                transform.localPosition += speed * Time.deltaTime * transform.forward;
            if (Input.GetKey(KeyCode.S))
                transform.localPosition += speed * Time.deltaTime * -transform.forward;
            if (Input.GetKey(KeyCode.A))
                transform.localPosition += speed * Time.deltaTime * -transform.right;
            if (Input.GetKey(KeyCode.D))
                transform.localPosition += speed * Time.deltaTime * transform.right;

            if (Input.GetButtonDown("Fire2"))
                characterStatus.isAiming = true;
            if (Input.GetButtonUp("Fire2"))
                characterStatus.isAiming = false;
        }
        else
            if (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                transform.localPosition += speed * Time.deltaTime * transform.forward;

        if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask))
            animator.SetBool("Falling", false);
        else
            animator.SetBool("Falling", true);
            
        if (Input.GetButtonDown("Jump"))
            if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask)) {
                animator.SetTrigger("Jump");
                rigid_body.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
            }

        Physics.IgnoreCollision(GetComponent<Collider>(), arrowInHand.GetComponent<Collider>());
        if (characterStatus.isAiming && !characterStatus.isSprinting)
        {
            arrowInHand.gameObject.SetActive(true);
            if (Input.GetButtonUp("Fire1"))
            {
                GameObject newArrow = Instantiate(arrowPrefab, arrowInHand.position, arrowInHand.rotation);
                newArrow.GetComponent<Rigidbody>().isKinematic = false;
                newArrow.GetComponent<Rigidbody>().AddForce(arrowInHand.up * force, ForceMode.Impulse);
            }
        }
        else
            arrowInHand.gameObject.SetActive(false);

        UpdateMovement();
        UpdateAnimation();
        characterInput.InputUpdate();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("vertical", Input.GetAxis("Vertical"), 0.15f, Time.deltaTime);
        animator.SetFloat("horizontal", Input.GetAxis("Horizontal"), 0.15f, Time.deltaTime);
        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("isAiming", characterStatus.isAiming);
        animator.SetBool("isSprinting", characterStatus.isSprinting);
    }

    void UpdateMovement()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 moveDirection = cameraTransform.forward * vertical;
        moveDirection += cameraTransform.right * horizontal;
        moveDirection.Normalize();
        this.moveDirection = moveDirection;

        rotationDirection = cameraTransform.forward;
        RotationNormal();
    }

    void RotationNormal()
    {
        if (characterStatus.isSprinting)
        {
            rotationDirection = moveDirection;
        }

        Vector3 TargetDirection = rotationDirection;
        TargetDirection.y = 0;

        if (TargetDirection == Vector3.zero)
            TargetDirection = transform.forward;

        Quaternion lookDirection = Quaternion.LookRotation(TargetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed);
        transform.rotation = targetRotation;
    }
}
