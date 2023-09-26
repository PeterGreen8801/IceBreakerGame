using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 4.0f; // Adjust the speed as needed.
    private bool isMoving = false;
    private Vector3 targetPosition;

    [SerializeField] private bool isDrowning = false;

    [SerializeField] private GameInput gameInput;

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
        else
        {
            Move();
            DrownPlayerCheck();
        }
    }

    void DrownPlayerCheck()
    {
        if (isDrowning)
        {
            //Can Play animation and then reset level.
            float animationTime = 0.25f;
            StartCoroutine(ExampleCoroutine());
            IEnumerator ExampleCoroutine()
            {
                //Print the time of when the function is first called.
                Debug.Log("Started Coroutine at timestamp : " + Time.time);

                //yield on a new YieldInstruction that waits for 5 seconds.
                yield return new WaitForSeconds(animationTime);

                //Can play an animation here where the player spins and shrinks slowly down into water.

                //After we have waited 5 seconds print the time again.
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);

                //Reloads the Active Scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void HandleInput()
    {
        //Gets the horizontal(x) and vertical(y) inputs from the GameInput class
        float horizontalInput = gameInput.GetHorizontalInputFromVector2();
        float verticalInput = gameInput.GetVerticalInputFromVector2();

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
            if (collider.TryGetComponent(out WallTile wallTile))
            {
                //Has WallTile Component
                canMove = false;
                Debug.Log("TRYGETCOMPONENT There is a wall in the way");
                break;
            }
            if (collider.TryGetComponent(out WaterFloorTile waterFloorTile))
            {
                Debug.Log("Moved on to Water floor");
                isDrowning = true;
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

