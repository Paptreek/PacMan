using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    private Rigidbody2D _rb;
    private float _moveSpeed = 250.0f;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputValue = _moveAction.ReadValue<Vector2>();

        if (_moveAction.IsPressed())
        {
            if (inputValue.x != 0)
            {
                _rb.linearVelocityX = inputValue.x * _moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _rb.linearVelocityX = 0;
            }

            if (inputValue.y != 0)
            {
                _rb.linearVelocityY = inputValue.y * _moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _rb.linearVelocityY = 0;
            }
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
