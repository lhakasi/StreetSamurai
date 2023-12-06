using UnityEngine;
using States;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    private PlayerController _playerController;

    public bool IsFacingRight { get; private set; }

    private void Awake()
    {        
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable() =>
        _playerController.PlayerStateChanged += OnPlayerStateChanged;

    private void OnDisable() =>
        _playerController.PlayerStateChanged -= OnPlayerStateChanged;

    private void OnPlayerStateChanged(PlayerStates newState)
    {
        string state = newState.ToString();
        SetAnimation(state);
    }

    private void SetAnimation(string state)
    {
        _animator.Play(state);

        if (state == PlayerStates.Run.ToString())
            Flip();
    }

    public void Flip()
    {
        IsFacingRight = !IsFacingRight;

        if (IsFacingRight)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else
            transform.rotation = Quaternion.identity;
    }

    public float GetAnimationFullLenght() => 
        _animator.GetCurrentAnimatorStateInfo(0).length;
}