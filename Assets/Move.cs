using System;
using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	public Transform Ground;
	public Transform LeftTopT, BottomRightT;

	public float Speed;
	public AnimationCurve JumpCurve;

	float groundHeight;
	bool jumping = false;
	float jumpRatio;

	void Awake()
	{
		groundHeight = Ground.position.y;
	}

	void Update()
	{
		if (transform.parent != null)
			return;

		var x = Input.GetAxis("Horizontal");
		if ((x < 0 && transform.position.x > LeftTopT.position.x) ||
			(x > 0 && transform.position.x < BottomRightT.position.x))
		{
			Ground.Translate(-x * Time.deltaTime * Speed, 0, 0);
		}

		var y = Input.GetAxis("Vertical");
		if ((y < 0 && transform.position.z > BottomRightT.position.z) ||
			(y > 0 && transform.position.z < LeftTopT.position.z))
		{
			Ground.Translate(0, 0, -y * Time.deltaTime * Speed);
		}

		if (!jumping && Input.GetAxis("Jump") > 0)
		{
			jumping = true;
			jumpRatio = 0;
		}

		if (jumping)
		{
			jumpRatio += Time.deltaTime;
			Ground.position = new Vector3(Ground.position.x, groundHeight - JumpCurve.Evaluate(jumpRatio), Ground.position.z);

			if (jumpRatio >= 1)
				jumping = false;
		}
	}
}