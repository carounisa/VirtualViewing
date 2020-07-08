using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DataHandler : MonoBehaviour
{
    [HideInInspector]
    public PlayerData playerData;
    [HideInInspector]
    public PlayerData.Evidence evidenceData;

    private PlayerData.LookingBehaviour lookingData;
    private PlayerData.HeadData _head;
    private Stopwatch _stopwatch;
    private string _logFile;
    private string _logFilePath;
    private string _logDir;

    private float _interval = 1f;
    private float _currentTime = 0f;
    private bool _isRecording;

    private GameObject _target;


    private static DataHandler _instance;
    public static DataHandler instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataHandler>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Recording") == 1)
            _isRecording = true;

        UnityEngine.Debug.Log(_isRecording);

        _logDir = Path.Combine(Application.dataPath, "PlayerRecordings");

        if (!Directory.Exists(_logDir))
            Directory.CreateDirectory(_logDir);

        playerData = new PlayerData();
        playerData.headDataList = new List<PlayerData.HeadData>();
        playerData.evidenceList = new List<PlayerData.Evidence>();
        playerData.observationList = new List<PlayerData.LookingBehaviour>();
        playerData.timeStampList = new List<string>();
        playerData.pNumber = PlayerPrefs.GetInt("Participant Number");
        playerData.condition = PlayerPrefs.GetString("Condition");

        _logFile = string.Format("log{0}-PNum{1}_Con_{2}.json",
            System.DateTime.Now.ToString("dd-MM-yyyy"), 
            playerData.pNumber, playerData.condition);
        _logFilePath = Path.Combine(_logDir, _logFile);

        _stopwatch = new Stopwatch();
        _head = new PlayerData.HeadData();
        evidenceData = new PlayerData.Evidence();
        lookingData = new PlayerData.LookingBehaviour();

        UnityEngine.Debug.Log("Log files stored to: " + _logDir);

    }

    private void Update()
    {
        if (_isRecording) {
            _currentTime += Time.deltaTime;
            // record every second
            if (_currentTime >= _interval)
            {
                if (playerData.condition.Equals("HitAndRun"))
                {
                    _head = new PlayerData.HeadData();
                    _head.headPosition = Player.instance.hmdTransform.position;
                    _head.direction = Player.instance.hmdTransform.forward;
                    playerData.headDataList.Add(_head);
                    playerData.timeStampList.Add(string.Format("{0}:{1}:{2}:{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond));
                }
                _currentTime = _currentTime % _interval;

            }
        }
    }

    public void startTimer()
    {
        _stopwatch.Reset();
        _stopwatch.Start();
    }

    public void stopTimer()
    {
        _stopwatch.Stop();
        System.TimeSpan elapsed = _stopwatch.Elapsed;
        evidenceData.elapsedTime = string.Format("{0:00}:{1:00}:{2:00}", elapsed.Minutes, elapsed.Seconds, elapsed.Milliseconds);
    }

    public bool isWatchRunning()
    {
        return _stopwatch.IsRunning;
    }

    public void startRecordingEvidence(string name)
    {
        evidenceData = new PlayerData.Evidence();
        evidenceData.name = name;
        evidenceData.startTime = string.Format("{0}:{1}:{2}:{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        startTimer();
    }

    public void endRecordingEvidence()
    {
        evidenceData.endTime = string.Format("{0}:{1}:{2}:{3}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        playerData.evidenceList.Add(DataHandler.instance.evidenceData);
        stopTimer();
    }

    private void WriteToFile()
    {
            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(_logFilePath, json);
    }

    private void OnApplicationQuit()
    {

        if (PlayerPrefs.GetString("Condition").Equals("HitAndRun"))
        {
            foreach (KeyValuePair<string, string> item in RayHitEvidence._evidenceTable)
            {
                lookingData = new PlayerData.LookingBehaviour();
                lookingData.name = item.Key;
                lookingData.time = item.Value;
                playerData.observationList.Add(lookingData);
            }
        }

        if (_isRecording)
        {
            if (DataHandler.instance.isWatchRunning())
            {
                DataHandler.instance.endRecordingEvidence();
            }

            UnityEngine.Debug.Log("Exiting application and writing to file.");
            WriteToFile();
        }

    }
}
