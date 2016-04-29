using UnityEngine;
using System.Collections.Generic;

public class CameraLookAt : MonoBehaviour {

    public List<GameObject> gaoToLookAtList = new List<GameObject>();

    private Vector3 lookAtPos = Vector3.zero;
    private Vector3 prevLookAtPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        lookAtPos = ComputePosToLookAt();
        prevLookAtPos = lookAtPos;
	}

    Vector3 ComputePosToLookAt()
    {
        Vector3 middlePos = Vector3.zero;

        foreach (GameObject gao in gaoToLookAtList)
        {
            Vector3 pos = gao.transform.position;
            middlePos += pos;
        }

        middlePos /= gaoToLookAtList.Count;

        return middlePos;
    }


	// Update is called once per frame
	void Update () {
        if (gaoToLookAtList.Count == 0)
            return;

        prevLookAtPos = lookAtPos;
        lookAtPos = ComputePosToLookAt();
        transform.LookAt(lookAtPos);

        transform.position += lookAtPos - prevLookAtPos;
	}
}
