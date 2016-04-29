using UnityEngine;
using System.Collections;

public class TrajectoryPara : MonoBehaviour {

    public GameObject ballGao = null;
    public GameObject markerGao = null;

    private Transform throwAnchor = null;

    public Vector3 gravity = new Vector3(0f, -9.81f, 0f);
    public Vector3 shootDir = Vector3.up;
    public float startVelocity = 1f;

    //public float viscousDrag = 0f;
    //public float aerodynamicDrag = 0f;
    //public Vector3 DragForce = Vector3.zero;

    public bool trajectoryFired = false;
    public bool computeInter = false;
    public bool reset = false;

    private Vector3 startPos;
    private float time = 0f;

    public float debugTime = 0f;

    public float A = 0f;
    public float B = 0f;
    public float C = 0f;

	// Use this for initialization
	void Start () {

        throwAnchor = transform.FindChild("ThrowAnchor");

        startPos = throwAnchor.position;
        shootDir.Normalize();

        A = 0.5f * gravity.y;
        B = (shootDir * startVelocity).y;
        C = startPos.y;
	}
	
	// Update is called once per frame
	void Update () {

        if (reset)
        {
            reset = false;
            trajectoryFired = false;
            time = 0f;
            ballGao.transform.localPosition = Vector3.zero;
        }

        if (trajectoryFired)
        {
            if (ballGao.transform.position.y >= startPos.y)
                ballGao.transform.position = ComputeTrajectoryPosition(time);

            time += Time.deltaTime;
        }

        if (markerGao != null && computeInter)
        {
            computeInter = false;

            float tInter = FindIntersectionTime();
            Vector3 interPos = ComputeTrajectoryPosition(tInter);
            markerGao.transform.position = interPos;
        }
	}

    Vector3 ComputeTrajectoryPosition(float t)
    {
        Vector3 a = 0.5f * gravity;
        Vector3 b = (shootDir * startVelocity);
        Vector3 c = startPos;

        //Vector3 newPos = startPos + shootDir * startVelocity * t + 0.5f * gravity * t * t;
        Vector3 newPos = a * t * t + b * t + c;
        return newPos;
    }

    float ComputeTrajectoryPositionY(float t)
    {
        startPos = throwAnchor.position;
        shootDir.Normalize();

        A = 0.5f * gravity.y;
        B = (shootDir * startVelocity).y;
        C = startPos.y;

        float newPosY = A * t * t + B * t + C;
        return newPosY;
    }

    float FindIntersectionTime()
    {
        // equation composante y : 1/2 * gy * t * t + vy * t + y0 = 0
        startPos = throwAnchor.position;
        shootDir.Normalize();

        A = 0.5f * gravity.y;
        B = (shootDir * startVelocity).y;
        C = startPos.y;

        float delta = B * B - 4 * A * C;

        Debug.Log("delta = " + delta);
        Debug.Log("Sqrt delta = " + Mathf.Sqrt(delta));

        float t1 = (-B + Mathf.Sqrt(delta)) / (2 * A);
        float t2 = (-B - Mathf.Sqrt(delta)) / (2 * A);

        Debug.Log("t1 = " + t1);
        Debug.Log("t2 = " + t2);

        float time = (t1 < t2) ? t2 : t1;
        return time;
    }

    void OnDrawGizmos()
    {
        if (throwAnchor != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(throwAnchor.position, shootDir * startVelocity);
        }
    }
}
