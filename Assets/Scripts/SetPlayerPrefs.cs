using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class SetPlayerPrefs : MonoBehaviour
{
    public Toggle isRecording;
    public TMPro.TMP_InputField participantNumber;
    public TMPro.TMP_Dropdown dropDown;
    public Button button;
    private int _pNumber;
    private string _dropdownSelection;

    private void Start()
    {
        XRSettings.enabled = false;

        PlayerPrefs.SetInt("Recording", 0);
        _dropdownSelection = dropDown.options[dropDown.value].text;
        _pNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dropDown.onValueChanged.AddListener( delegate
        {
            DropDownValueChanged(dropDown);
        });
    }

    public void DropDownValueChanged(TMPro.TMP_Dropdown change)
    {
        _dropdownSelection = change.options[change.value].text;
    }

    public void SetNumber()
    {
        _pNumber = int.Parse(participantNumber.text);
    }

    public void LoadSceneFromDropDown()
    {
        Debug.Log(_pNumber + " " + _dropdownSelection);
        PlayerPrefs.SetString("Condition", _dropdownSelection);
        if (isRecording.isOn)
        {
            Debug.Log("Recording to file: " + "pNumber");
            PlayerPrefs.SetInt("Participant Number", _pNumber);
            PlayerPrefs.SetInt("Recording", 1);
        }   

        if (!(PlayerPrefs.GetString("Condition").Equals("HitAndRunPhoto")))
        {
            XRSettings.enabled = true;
        }

        SceneManager.LoadScene(_dropdownSelection);
    }
}
