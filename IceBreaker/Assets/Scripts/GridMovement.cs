using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking;
    private Vector3 lastInteractDirection;

    [SerializeField] private GameInput gameInput;

    private void Update()
    {
        HandleInteractions();

        HandleMovement();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance))
        {
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("-");
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);


        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += moveDistance * moveDirection;


        isWalking = moveDirection != Vector3.zero;
    }

    /*    
    public float moveSpeed = 3.0f;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.A)) Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.S)) Move(Vector3.back);
        if (Input.GetKeyDown(KeyCode.D)) Move(Vector3.right);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Move(Vector3 direction)
    {
        Vector3 newPosition = transform.position + direction;
        if (Physics.CheckBox(newPosition, Vector3.one * 0.9f, Quaternion.identity, LayerMask.GetMask("Floor")) &&
            !Physics.CheckBox(newPosition, Vector3.one * 0.9f, Quaternion.identity, LayerMask.GetMask("Wall")))
        {
            targetPosition = newPosition;
        }
    }

    */
}

