using UnityEngine;
using System.Collections;

public class Pick : MonoBehaviour
{
	Transform mBall;
	Transform mPlatform;

	void Start()
	{
		mBall = GameObject.FindGameObjectWithTag("Ball").transform;
		mPlatform = GameObject.FindGameObjectWithTag("Platform").transform;
	}

	void Update()
	{
		if (Input.GetAxis("Fire1") > 0)
		{
			mBall.transform.parent = transform;
			mBall.transform.localPosition = Vector3.right * 0.5f;
		}

		if (Input.GetAxis("Fire2") > 0)
		{
			mBall.transform.parent = mPlatform;
		}
	}
}