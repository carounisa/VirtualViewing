using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTooltip : MonoBehaviour
{
    Canvas _canvas;

    public void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _canvas.enabled = false;
        }
    }
}
