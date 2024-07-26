using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject _PointA, _PointB;
    private Rigidbody2D _rb;
    private Transform _currentPoint;
    public float _speed;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentPoint = _PointB.transform;

    }

    void Update()
    {
        Vector2 point = _currentPoint.position - transform.position;
        if(_currentPoint == _PointB.transform)
        {
            _rb.velocity = new Vector2(_speed, 0);
        }
        else
        {
            _rb.velocity = new Vector2(-_speed, 0);
        }

        if (Vector2.Distance(transform.position,_currentPoint.position) < 0.5f && _currentPoint == _PointB.transform)
        {
            _currentPoint = _PointA.transform;
        }
        if(Vector2.Distance(transform.position, _currentPoint.position) < 0.5f && _currentPoint == _PointA.transform)
        {
            _currentPoint = _PointB.transform;
        }
    }
}
