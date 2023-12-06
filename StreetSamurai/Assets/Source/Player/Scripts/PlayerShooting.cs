using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Bullet _bulletBlueprint;
    [SerializeField] private Transform _firePoint;          

    public void Shoot(float bulletSpeed)
    {
        Bullet bullet = Instantiate(_bulletBlueprint, _firePoint.position, _firePoint.rotation);

        bullet.SetSpeed(bulletSpeed);
    }
}