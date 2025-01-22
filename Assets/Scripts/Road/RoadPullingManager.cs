using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RoadPullingManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _roads;
    [SerializeField] private float _startZPos = 65f;
    [SerializeField] private float _endZPos = -65f;

    void Update()
    {
        foreach(Transform t in _roads)
        {
            if(t.position.z <= _endZPos)
            {
                t.position = new Vector3(0, 0, _startZPos);
            }
        }
    }
}
