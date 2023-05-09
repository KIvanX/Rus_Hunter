using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform pivot;
    public Transform character;
    public Transform mainTranform;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool isLeftPivot;
    public bool isInventoryOpen = false;
    public float delta;
    public GameObject menu; 

    private float mouseX;
    private float mouseY;
    private float smoothX;
    private float smoothY;
    private float smoothXVelocity;
    private float smoothYVelocity;
    private float lookAngle;
    private float titleAngle;

    private void Update()
    {
        if (!menu.activeInHierarchy) FixedTick();
    }

    public void OnInventoryOpen()
    {
        if (enabled)
        {
            enabled = false;
            transform.rotation = character.rotation;
        }
        else
            enabled = true;
    }

    void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mainTranform.position, character.position, 1);
        mainTranform.position = targetPosition;
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        if (characterStatus.isAiming && !characterStatus.isSprinting)
        {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
        }

        if (characterStatus.isCrouching)
            targetY = cameraConfig.aimY;

        if (isLeftPivot)
            targetX = -targetX;

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = cameraTransform.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.xRotateSpeed;
        Quaternion targetRotation = Quaternion.Euler(0, lookAngle, 0);
        mainTranform.rotation = targetRotation;

        titleAngle -= smoothY * cameraConfig.yRotateSpeed;
        titleAngle = Mathf.Clamp(titleAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titleAngle, 0, 0);
    }
}
