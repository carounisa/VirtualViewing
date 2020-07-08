using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTooltip : MonoBehaviour
{
    public GameObject tooltipPrefab;
    public string action;
    private float _tooltipHeight = 1f;
    GameObject clone;

    private void Start()
    {
        Valve.VR.Extras.SteamVR_LaserPointer.PointerIn += OnPoint;
        Valve.VR.Extras.SteamVR_LaserPointer.PointerClick += OnClick;
    }

    private void Awake()
    {
        clone = Instantiate(tooltipPrefab);

        clone.transform.position = new Vector3(transform.position.x, transform.position.y + _tooltipHeight, transform.position.z);
        clone.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);

       clone.GetComponentInChildren<Text>().text = action;

        clone.SetActive(true);
    }

    protected void OnPoint(object sender, Valve.VR.Extras.PointerEventArgs e)
    {
        if(e.target == transform && transform.tag == "Hover")
            clone.SetActive(false);
    }

    private void OnClick(object sender, Valve.VR.Extras.PointerEventArgs e)
    {
        if(e.target == transform && transform.tag == "Click")
            clone.SetActive(false);
    }







}
