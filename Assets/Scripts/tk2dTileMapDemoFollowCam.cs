// dnSpy decompiler from Assembly-CSharp.dll class: tk2dTileMapDemoFollowCam
using System;
using UnityEngine;

public class tk2dTileMapDemoFollowCam : MonoBehaviour
{
	private void Awake()
	{
		this.cam = base.GetComponent<tk2dCamera>();
	}

	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 position2 = Vector3.MoveTowards(position, this.target.position, this.followSpeed * Time.deltaTime);
		position2.z = position.z;
		base.transform.position = position2;
		Rigidbody component = this.target.GetComponent<Rigidbody>();
		if (component != null && this.cam != null)
		{
			float magnitude = component.velocity.magnitude;
			float t = Mathf.Clamp01((magnitude - this.minZoomSpeed) / (this.maxZoomSpeed - this.minZoomSpeed));
			float num = Mathf.Lerp(1f, this.maxZoomFactor, t);
			this.cam.ZoomFactor = Mathf.MoveTowards(this.cam.ZoomFactor, num, 0.2f * Time.deltaTime);
		}
	}

	private tk2dCamera cam;

	public Transform target;

	public float followSpeed = 1f;

	public float minZoomSpeed = 20f;

	public float maxZoomSpeed = 40f;

	public float maxZoomFactor = 0.6f;
}
