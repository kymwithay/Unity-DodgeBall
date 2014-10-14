using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	void Update()
	{
		if (transform.parent != null)
			return;

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			if (Ball.Instance.transform.IsChildOf(transform))
				Ball.Instance.Shoot();
		}
	}
}
