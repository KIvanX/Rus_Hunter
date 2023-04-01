using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    public CharacterStatus characterStatus;
    public Transform targetLook;

    public Vector3 aimingPosition;

    private Transform leftHand;
    private Transform rightHand;

    private Transform shoulder;
    private Transform rightHandBone;
    private Transform aimPivot;

    private Animator animator;

    private float weight;
    private float bodyWeight;
    private float headWeight;
    private float rightHandWeight;
    private float leftHandWeight;

    void Start()
    {
        animator = GetComponent<Animator>();

        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;
        rightHandBone = animator.GetBoneTransform(HumanBodyBones.RightHand).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "Aim Pivot";
        aimPivot.transform.parent = transform;

        rightHand = new GameObject().transform;
        rightHand.name = "Right Hand";
        rightHand.transform.parent = aimPivot;

        leftHand = new GameObject().transform;
        leftHand.name = "Left Hand";
        leftHand.transform.parent = rightHand;
    }

    void Update()
    {
        aimPivot.position = shoulder.position;
        leftHand.localPosition = aimingPosition;
        rightHand.position = rightHandBone.position;

        if (characterStatus.isAiming && !characterStatus.isSprinting)
        {
            weight = Mathf.MoveTowards(weight, 1, Time.deltaTime);
            bodyWeight = Mathf.MoveTowards(bodyWeight, 1, Time.deltaTime);
            headWeight = Mathf.MoveTowards(headWeight, 1, Time.deltaTime);
            rightHandWeight = Mathf.MoveTowards(rightHandWeight, 1, Time.deltaTime * 4);
            leftHandWeight = Mathf.MoveTowards(leftHandWeight, 1, Time.deltaTime);
        }
        else
        {
            weight = Mathf.MoveTowards(weight, 0, Time.deltaTime * 4);
            bodyWeight = Mathf.MoveTowards(bodyWeight, 0, Time.deltaTime * 4);
            headWeight = Mathf.MoveTowards(headWeight, 0, Time.deltaTime * 4);
            rightHandWeight = Mathf.MoveTowards(rightHandWeight, 0, Time.deltaTime * 4);
            leftHandWeight = Mathf.MoveTowards(leftHandWeight, 0, Time.deltaTime * 4);
        }
    }

    private void OnAnimatorIK()
    {
        aimPivot.LookAt(targetLook);
        rightHand.LookAt(targetLook);

        if (targetLook != null)
        {
            animator.SetLookAtWeight(weight, bodyWeight, headWeight);
            animator.SetLookAtPosition(targetLook.position);
        }
        animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(targetLook.position));

        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation * Quaternion.Euler(0, 0, -90));

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
    }
}
