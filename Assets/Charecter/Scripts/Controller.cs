using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public float
        walkSpeed = 2.5f,
        sprintSpeed = 5,
        crouchSpeed = 1.5f,
        jump_power = 10,
        rotationSpeed = 0.4f,
        force = 20;
    private float HP = 100;
    private float speed;

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

    public UnityEvent<float> OnHPUpdate;
    public UnityEvent OnInventoryOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody>();
        speed = walkSpeed;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region Sprint Control
        if (Input.GetButtonDown("Sprint") && !characterStatus.isAiming && !characterStatus.isCrouching)
        {
            characterStatus.isSprinting = !characterStatus.isSprinting;
            if (characterStatus.isSprinting)
                speed = sprintSpeed;
            else
                speed = walkSpeed;
        }
        #endregion

        #region Movement Control
        if (!characterStatus.isSprinting || characterStatus.isCrouching)
        {
            if (Input.GetKey(KeyCode.W))
                transform.localPosition += speed * Time.deltaTime * transform.forward;
            if (Input.GetKey(KeyCode.S))
                transform.localPosition += speed * Time.deltaTime * -transform.forward;
            if (Input.GetKey(KeyCode.A))
                transform.localPosition += speed * Time.deltaTime * -transform.right;
            if (Input.GetKey(KeyCode.D))
                transform.localPosition += speed * Time.deltaTime * transform.right;

            if (Input.GetButtonDown("Aim"))
                characterStatus.isAiming = true;
            if (Input.GetButtonUp("Aim"))
                characterStatus.isAiming = false;
        }
        else
            if (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            transform.localPosition += speed * Time.deltaTime * transform.forward;
        #endregion

        #region Falling
        if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask))
            animator.SetBool("Falling", false);
        else
            animator.SetBool("Falling", true);

        if (Input.GetButtonDown("Jump"))
            if (Physics.CheckSphere(start_ray.position, 0.3f, not_player_mask))
            {
                animator.SetTrigger("Jump");
                rigid_body.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
            }
        #endregion

        #region Shooting Control
        if (characterStatus.isAiming)
        {
            if (characterStatus.isSprinting)
            {
                characterStatus.isSprinting = false;
                speed = walkSpeed;
            }

            arrowInHand.gameObject.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
                characterStatus.isStretching = true;
            if (Input.GetButtonUp("Fire1"))
            {
                GameObject newArrow = Instantiate(arrowPrefab, arrowInHand.position, arrowInHand.rotation);
                ArrowController.Shoot(newArrow, arrowInHand.up, force * animator.GetFloat("drawForce"));
                characterStatus.isStretching = false;
            }
        }
        else
            arrowInHand.gameObject.SetActive(false);
        #endregion

        #region Crouch
        if (Input.GetButtonDown("Crouch"))
        {
            characterStatus.isCrouching = true;
            speed = crouchSpeed;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            characterStatus.isCrouching = false;
            speed = GetCurrentSpeed();
        }
        #endregion

        #region Inventory
        if (Input.GetButtonDown("Inventory"))
            OnInventoryOpen.Invoke();
        #endregion

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
        animator.SetBool("isCrouching", characterStatus.isCrouching);
        animator.SetBool("isStretching", characterStatus.isStretching);
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
        RotateNormal();
    }

    void RotateNormal()
    {
        if (characterStatus.isSprinting && !characterStatus.isCrouching)
            rotationDirection = moveDirection;

        Vector3 TargetDirection = rotationDirection;
        TargetDirection.y = 0;

        if (TargetDirection == Vector3.zero)
            TargetDirection = transform.forward;

        Quaternion lookDirection = Quaternion.LookRotation(TargetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed);
        transform.rotation = targetRotation;
    }

    float GetCurrentSpeed()
    {
        if (characterStatus.isSprinting)
            return sprintSpeed;
        else
            return walkSpeed;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        OnHPUpdate.Invoke(HP);

        if (HP <= 0)
            Application.Quit();
    }
}
