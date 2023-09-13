using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f; // Adjust the speed as needed.
    private bool isMoving = false;
    private Vector3 targetPosition;

    public bool isDrowning = false;


    [SerializeField] private GameInput gameInput;
    [SerializeField] private bool movedFromFirstTile = false;

    void Update()
    {
        if (!isMoving)
        {
            HandleInput();
            detect();
        }
        else
        {
            Move();
            if (isDrowning)
            {
                //Can Play animation and then reset level.
                isDrowning = false;
                float animationTime = 0.25f;
                StartCoroutine(ExampleCoroutine());
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                IEnumerator ExampleCoroutine()
                {
                    //Print the time of when the function is first called.
                    Debug.Log("Started Coroutine at timestamp : " + Time.time);

                    //yield on a new YieldInstruction that waits for 5 seconds.
                    yield return new WaitForSeconds(animationTime);

                    //Can play an animation here where the player spins and shrinks slowly down into water.

                    //After we have waited 5 seconds print the time again.
                    Debug.Log("Finished Coroutine at timestamp : " + Time.time);

                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
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

    void detect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);

        StartIceFloorTile startIceFloorTile;
        IceFloorTile iceFloorTile;


        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.gameObject.name + " This object is here");
            //Debug.Log(collider.gameObject.TryGetComponent(out StartIceFloorTile startIceFloorTile) + " This startIceFloorTile object is here");
            //Debug.Log(collider.gameObject.TryGetComponent(out IceFloorTile iceFloorTile) + " This iceFloorTile object is here");

            if (collider.gameObject.TryGetComponent(out startIceFloorTile))
            {
                // Debug.Log("On a StartIceFloorTile now");
            }

            if (collider.gameObject.TryGetComponent(out iceFloorTile))
            {
                //Debug.Log("On an IceFloorTile now");
                //Melt first start tile now
                movedFromFirstTile = true;
                //startIceFloorTile.meltStartingTile();
            }
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
                Debug.Log("There is a wall in the way");
                break;
            }
            if (collider.CompareTag("IceFloor"))
            {
                Debug.Log("Moved on to ice floor");
                //Needs to move onto new ice floor and melt the ice floor tile that the player was previously on
                Debug.Log("The object is:" + collider.gameObject.TryGetComponent(out IceFloorTile iceFloorTile1));

                //Need to interact with Interact Function here
                if (collider.gameObject.TryGetComponent(out IceFloorTile iceFloorTile))
                {
                    iceFloorTile.Interact();
                }

                //Need a reference to the exact tile the player is on, and then a reference to the tile
                //the player is moving to. Once player completely moves off the tile, the old tile melts

            }
            if (collider.CompareTag("WaterFloor"))
            {
                Debug.Log("Moved on to Water floor");

                if (collider.gameObject.TryGetComponent(out WaterFloorTile waterFloorTile))
                {
                    waterFloorTile.DrownInteract();
                    isDrowning = true;
                }
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

