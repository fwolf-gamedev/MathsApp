using UnityEngine;
using System.Collections;

public class DriftMovement : MonoBehaviour {

    public float driftFactor = 1f;
    public float speed = 1f;
    public Vector2 direction = Vector2.zero;

    private Vector2 velocity;
    private Vector3 startPos;
    private float currentTime = 0f;
    private Vector3 wantedDir;
    private Vector3 normProj;
    private Vector3 tanProj;

	// Use this for initialization
	void Start () {
        currentTime = Time.time;
        startPos = transform.position;
        wantedDir = velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 destPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            destPos.z = 0f;
            wantedDir = (destPos - transform.position).normalized * speed;
            normProj = Vector3.Dot(wantedDir, transform.right) * transform.right * driftFactor;
            tanProj = Vector3.Dot(wantedDir, transform.up) * transform.up;
            velocity = normProj + tanProj;
        }

        if (wantedDir.sqrMagnitude != 0f)
            transform.up = velocity;

        transform.position += new Vector3(velocity.x, velocity.y) * Time.deltaTime;
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, wantedDir);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, normProj);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, tanProj);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, velocity);
    }
}
