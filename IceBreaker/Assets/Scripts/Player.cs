using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;

    private bool isWalking;

    [SerializeField] private GameInput gameInput;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.5f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            //Cannot move towards move direction

            //Attempt only X movement
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            //Testing here
            Ray ray = new Ray(transform.position, transform.forward);

            float maxDistance = 1f;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                // Check if the object hit has the specified tag.
                if (hit.collider.CompareTag("BreakableFloor"))
                {
                    // The ray hit an object with the specified tag.
                    Debug.Log("Object with tag BreakableFloor" + " is in front of the player.");

                    // You can perform additional actions here, such as interacting with the object.
                }
            }

            /*
            if (Physics.Raycast(transform.position, transform.forward, 1f))
            {
                //Detection
                Debug.Log("Detected something");
            }
            */

            if (canMove)
            {
                //Can move only on x axis
                moveDirection = moveDirectionX;
            }
            else
            {
                //Cannot move only on the x

                //Attempt only Z movement
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    //can move only on the z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDistance * moveDirection;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BreakableFloor"))
        {
            Debug.Log("Touched Breakable Floor!");

        }
    }
}
