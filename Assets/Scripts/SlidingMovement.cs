using UnityEngine;
using System.Collections;

public class SlidingMovement : MonoBehaviour {
    public float Speed = 10f;
    public Vector3 Velocity;
    public Transform Wall;
    private Vector3 wallNormal;
    private Vector3 slidingVelocity;

    // Use this for initialization
    void Start () {
        Velocity = transform.forward * Speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (Wall == null)
            return;

        Velocity = transform.forward * Speed;
        wallNormal = Wall.up;

        slidingVelocity = Velocity - (Vector3.Dot(Velocity, wallNormal) * wallNormal);
    }

    void OnDrawGizmos()
    {
        if (Wall == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Velocity);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Wall.position, wallNormal);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(Wall.position, slidingVelocity);
    }
}
