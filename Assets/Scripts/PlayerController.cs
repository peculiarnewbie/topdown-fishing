using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1;
    GameState gameState = GameState.moving;
    InputAction moveAction;
    InputAction fishAction;

    // Fishing
    bool castStarted = false;
    [SerializeField] CastUIController castUIController;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fishAction = InputSystem.actions.FindAction("Fish");
    }

    void Update()
    {
        if (gameState == GameState.moving)
        {
            Move();
        }

        if (fishAction.IsPressed())
        {
            if (!castStarted)
            {
                castStarted = true;
                castUIController.StartCasting();
            }
            else
            {
                castUIController.PowerUp();
            }
        }
        else
        {
            if (castStarted)
            {
                castStarted = false;
                castUIController.StopCasting();
            }
        }
    }

    void Move()
    {
        Vector2 playerInput = moveAction.ReadValue<Vector2>();
        Vector2 totalMove = playerInput * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + totalMove.x, transform.position.y, transform.position.z + totalMove.y);
    }
}
