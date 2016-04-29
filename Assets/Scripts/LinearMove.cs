using UnityEngine;
using System.Collections;

public class LinearMove : MonoBehaviour {

    public bool IsLaunched = false;
    public float Speed = 1f;
    public Vector2 Direction = Vector2.zero;

    private Vector2 velocity;
    public Vector2 Velocity { get { return velocity; } }
    private Vector3 startPos;
    public Vector2 StartPos { get { return new Vector2(startPos.x, startPos.y); } }

    private float currentTime = 0f;


	// Use this for initialization
	void Awake () {
        //currentTime = Time.time;
        startPos = transform.position;
        Direction.Normalize();
        velocity = Direction * Speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsLaunched == false)
            return;

        transform.forward = Direction;
        transform.position = startPos + new Vector3(velocity.x, velocity.y) * currentTime;
        currentTime += Time.deltaTime;
	}

    public void Launch(Vector3 pos, Vector2 dir)
    {
        //if (IsLaunched)
        //    return;

        IsLaunched = true;
        startPos = pos;
        Direction = dir;
        Direction.Normalize();
        velocity = Direction * Speed;
        currentTime = 0f;
    }
}
