using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Adjust the speed as needed.
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
        else
        {
            Move();
        }
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            targetPosition = transform.position + new Vector3(Mathf.Sign(horizontalInput), 0, 0);
            TryMove();
        }
        else if (Mathf.Abs(verticalInput) > 0.1f)
        {
            targetPosition = transform.position + new Vector3(0, 0, Mathf.Sign(verticalInput));
            TryMove();
        }
    }

    void TryMove()
    {
        Collider[] colliders = Physics.OverlapSphere(targetPosition, 0.1f); // Adjust the radius as needed.

        bool canMove = true;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                canMove = false;
                break;
            }
        }

        if (canMove)
        {
            isMoving = true;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}

