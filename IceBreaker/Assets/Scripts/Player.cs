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
    [SerializeField] private bool reachedGoal = false;

    private bool freezePlayer = false;

    [SerializeField] private GameInput gameInput;

    [SerializeField] private PlayerAnimator playerAnimator;

    //[SerializeField] private GameObject playerCollider;
    void Update()
    {
        if (!isMoving)
        {
            if (!freezePlayer)
            {
                HandleInput();
            }
        }
        else
        {
            Move();
            ReachedGoalCheck();
            DrownPlayerCheck();
        }
    }

    void DrownPlayerCheck()
    {
        if (isDrowning)
        {
            DisableCollider();
            playerAnimator.playDrownAnimation();
            float animationTime = 1f;
            StartCoroutine(ExampleCoroutine());
            IEnumerator ExampleCoroutine()
            {
                yield return new WaitForSeconds(animationTime);

                //Reloads the Active Scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void ReachedGoalCheck()
    {
        if (reachedGoal)
        {
            //Can do a coroutine / passed level screen popup if desired
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

            float waitTime = 0.5f;
            StartCoroutine(NextLevel());
            IEnumerator NextLevel()
            {
                freezePlayer = true;
                yield return new WaitForSeconds(waitTime);
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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
            //Checks if next tile is a wall
            if (collider.TryGetComponent(out WallTile wallTile))
            {
                //Has WallTile Component
                canMove = false;
                Debug.Log("TRYGETCOMPONENT There is a wall in the way");
                break;
            }
            //Checks if next tile is water
            if (collider.TryGetComponent(out WaterFloorTile waterFloorTile))
            {
                Debug.Log("Moved on to Water floor");
                isDrowning = true;
            }
            //Checks if next tile is the goal
            if (collider.TryGetComponent(out GoalTile goalTile))
            {
                Debug.Log("Moved on to Goal Tile");
                reachedGoal = true;
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

    //Disables collider on PlayerVisual. Used to prevent accidental melting while Drown animation plays
    void DisableCollider()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }
}

