using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    private float mouse_X, mouse_Y, mouse_sens = 5f;
    private bool attack = false;
    public Transform player;
    public Camera MainCamera;
    public Camera AimCamera;
    public Animator player_animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        player_animator.SetBool("attack", attack);

        mouse_X = Input.GetAxis("Mouse X") * mouse_sens;
        mouse_Y = Input.GetAxis("Mouse Y") * mouse_sens;

        player.Rotate(mouse_X * new Vector3(0, 1, 0));
        transform.Rotate(-mouse_Y * new Vector3(1, 0, 0));
    }
}
