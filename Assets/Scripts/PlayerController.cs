using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    GameState gameState;
    InputAction moveAction;


    private void Start()
    {
        gameState = GameState.moving;
    }

    void Update()
    {
        if (gameState == GameState.moving)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 totalMove = playerInput * moveSpeed;
        transform.position = new Vector3(transform.position.x + totalMove.x, transform.position.y, transform.position.z + totalMove.y);
    }
}
