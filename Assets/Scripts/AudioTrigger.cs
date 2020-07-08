using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player" && !hasPlayed)
        {
            hasPlayed = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
