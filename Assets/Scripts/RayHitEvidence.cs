using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RayHitEvidence : MonoBehaviour
{

    public static Dictionary<string, string> _evidenceTable;
    private Vector3 _playerPos;
    private Vector3 _playerForwardDirection;
    private Transform _evidencePlayerIsLookingAt;

    public delegate void RayEvent(Transform transform);
    public static event RayEvent RayHit;
    public static event RayEvent RayOut;



    // Start is called before the first frame update
    void Start()
    {
        _evidenceTable = new Dictionary<string, string>();

    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = Player.instance.hmdTransform.position;
        _playerForwardDirection = Player.instance.hmdTransform.forward;

        UnityEngine.Debug.DrawRay(_playerPos, _playerForwardDirection, Color.blue);

       // UnityEngine.Debug.Log(Vector3.Dot(Player.instance.hmdTransform.forward, (this.transform.position - Player.instance.hmdTransform.transform.position).normalized) + " " + this.transform.tag);


        RaycastHit hit;
        if (Physics.Raycast(_playerPos, _playerForwardDirection, out hit, 3f, LayerMask.GetMask("Evidence"))) {
            _evidencePlayerIsLookingAt = hit.transform;
            OnRayHit(_evidencePlayerIsLookingAt);
        } else
        {
            OnRayOut(transform);
        }
    }

    public void OnRayHit(Transform transform)
    {
        if(RayHit != null)
        {
            RayHit(transform);
        }

    }

    public void OnRayOut(Transform transform)
    {
        if(RayOut != null)
        {
            RayOut(transform);
        }
    }

}
