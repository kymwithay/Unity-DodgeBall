using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int HP = 10;

	Material m;
	void Start()
	{
		MyGUI.Instance.Register(this);
		m = GetComponent<MeshRenderer>().material;
	}

	public void Hit()
	{
		HP--;
		if (HP < 0) HP = 0;

		m.color = Color.red;
		StartCoroutine(C());
	}

	IEnumerator C()
	{
		yield return new WaitForSeconds(0.5f);
		m.color = Color.white;
	}
}
