using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private const string HorizontalInputAxis = "Horizontal";
    private const string VerticalInputAxis = "Vertical";

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;

    private CharacterController _characterController;
    private Vector3 _moveVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw(HorizontalInputAxis);
        float verticalInput = Input.GetAxisRaw(VerticalInputAxis);

        if(_characterController.isGrounded)
        {
            _moveVelocity = new Vector3(horizontalInput * _speed, 0, verticalInput * _speed);
        }

        Jump();

        _moveVelocity.y += Physics.gravity.y * Time.deltaTime;

        _characterController.Move(_moveVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            _moveVelocity.y = _jumpSpeed;
        }
    }
}
