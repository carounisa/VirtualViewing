using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LookingAtEvidence : MonoBehaviour
{
    private Stopwatch _stopwatch;

    void Start()
    {
        _stopwatch = new Stopwatch();
        RayHitEvidence.RayHit += OnRayHit;
        RayHitEvidence.RayOut += OnRayOut;
        
    }

    private void OnRayHit(Transform transform)
    {
        if (!_stopwatch.IsRunning && transform == this.transform)
        {
            _stopwatch.Start();
            
        }
    }

    private void OnRayOut(Transform transform)
    {
        if( (transform != this.transform) && _stopwatch.IsRunning)
        {
            _stopwatch.Stop();
            if(!(RayHitEvidence._evidenceTable.ContainsKey(this.transform.tag)))
            {
                RayHitEvidence._evidenceTable.Add(this.transform.tag, _stopwatch.Elapsed.ToString());
            }

            RayHitEvidence._evidenceTable[this.transform.tag] = _stopwatch.Elapsed.ToString();
        }
    }

}
