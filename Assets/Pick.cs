using UnityEngine;
using System.Collections;

public class Pick : MonoBehaviour
{
	Ball mBall;
	Transform mPlatform;

	void Start()
	{
		mBall = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
		mPlatform = GameObject.FindGameObjectWithTag("Platform").transform;
	}

	void Update()
	{
		if (transform.parent != null)
			return;

		// 吸球到身上
		if (Input.GetAxis("Fire1") > 0)
		{
			mBall.transform.parent = transform;
			mBall.Direction = Vector3.zero;

			mBall.transform.localPosition = Vector3.right * 0.5f;

			mBall.LastBallHolder = gameObject;
		}

		if (Input.GetAxis("Fire2") > 0)
		{
			var picks = GameObject.FindObjectsOfType<Pick>();
			var anotherPlayer = System.Array.Find(picks, p => { return p != this; });

			mBall.transform.parent = mPlatform;
			mBall.Direction = (anotherPlayer.transform.position - transform.position).normalized;
		}
	}
}