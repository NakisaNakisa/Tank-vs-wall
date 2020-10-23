using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody myRB = null;
    WallManager wallManager = null;
    Tank tank = null;
    const float baseLifeTime = 4;
    float lifeTime = 0;

    private void Start()
    {
        myRB = GetComponent<Rigidbody>();
        wallManager = FindObjectOfType<WallManager>();
    }

    public void SetTank(Tank _tank)
    {
        tank = _tank;
    }


    public void Shoot(Vector3 _force)
    {
        gameObject.SetActive(true);
        if (!myRB)
            myRB = GetComponent<Rigidbody>();        
        myRB.AddForce(_force);
        lifeTime = baseLifeTime;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Stone _stone = other.GetComponent<Stone>();
        if(_stone && !_stone.IsHit)
        {
            Color _toChange = _stone.GetColor;
            tank.SetColor(_toChange);
            foreach (var _otherStone in wallManager.StoneAccess)
            {
                if (_otherStone.GetColor == _toChange)
                    _otherStone.Hit(Color.black);
            }
        }
        myRB.velocity = Vector3.zero;
        myRB.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
