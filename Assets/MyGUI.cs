using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGUI : MonoBehaviour
{
	static public MyGUI Instance;

	List<Enemy> mList;

	void Awake()
	{
		Instance = this;
		mList = new List<Enemy>();
	}

	public void Register(Enemy enemy)
	{
		mList.Add(enemy);
	}

	void OnGUI()
	{
		for (int i = 0; i < mList.Count; i++)
			GUI.Label(new Rect(0, 20 * i, 200, 20), mList[i].name + ":" + string.Empty.PadLeft(mList[i].HP, '='));
	}

	void OnDestroy()
	{
		Instance = null;
	}
}
