using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    //returns the value of the player's input on the x axis
    public float GetHorizontalInputFromVector2()
    {
        float horizontal = playerInputActions.Player.Move.ReadValue<Vector2>().x;

        return horizontal;
    }

    //returns the value of the player's input on the y axis
    public float GetVerticalInputFromVector2()
    {
        float vertical = playerInputActions.Player.Move.ReadValue<Vector2>().y;

        return vertical;
    }

}
