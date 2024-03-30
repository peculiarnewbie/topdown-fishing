using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    GameState gameState = GameState.moving;
    InputAction moveAction;
    InputAction fishAction;


    // Fishing
    bool castStarted = false;
    [SerializeField] RectTransform castFillTransform;
    float castFillSize = 0;

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
            }
            else
            {
                if (castFillSize == 200f) return;
                castFillSize += Time.deltaTime * 100;
                if (castFillSize > 200f) castFillSize = 200f;
                castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);

            }
        }
        else
        {
            if (castStarted)
            {
                castStarted = false;
                castFillSize = 0;
                castFillTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, castFillSize);
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
