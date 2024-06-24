// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUIBaseDemoController
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tk2dUIBaseDemoController : MonoBehaviour
{
	protected void RegisterWindow(Transform t)
	{
		this.RemoveUnity3HackFromWindow(t);
		this.ShowWindow(t);
		tk2dUIBaseDemoController.InitTransform initTransform = new tk2dUIBaseDemoController.InitTransform();
		initTransform.pos = t.position;
		initTransform.scale = t.localScale;
		initTransform.angle = t.eulerAngles.z;
		this.registeredWindows.Add(t, initTransform);
		this.HideWindow(t);
	}

	protected void AnimateShowWindow(Transform t)
	{
		if (!this.registeredWindows.ContainsKey(t))
		{
			this.RegisterWindow(t);
		}
		tk2dUIBaseDemoController.InitTransform initTransform = this.registeredWindows[t];
		this.ShowWindow(t);
		t.localPosition = new Vector3(-5f, 0f, 0f);
		t.localScale = Vector3.zero;
		t.localEulerAngles = new Vector3(0f, 0f, 10f);
		base.StartCoroutine(this.coTweenTransformTo(t, 0.3f, initTransform.pos, initTransform.scale, initTransform.angle));
	}

	protected void AnimateHideWindow(Transform t)
	{
		if (!this.registeredWindows.ContainsKey(t))
		{
			this.RegisterWindow(t);
		}
		base.StartCoroutine(this.coAnimateHideWindow(t));
	}

	private IEnumerator coAnimateHideWindow(Transform t)
	{
		yield return base.StartCoroutine(this.coTweenTransformTo(t, 0.3f, new Vector3(5f, 0f, 0f), Vector3.zero, -10f));
		this.HideWindow(t);
		yield break;
	}

	protected IEnumerator coResizeLayout(tk2dUILayout layout, Vector3 min, Vector3 max, float time)
	{
		Vector3 minFrom = layout.GetMinBounds();
		Vector3 maxFrom = layout.GetMaxBounds();
		for (float t = 0f; t < time; t += tk2dUITime.deltaTime)
		{
			float nt = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(t / time));
			Vector3 currMin = Vector3.Lerp(minFrom, min, nt);
			Vector3 currMax = Vector3.Lerp(maxFrom, max, nt);
			layout.SetBounds(currMin, currMax);
			yield return 0;
		}
		layout.SetBounds(min, max);
		yield break;
	}

	protected IEnumerator coTweenAngle(Transform t, float xAngle, float time)
	{
		float xStart = t.localEulerAngles.x;
		if (xStart > 0f)
		{
			xStart -= 360f;
		}
		for (float ut = 0f; ut < time; ut += Time.deltaTime)
		{
			float nt = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(ut / time));
			float a = Mathf.Lerp(xStart, xAngle, nt);
			t.localEulerAngles = new Vector3(a, 0f, 0f);
			yield return 0;
		}
		t.localEulerAngles = new Vector3(xAngle, 0f, 0f);
		yield break;
	}

	protected IEnumerator coMove(Transform t, Vector3 targetPosition, float time)
	{
		Vector3 startPosition = t.position;
		for (float ut = 0f; ut < time; ut += Time.deltaTime)
		{
			float nt = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(ut / time));
			t.position = Vector3.Lerp(startPosition, targetPosition, nt);
			yield return 0;
		}
		t.position = targetPosition;
		yield break;
	}

	protected IEnumerator coShake(Transform t, Vector3 translateConstraint, Vector3 rotationConstraint, float time)
	{
		Vector3 pos = t.position;
		Quaternion rot = t.rotation;
		for (float ut = 0f; ut < time; ut += Time.deltaTime)
		{
			float nt = Mathf.Clamp01(ut / time);
			float strength = 1f - nt;
			t.position = pos + Vector3.Scale(UnityEngine.Random.onUnitSphere, translateConstraint).normalized * strength * 0.01f;
			t.rotation = rot;
			t.Rotate(Vector3.Scale(UnityEngine.Random.onUnitSphere, rotationConstraint), 2f * strength);
			yield return 0;
		}
		t.position = pos;
		t.rotation = rot;
		yield break;
	}

	protected IEnumerator coTweenTransformTo(Transform transform, float time, Vector3 toPos, Vector3 toScale, float toRotation)
	{
		Vector3 fromPos = transform.localPosition;
		Vector3 fromScale = transform.localScale;
		Vector3 euler = transform.localEulerAngles;
		float fromRotation = euler.z;
		for (float t = 0f; t < time; t += tk2dUITime.deltaTime)
		{
			float nt = Mathf.Clamp01(t / time);
			nt = Mathf.Sin(nt * 3.14159274f * 0.5f);
			transform.localPosition = Vector3.Lerp(fromPos, toPos, nt);
			transform.localScale = Vector3.Lerp(fromScale, toScale, nt);
			euler.z = Mathf.Lerp(fromRotation, toRotation, nt);
			transform.localEulerAngles = euler;
			yield return 0;
		}
		euler.z = toRotation;
		transform.localPosition = toPos;
		transform.localScale = toScale;
		transform.localEulerAngles = euler;
		yield break;
	}

	protected void DoSetActive(Transform t, bool state)
	{
		t.gameObject.SetActive(state);
	}

	protected void ShowWindow(Transform t)
	{
		t.gameObject.SetActive(true);
	}

	protected void HideWindow(Transform t)
	{
		t.gameObject.SetActive(false);
	}

	protected void RemoveUnity3HackFromWindow(Transform t)
	{
		Vector3 position = t.position;
		position.y %= 1f;
		position.x %= 2f;
		t.position = position;
	}

	private Dictionary<Transform, tk2dUIBaseDemoController.InitTransform> registeredWindows = new Dictionary<Transform, tk2dUIBaseDemoController.InitTransform>();

	private class InitTransform
	{
		public Vector3 pos;

		public Vector3 scale;

		public float angle;
	}
}
