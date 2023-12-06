using CodeMonkey.Utils;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform _aimTransform;

    private void Awake()
    {
        _aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        Debug.Log("Mouse Position: " + UtilsClass.GetMouseWorldPosition());
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void HandleShooting()
    {

    }
}