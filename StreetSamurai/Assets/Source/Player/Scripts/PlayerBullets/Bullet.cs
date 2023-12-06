using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * _bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        _bulletSpeed = speed;
    }
}