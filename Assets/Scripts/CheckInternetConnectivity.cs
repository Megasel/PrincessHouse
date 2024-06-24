// dnSpy decompiler from Assembly-CSharp.dll class: CheckInternetConnectivity
using System;
using System.Collections;
using UnityEngine;

public class CheckInternetConnectivity : MonoBehaviour
{
	public void Awake()
	{
		if (CheckInternetConnectivity._instance == null)
		{
			CheckInternetConnectivity._instance = this;
		}
	}

	//public void CheckInternetConnection(Action<bool, Ping> action)
	//{
	//	if (!this.busy)
	//	{
	//		this.busy = true;
	//		this.CheckPing("8.8.8.8", action);
	//	}
	//}

	//private void CheckPing(string ip, Action<bool, Ping> action)
	//{
	//	base.StartCoroutine(this.StartPing(ip, action));
	//}

	//private IEnumerator StartPing(string ip, Action<bool, Ping> action)
	//{
	//	WaitForSeconds f = new WaitForSeconds(0.05f);
	//	Ping p = new Ping(ip);
	//	while (!p.isDone && this.tempTime < this.timeOut)
	//	{
	//		this.tempTime += Time.deltaTime;
	//		yield return f;
	//	}
	//	this.PingFinished(p, action);
	//	yield break;
	//}

	//private void PingFinished(Ping p, Action<bool, Ping> action)
	//{
	//	this.busy = false;
	//	if (this.tempTime < this.timeOut)
	//	{
	//		this.tempTime = 0f;
	//		if (p.time < 80)
	//		{
	//			action(true, p);
	//		}
	//		else
	//		{
	//			action(false, p);
	//		}
	//	}
	//	else
	//	{
	//		this.tempTime = 0f;
	//		action(false, p);
	//	}
	//}

	public static CheckInternetConnectivity _instance;

	public float timeOut = 1f;

	private float tempTime;

	private bool busy;
}
