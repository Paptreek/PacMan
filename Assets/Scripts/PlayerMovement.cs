using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    private Rigidbody2D _rb;
    private float _moveSpeed = 250.0f;
    private bool _canMoveLeft;
    private bool _canMoveUp;
    private bool _canMoveRight;
    private bool _canMoveDown;
    private bool _movingLeft;
    private bool _movingUp;
    private bool _movingRight;
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

        if (inputValue.x < 0 && _canMoveLeft)
        {
            _movingUp = false;
            _movingLeft = true;
            _movingDown = false;
            _movingRight = false;
        }
        else if (inputValue.y > 0 && _canMoveUp)
        {
            _movingUp = true;
            _movingLeft = false;
            _movingDown = false;
            _movingRight = false;
        }
        else if (inputValue.x > 0 && _canMoveRight)
        {
            _movingUp = false;
            _movingLeft = false;
            _movingDown = false;
            _movingRight = true;
        }
        else if (inputValue.y < 0 && _canMoveDown)
        {
            _movingUp = false;
            _movingLeft = false;
            _movingDown = true;
            _movingRight = false;
        }
    }

    private void CheckSurroundings()
    {
        RaycastHit2D hitWest = Physics2D.Raycast(transform.position, Vector2.left, 1);
        RaycastHit2D hitNorth = Physics2D.Raycast(transform.position, Vector2.up, 1);
        RaycastHit2D hitEast = Physics2D.Raycast(transform.position, Vector2.right, 1);
        RaycastHit2D hitSouth = Physics2D.Raycast(transform.position, Vector2.down, 1);

        if (hitWest.collider)
        {
            if (hitWest.collider.CompareTag($"Wall"))
            {
                _canMoveLeft = false;
                Debug.Log($"Hit WEST wall");
            }
        }
        else
        {
            _canMoveLeft = true;
        }

        if (hitNorth.collider)
        {
            if (hitNorth.collider.CompareTag($"Wall"))
            {
                _canMoveUp = false;
                Debug.Log($"Hit NORTH wall");
            }
        }
        else
        {
            _canMoveUp = true;
        }

        if (hitEast.collider)
        {
            if (hitEast.collider.CompareTag($"Wall"))
            {
                _canMoveRight = false;
                Debug.Log($"Hit EAST wall");
            }
        }
        else
        {
            _canMoveRight = true;
        }

        if (hitSouth.collider)
        {
            if (hitSouth.collider.CompareTag($"Wall"))
            {
                _canMoveDown = false;
                Debug.Log($"Hit SOUTH wall");
            }
        }
        else
        {
            _canMoveDown = true;
        }
    }
}
