using System;
using UnityEngine;
using States;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _movementBorders;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpStateDuration;
    [SerializeField] private float _fireRate = 8f;
    [SerializeField] private float _bulletSpeed = 8f;

    private Player _player;
    private Rigidbody2D _rigidbody;
    private PlayerMovement _playerMovement;
    private PlayerAnimator _playerAnimator;
    private GroundChecker _groundChecker;
    private PlayerShooting _playerShooting;

    private float _horizontalInput;
    private float _nextFireTime;

    private bool _isJumpPressed;
    private bool _isJumping = false;
    private bool _isShootingPressed;
    private bool _isFacingRight;
    private bool _isCrouchPressed;
    private bool _isCrouching = false;

    public event Action<PlayerStates> PlayerStateChanged;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
        _playerShooting = GetComponent<PlayerShooting>();

        _player.SetState(PlayerStates.Idle);
    }

    private void Start()
    {
        StartState(PlayerStates.Idle);
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _isJumpPressed = true;            
        }

        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _isShootingPressed = true;

            
        }

        if (Input.GetKey(KeyCode.S))
        {
            _isCrouching = true;
        }
        
        if (Input.GetKeyUp(KeyCode.S))
        {
            _isCrouching = false;
        }

        SetMovementState();

        SetFlip();
    }

    void FixedUpdate()
    {
        ExecuteMovementCommands();
    }

    private void SetMovementState()
    {
        if (_groundChecker.IsGrounded)
        {
            if (_isShootingPressed && Time.time >= _nextFireTime)
            {
                _isShootingPressed = false;

                _playerShooting.Shoot(_bulletSpeed);
                _nextFireTime = Time.time + 1f / _fireRate;               
                
            }

            if (_isCrouchPressed)
            {
                StartState(PlayerStates.Crouch);
                _isCrouchPressed = false;

                float crouchStateDuration = _playerAnimator.GetAnimationFullLenght();
                Invoke("CompleteState", crouchStateDuration);

                _isCrouching = true;
            }

            if (_isCrouching)
            {
                StartState(PlayerStates.Crouching);
            }
            else
            {
                if (_horizontalInput == 0)
                {
                    StartState(PlayerStates.Idle);
                }
                else
                {
                    StartState(PlayerStates.Run);
                }
            }            
        }
        else
        {
            if (_rigidbody.velocity.y < -3f)
            {
                _isJumping = false;

                StartState(PlayerStates.Fall);
            }

            if (_isJumping)
            {
                StartState(PlayerStates.Jump);
            }
        }
    }

    private void StartState(PlayerStates state)
    {
        if (_player.State != state)
        {
            _player.SetState(state);
            PlayerStateChanged?.Invoke(_player.State);
        }
    }

    private void CompleteState() =>    
        _player.SetState(PlayerStates.Idle);    

    private void SetFlip()
    {
        _isFacingRight = _playerAnimator.IsFacingRight;

        if (_horizontalInput > 0 && _isFacingRight)
            _playerAnimator.Flip();
        else if (_horizontalInput < 0 && !_isFacingRight)
            _playerAnimator.Flip();
    }

    private void ExecuteMovementCommands()
    {
        _playerMovement.Move(_horizontalInput, _speed);

        if (_isJumpPressed && _groundChecker.IsGrounded)
        {
            _playerMovement.Jump(_jumpForce);
            _isJumpPressed = false;
            _isJumping = true;
        }

        SetMovementBorders();

        Debug.Log(_isJumpPressed);
    }

    private void SetMovementBorders()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, _movementBorders.bounds.min.x, _movementBorders.bounds.max.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, _movementBorders.bounds.min.y, _movementBorders.bounds.max.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, _movementBorders.bounds.min.z, _movementBorders.bounds.max.z);

        transform.position = currentPosition;
    }
}

namespace States
{
    public enum PlayerStates
    {
        Idle = 0,
        Run = 1,
        Jump = 2,
        Fall = 3,
        Shoot = 4,
        Crouch = 5,
        Crouching = 6,
        Hurt = 7
    }
}