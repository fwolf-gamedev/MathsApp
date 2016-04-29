using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public Transform A;
    public Transform B;
    public Transform C;
    public Transform Center;
    public Transform Light;
    public Vector3 AB = Vector3.zero;
    public Vector3 AC = Vector3.zero;
    public Vector3 ABCrossAC = Vector3.zero;
    public Vector3 ABCrossACNorm = Vector3.zero;
	public Vector3 ACCrossABNorm = Vector3.zero;
	public Vector3 ACCrossAB = Vector3.zero;
	public Vector3 Normale = Vector3.zero;
    public Vector3 L = Vector3.zero;
	public Vector3 LNormalized = Vector3.zero;
    public float Dot = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        AB = B.localPosition - A.localPosition;
        AC = C.localPosition - A.localPosition;
        ABCrossAC = Vector3.Cross(AB, AC);
		ACCrossAB = Vector3.Cross(AC, AB);
        ABCrossACNorm = ABCrossAC.normalized;
		ACCrossABNorm = ACCrossAB.normalized;
		Normale = ACCrossABNorm;
//        L = (Light.localPosition - Center.localPosition).normalized;
		LNormalized = L.normalized;
		Dot = Vector3.Dot(LNormalized, ACCrossABNorm);
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(A.localPosition, B.localPosition);
        Gizmos.DrawLine(A.localPosition, C.localPosition);
        Gizmos.DrawLine(B.localPosition, C.localPosition);
        Gizmos.color = new Color(Dot, Dot, 0f);
        Gizmos.DrawSphere(Center.localPosition, 0.1f);
        Gizmos.color = Color.red;
		Gizmos.DrawLine(Center.position, Center.position + Normale);
		Gizmos.color = Color.yellow;
		Vector3 lightpos = Light.position;
		Gizmos.DrawLine(lightpos, lightpos + L);
    }
}
