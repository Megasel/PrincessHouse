// dnSpy decompiler from Assembly-CSharp.dll class: FinalScene
using System;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
	private void Start()
	{
	}

	public void AddMask(float x, float y)
	{
		UnityEngine.Debug.Log("Rect Start");
		if (x < 2.4f && x > -2.4f && y < 0f && y > -1.91f)
		{
			GameObject.Find("eatingsound").GetComponent<AudioSource>().Play();
			UnityEngine.Object.Instantiate<GameObject>(GameObject.Find("DepthMask"), new Vector3(x, y, 0f), Quaternion.identity);
			UnityEngine.Debug.Log("In To The Rect Start");
		}
		UnityEngine.Debug.Log("Rect End");
	}
}
