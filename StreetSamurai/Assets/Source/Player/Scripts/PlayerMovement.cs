using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontalInput, float speed) =>
        _rigidbody2D.velocity = new Vector2(horizontalInput * speed, _rigidbody2D.velocity.y);

    public void Jump(float jumpForce)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
    }
}
