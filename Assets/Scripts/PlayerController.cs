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
    FishingPhase fishingPhase = FishingPhase.Start;
    [SerializeField] CastController castController;

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

        if (fishAction.phase == InputActionPhase.Started)
        {
            if (fishingPhase == FishingPhase.Start)
            {
                fishingPhase = FishingPhase.PowerUp;
                castController.StartPowerUp();
            }
            else if (fishingPhase == FishingPhase.Casted)
            {
                bool pulled = castController.PullCast();
                Debug.Log(pulled);
                fishingPhase = FishingPhase.Pulled;
            }
        }
        else if (fishAction.IsPressed())
        {
            castController.PowerUp();
        }
        else
        {
            if (fishingPhase == FishingPhase.PowerUp)
            {
                fishingPhase = FishingPhase.Casted;
                castController.StartCasting();
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
