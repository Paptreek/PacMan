using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    private Rigidbody2D _rb;
    private float _moveSpeed = 250.0f;
    private bool _movingLeft;
    private bool _movingRight;
    private bool _movingUp;
    private bool _movingDown;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckSurroundings();
        SetMovementDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_movingUp)
        {
            _rb.linearVelocityX = 0;
            _rb.linearVelocityY = _moveSpeed * Time.fixedDeltaTime;
        }
        else if (_movingLeft)
        {
            _rb.linearVelocityY = 0;
            _rb.linearVelocityX = -_moveSpeed * Time.fixedDeltaTime;
        }
        else if (_movingDown)
        {
            _rb.linearVelocityX = 0;
            _rb.linearVelocityY = -_moveSpeed * Time.fixedDeltaTime;
        }
        else if (_movingRight)
        {
            _rb.linearVelocityY = 0;
            _rb.linearVelocityX = _moveSpeed * Time.fixedDeltaTime;
        }
        
    }
    
    private void SetMovementDirection()
    {
        Vector2 inputValue = _moveAction.ReadValue<Vector2>();

        if (inputValue.x < 0 /*&& _canMoveLeft*/)
        {
            _movingUp = false;
            _movingLeft = true;
            _movingDown = false;
            _movingRight = false;
        }
        else if (inputValue.x > 0 /*&& _canMoveRight*/)
        {
            _movingUp = false;
            _movingLeft = false;
            _movingDown = false;
            _movingRight = true;
        }
        else if (inputValue.y < 0)
        {
            _movingUp = false;
            _movingLeft = false;
            _movingDown = true;
            _movingRight = false;
        }
        else if (inputValue.y > 0)
        {
            _movingUp = true;
            _movingLeft = false;
            _movingDown = false;
            _movingRight = false;
        }
    }

    private void CheckSurroundings()
    {
        RaycastHit2D hitNorth = Physics2D.Raycast(transform.position, Vector2.up, 1);
        //set up layer that only includes walls and use that?

        if (hitNorth.collider)
        {
            Debug.Log($"Hitting North Wall!");
        }
    }
}
