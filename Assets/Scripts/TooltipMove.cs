using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipMove : MonoBehaviour
{
    private float _speed = 1f;
    private float _height = 0.1f;
    private float _posY;

    private void Start()
    {
        _posY = transform.position.y;
        
    }

    void Update()
    {
        Vector3 pos = transform.position;
        float newY = (Mathf.Sin(Time.time * _speed));
        transform.position = new Vector3(pos.x, (newY * _height) + _posY, pos.z);

    }
}
