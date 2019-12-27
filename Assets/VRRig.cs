using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trakingPositionOffset;
    public Vector3 trakingRotationOffset;

    public void Map()
    {
        if (rigTarget != null)
        {
            rigTarget.position = vrTarget.TransformPoint(trakingPositionOffset);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trakingRotationOffset);
        }
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraint;
    public Vector3 headBodyOffset;

    void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    void Update()
    {
        /*transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;*/

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
}
