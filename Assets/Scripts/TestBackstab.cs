using UnityEngine;
using System.Collections;

public class TestBackstab : MonoBehaviour {

	public float DistMin = 1f;
	public float anglePlayer = 0f;
	public float angleEnemy = 0f;
	public Transform enemyTr;
	public Vector3 dirToEnemy;
	public Vector3 dirEnemy;
	public Vector3 dirPlayer;

	public float dotDirPDirE = 0f;
	public float dotDirToEDirP = 0f;

	public bool isBackStabbing = false;

	public float deltaAngles = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		dirEnemy = new Vector3(Mathf.Cos (angleEnemy), Mathf.Sin (angleEnemy));
		dirPlayer = new Vector3(Mathf.Cos (anglePlayer), Mathf.Sin (anglePlayer));

		transform.forward = dirPlayer;
		enemyTr.forward = dirEnemy;

		dirToEnemy = enemyTr.position - transform.position;

		dotDirPDirE = Vector3.Dot(dirPlayer, dirEnemy);
		dotDirToEDirP = Vector3.Dot(dirToEnemy, dirPlayer);

		isBackStabbing = dirToEnemy.magnitude <= DistMin && dotDirPDirE > 0 && dotDirToEDirP > 0;

		deltaAngles = Mathf.Abs(angleEnemy - anglePlayer);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, DistMin);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + dirPlayer);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(enemyTr.position, enemyTr.position + dirEnemy);
	}
}
