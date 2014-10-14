using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	public static Ball Instance;


	public Vector3 Direction = Vector3.right;
	public GameObject LastBallHolder;

	Transform mTrans;
	Transform mPlatform;

	float mGroundHeight;
	int shotType = 0;
	float mSpeedFactor = 3;

	void Awake()
	{
		Instance = this;
	}
	void OnDestroy()
	{
		Instance = null;
	}

	void Start()
	{
		mPlatform = GameObject.FindGameObjectWithTag("Platform").transform;
		mTrans = transform;
		mGroundHeight = mTrans.position.y;
	}

	void Update()
	{
		if (transform.parent.GetComponent<Pick>() != null)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.right * 0.5f, Time.deltaTime);
		}
		if (Direction != Vector3.zero)
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
		else if (transform.parent == null && mTrans.position.y > mGroundHeight)
			mTrans.position += Physics.gravity * Time.deltaTime;
	}

	public void OnTriggerEnter(Collider c)
	{
		Debug.Log(c.name);
		if (c.name == "TeamMate" || c.name=="Capsule")
		{
			//mFlying = false;
			Direction = Vector3.zero;
			transform.parent = c.transform;

			// 把之前的玩家黏到地上
			if (LastBallHolder != null)
				LastBallHolder.transform.parent = LastBallHolder.GetComponent<Move>().Ground;

			// 把拿到球的玩家移出來
			c.transform.parent = null;
			LastBallHolder = c.gameObject;
			Direction = Vector3.zero;
		}
		else
		{
			var enemy = c.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.Hit();
				//c.rigidbody.AddForce(Vector3.up * 50, ForceMode.Force);
				//mFlying = false;
				Direction = Vector3.zero;

			}
		}
	}

	public void Shoot()
	{
		mTrans.parent = mPlatform;

		Direction = Vector3.right;
		StopAllCoroutines();
		StartCoroutine(StopFlying());

		shotType = Random.Range(0, 2);
		Debug.Log(shotType == 0 ? "normal" : "magic");

		mSpeedFactor = 3;
	}

	IEnumerator StopFlying()
	{
		yield return new WaitForSeconds(10);
		Direction = Vector3.zero;
	}

	private void NormalShot()
	{
		mTrans.position += Direction * Time.deltaTime * mSpeedFactor;
	}

	private void MagicShot()
	{
		mSpeedFactor += Time.deltaTime * 10;
		NormalShot();
	}
}