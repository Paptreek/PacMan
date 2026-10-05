using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction _moveAction;
    private float _moveSpeed = 10.0f;

    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 inputValue = _moveAction.ReadValue<Vector2>();

        if (_moveAction.IsPressed())
        {
            transform.Translate(new Vector3(inputValue.x, inputValue.y) * _moveSpeed * Time.deltaTime);
        }
    }
}
