using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementPlayer : MonoBehaviour
{
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] private bool isJoyStick;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Animator animator;

    private void Start()
    {
        EnableJoyStickInput();
    }
    public void EnableJoyStickInput()
    {
        isJoyStick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isJoyStick)
        {
            var movermentDirection = new Vector3(variableJoystick.Direction.x, 0, variableJoystick.Direction.y );
            characterController.SimpleMove(movermentDirection * speed);

            if ( movermentDirection.sqrMagnitude <= 0)
            {
                
                return;
            }
            animator.SetBool("isWalking", true);
            var targetRotation = Vector3.RotateTowards(characterController.transform.forward, movermentDirection,rotationSpeed * Time.deltaTime, 0.0f);
            characterController.transform.rotation = Quaternion.LookRotation(targetRotation);
        }
    }       
}
