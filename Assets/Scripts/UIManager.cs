using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;
using System;
using System.Diagnostics;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    UIData[] _uiArray;
    [SerializeField]
    ArrayList _bread;
    [SerializeField]
    GameObject _dot;
    [SerializeField]
    GameObject _panel;

    public Sprite emptyBreadcrumb;
    public Sprite filledBreadcrumb;
    public GameObject dotHolder;
    Image currImg;
    bool _hasPlayed = false;
    Stopwatch stopWatch;


    //Panel tracking
    int _currPanel;

    //Main panel
    GameObject _mainPanel;
    Image _mainSprite;
    //TextMeshProUGUI _mainText;
    Text _mainText;

    //Back Panel
    GameObject _backPanel;
    Image _backSprite;
    //TextMeshProUGUI _backText;

    // Start is called before the first frame update
    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        stopWatch = new Stopwatch();

        if(UnityEngine.XR.XRSettings.enabled)
            UnityEngine.XR.XRSettings.enabled = false;

        _bread = new ArrayList();

        foreach (UIData data in _uiArray)
        {
            GameObject newCrumb = Instantiate(_dot, dotHolder.transform);

            Image newCrumbImg = newCrumb.GetComponent<Image>();

            newCrumbImg.sprite = emptyBreadcrumb;

            _bread.Add(newCrumbImg);
        }

        _currPanel = 0;

        currImg = (Image)_bread[_currPanel];
        currImg.sprite = filledBreadcrumb;

        //Initialize our main panel with data
        _mainPanel = Instantiate(_panel, transform);
    //    _mainText = _mainPanel.GetComponentInChildren<TextMeshProUGUI>();
        _mainText = _mainPanel.GetComponentInChildren<Text>();
        _mainSprite = _mainPanel.GetComponentInChildren<Image>();


        //backSprite = _mainPanel.GetComponentInChildren<Image>();
        //_backText = _mainPanel.GetComponentInChildren<TextMeshProUGUI>();


        _mainSprite.sprite = _uiArray[_currPanel].image;
        _mainText.text = _uiArray[_currPanel].header;
        
    }

    // Update is called once per frame
    void Update()
    {
        _mainSprite.sprite = _uiArray[_currPanel].image;
        _mainText.text = _uiArray[_currPanel].header;

        if (Input.GetKeyDown(KeyCode.RightArrow) && (_currPanel != _uiArray.Length-1))
        {
            if (DataHandler.instance.isWatchRunning())
            {
                DataHandler.instance.endRecordingEvidence();
            }


            // _backSprite.sprite = _uiArray[_currPanel].image;
            // _backText.text = _uiArray[_currPanel].header;

            currImg = (Image)_bread[_currPanel];
            currImg.sprite = emptyBreadcrumb;

            if (_currPanel < _uiArray.Length - 1)
                _currPanel++;

            currImg = (Image)_bread[_currPanel];
            currImg.sprite = filledBreadcrumb;

            _mainSprite.sprite = _uiArray[_currPanel].image;
            _mainText.text = _uiArray[_currPanel].header;
            DataHandler.instance.startRecordingEvidence(_mainText.text);
            if (_mainText.text.Equals("Eye Witness") && !(_hasPlayed))
            {
                _hasPlayed = true;
                UnityEngine.Debug.Log("is playing");
                GetComponent<AudioSource>().Play();
            }


        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _currPanel != 0)
        {

            if (DataHandler.instance.isWatchRunning())
            {
                DataHandler.instance.endRecordingEvidence();
            }


            // _backSprite.sprite = _uiArray[_currPanel].image;
            // _backText.text = _uiArray[_currPanel].header;

            currImg = (Image)_bread[_currPanel];
            currImg.sprite = emptyBreadcrumb;

            _currPanel--;

            currImg = (Image)_bread[_currPanel];
            currImg.sprite = filledBreadcrumb;
            _mainSprite.sprite = _uiArray[_currPanel].image;
            _mainText.text = _uiArray[_currPanel].header;
            DataHandler.instance.startRecordingEvidence(_mainText.text);
        }
    }
}
