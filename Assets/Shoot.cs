using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	Transform mBall;
	Ball mBallScript;

	void Start()
	{
		mBall = GameObject.FindGameObjectWithTag("Ball").transform;
		mBallScript = mBall.GetComponent<Ball>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (mBall.IsChildOf(transform))
				mBallScript.Shoot();
		}
	}
}
