using UnityEngine;
using System.Collections;

public class Atan2Test : MonoBehaviour {

    public Transform otherTransform;
    public float angle = 0f;
    public float angle2 = 0f;
    //public float angle3 = 0f;
    private Vector3 distToObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        distToObj = otherTransform.position - transform.position;
        angle = Mathf.Atan2(distToObj.y, distToObj.x) * Mathf.Rad2Deg;
        angle2 = Mathf.Acos(distToObj.normalized.x) * Mathf.Sign(distToObj.y) * Mathf.Rad2Deg;
        //angle3 = (Mathf.PI - Mathf.Asin(distToObj.normalized.y)) * Mathf.Rad2Deg;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, distToObj);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * Vector3.Dot(distToObj, transform.right));
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up * Vector3.Dot(distToObj, transform.up));
    }
}
