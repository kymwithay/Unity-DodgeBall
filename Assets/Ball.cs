using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	Transform mTrans;
	Transform mPlatform;

	float mGroundHeight;
	bool mFlying = false;
	int shotType = 0;
	float mSpeedFactor = 3;

	void Start()
	{
		mPlatform = GameObject.FindGameObjectWithTag("Platform").transform;
		mTrans = transform;
		mGroundHeight = mTrans.position.y;
	}

	void Update()
	{
		if (transform.parent.tag == "Player")
			mFlying = false;

		if (mFlying)
		{
			if (shotType == 0)
			{
				NormalShot();
			}
			else if (shotType == 1)
			{
				MagicShot();
			}
		}
		else if (transform.parent.tag != "Player" && mTrans.position.y > mGroundHeight)
			mTrans.position += Physics.gravity * Time.deltaTime;
	}

	public void OnTriggerEnter(Collider c)
	{
		if (c.name == "TeamMate")
		{
			mFlying = false;
			transform.parent = c.transform;
		}
		else
		{
			var enemy = c.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.Hit();
				//c.rigidbody.AddForce(Vector3.up * 50, ForceMode.Force);
				mFlying = false;
			}
		}
	}

	public void Shoot()
	{
		mTrans.parent = mPlatform;
		mFlying = true;

		StopAllCoroutines();
		StartCoroutine(StopFlying());

		shotType = Random.Range(0, 2);
		Debug.Log(shotType == 0 ? "normal" : "magic");
		mFlying = true;
		mSpeedFactor = 3;
	}

	IEnumerator StopFlying()
	{
		yield return new WaitForSeconds(10);
		mFlying = false;
	}

	private void NormalShot()
	{
		mTrans.position += Vector3.right * Time.deltaTime * mSpeedFactor;
	}

	private void MagicShot()
	{
		mSpeedFactor += Time.deltaTime * 10;
		NormalShot();
	}
}