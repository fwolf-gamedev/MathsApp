using UnityEngine;
using System.Collections;

public class InterceptLinearMove : MonoBehaviour {

    public LinearMove MovingTarget;
    public LinearMove Missile;

    private Vector2 direction;
    private Vector3 startPos;
    private float currentTime = 0f;
    float t = -1f;
    
	// Use this for initialization
	void Awake () {
        currentTime = Time.time;
        startPos = transform.position;
        direction.Normalize();
	}

    void Start()
    {
 
    }

    private void ComputeDirToAim()
    {
        if (MovingTarget == null || Missile == null)
            return;

        Vector2 v = MovingTarget.Velocity;
        Vector2 q = MovingTarget.transform.position;
        Vector2 p = new Vector2(startPos.x, startPos.y);
        Vector2 w = q - p;

        float A = Vector2.Dot(v, v) - Missile.Speed * Missile.Speed;
        float B = 2 * Vector2.Dot(v, w);
        float C = Vector2.Dot(w, w);

        if (A == 0f)
        {
            t = -C / B;
        }
        else
        {
            //Debug.Log("A = " + A);
            //Debug.Log("B = " + B);
            //Debug.Log("C = " + C);

            float delta = B * B - 4 * A * C;
            //Debug.Log("delta = " + delta);
            if (delta < 0)
            {
                return;
            }

            if (delta == 0)
            {
                t = -B / (2 * A);
            }
            else
            {
                float sqrtDelta = Mathf.Sqrt(delta);
                //Debug.Log("sqrtDelta = " + sqrtDelta);
                float t1 = (-B + sqrtDelta) / (2 * A);
                float t2 = (-B - sqrtDelta) / (2 * A);
                //Debug.Log("t1 = " + t1);
                //Debug.Log("t2 = " + t2);

                if (t1 < 0f && t2 < 0f)
                    return;

                if (t1 > 0f && t1 < t2 || t2 < 0f)
                    t = t1;
                else if (t2 > 0f)
                    t = t2;
                else
                    return;
            }
        }

        if (t < 0f)
            return;

        //Debug.Log("t = " + t);

        Vector2 targetPos = q + v * t;
        direction = targetPos - p;
    }

	// Update is called once per frame
	void Update () {
        if (MovingTarget == null)
            return;

        ComputeDirToAim();

        if (t == -1)
            return;

        transform.forward = direction.normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Missile.Launch(transform.position, direction);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}
