using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistanceToPlayer;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveTowardsTarget();
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

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Step step) && transform.position.y < _target.transform.position.y)
        {
            Jump();
        }
    }
}
