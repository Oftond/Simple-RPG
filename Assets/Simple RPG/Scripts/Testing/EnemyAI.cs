using Player.Movement;
using UnityEngine;
using Zenject;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private Vector2 _playerPosition;
    private bool _isMoving;
    private bool _isFacingRight = true;

    public bool IsMoving => _isMoving;
    public bool IsFacingRight => _isFacingRight;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPosition = _rb.position;
        _isMoving = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.TryGetComponent(out PlayerMovementSystem pms))
            {
                _isMoving = true;
                _playerPosition = pms.GetComponent<Rigidbody2D>().position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.TryGetComponent(out PlayerMovementSystem pms))
                _isMoving = false;
        }
    }

    private void Update()
    {
        if (!_isMoving) return;

        Vector2 curPos = _rb.position;
        Vector2 direction = _playerPosition - curPos;

        if (direction.x > 0 && _isFacingRight)
            _isFacingRight = false;
        else if (direction.x < 0 && !_isFacingRight)
            _isFacingRight = true;

        Vector2 movement = direction.normalized * _speed * Time.deltaTime;
        Vector2 newPos = curPos + movement;
        _rb.MovePosition(newPos);
    }
}