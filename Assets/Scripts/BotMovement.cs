using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private float _stepCheckDistance = 1f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _stepMask;

    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private bool _canJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Update()
    {
        MoveTowardsTarget();
        CheckStep();
    }

    private void MoveTowardsTarget()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, _target.position);

            _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, _groundMask);

            if (distanceToPlayer > _minDistanceToPlayer)
            {
                _rigidbody.MovePosition(transform.position + direction * _speed * Time.deltaTime);
            }
        }
    }

    private void CheckStep()
    {
        _canJump = false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, _stepCheckDistance, _stepMask))
        {
            if (hit.collider.TryGetComponent(out Step step) && transform.position.y < _target.position.y)
            {
                _canJump = true;
            }
        }
    }

    private void Jump()
    {
        if (_canJump && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
        }
    }
}
