using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarkerUI : MonoBehaviour
{
    public float heightOfUI;

    public TextMeshProUGUI heading;
    public Image image1;
    public Image image2;

    Canvas _canvas;
    bool _isEnabled = false;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = _isEnabled;

    }

    public void ShowUI(bool enabled)
    {
        _isEnabled = enabled;
        _canvas.enabled = enabled;
    }

    public bool IsEnabled()
    {
        return _isEnabled;
    }

    public void UpdateUI(string heading, Sprite image1, Sprite image2, Transform marker)
    {
        Vector3 positionUI = new Vector3(
            marker.position.x, 
            marker.position.y + heightOfUI, 
            marker.position.z);

        transform.position = positionUI;

        this.heading.text = heading;
        this.image1.sprite = image1;
        this.image1.SetNativeSize();
        this.image2.sprite = image2;
        this.image2.SetNativeSize();

    }

}
