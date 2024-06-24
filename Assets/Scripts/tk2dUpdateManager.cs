// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUpdateManager
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class tk2dUpdateManager : MonoBehaviour
{
	private static tk2dUpdateManager Instance
	{
		get
		{
			if (tk2dUpdateManager.inst == null)
			{
				tk2dUpdateManager.inst = (UnityEngine.Object.FindObjectOfType(typeof(tk2dUpdateManager)) as tk2dUpdateManager);
				if (tk2dUpdateManager.inst == null)
				{
					GameObject gameObject = new GameObject("@tk2dUpdateManager");
					gameObject.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector);
					tk2dUpdateManager.inst = gameObject.AddComponent<tk2dUpdateManager>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
				}
			}
			return tk2dUpdateManager.inst;
		}
	}

	public static void QueueCommit(tk2dTextMesh textMesh)
	{
		tk2dUpdateManager.Instance.QueueCommitInternal(textMesh);
	}

	public static void FlushQueues()
	{
		tk2dUpdateManager.Instance.FlushQueuesInternal();
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.coSuperLateUpdate());
	}

	private void LateUpdate()
	{
		this.FlushQueuesInternal();
	}

	private IEnumerator coSuperLateUpdate()
	{
		this.FlushQueuesInternal();
		yield break;
	}

	private void QueueCommitInternal(tk2dTextMesh textMesh)
	{
		this.textMeshes.Add(textMesh);
	}

	private void FlushQueuesInternal()
	{
		int count = this.textMeshes.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dTextMesh tk2dTextMesh = this.textMeshes[i];
			if (tk2dTextMesh != null)
			{
				tk2dTextMesh.DoNotUse__CommitInternal();
			}
		}
		this.textMeshes.Clear();
	}

	private static tk2dUpdateManager inst;

	[SerializeField]
	private List<tk2dTextMesh> textMeshes = new List<tk2dTextMesh>(64);
}
