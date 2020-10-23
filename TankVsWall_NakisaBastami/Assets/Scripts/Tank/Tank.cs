using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
    GameObject tank = null;
    ObjectPool<Projectile> projectilePool = null;
    MeshRenderer barrelMR = null;
    MeshRenderer tankMR = null;

    [Header("Shooting")]
    [SerializeField]
    GameObject shotSocket = null;
    [SerializeField]
    GameObject projectileTemplate = null;
    [SerializeField]
    int projectileAmount = 4;
    [SerializeField]
    float shootForce = 500;
    [SerializeField]
    float rotationSpeed = 30;
    Vector2 rotationInput = new Vector2();
    [SerializeField]
    bool inverseUpDownRotationInput = false;
    int xRotationDirection => inverseUpDownRotationInput ? 1 : -1;
    
    private void Start()
    {
        tank = gameObject.transform.parent.gameObject;
        projectilePool = new ObjectPool<Projectile>(projectileTemplate, projectileAmount, "BulletPool");
        barrelMR = GetComponentInChildren<MeshRenderer>();
        tankMR = GetComponentInParent<MeshRenderer>();
        foreach (var _proj in projectilePool.GetComponentList)
        {
            _proj.SetTank(this);
        }
    }

    public void SetColor(Color _color)
    {
        barrelMR.material.color = _color;
        tankMR.material.color = _color;
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime * rotationInput.y * xRotationDirection);
        tank.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationInput.x);
    }

    public void OnRotationInput(InputAction.CallbackContext _context)
    {
        rotationInput = _context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext _context)
    {
        if (!_context.performed)
            return;
        Projectile _proj = projectilePool.GetComponent();
        _proj.transform.position = shotSocket.transform.position;
        _proj.Shoot(shotSocket.transform.forward * shootForce);
    }
}
