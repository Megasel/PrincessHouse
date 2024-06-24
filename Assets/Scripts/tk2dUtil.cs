// dnSpy decompiler from Assembly-CSharp.dll class: tk2dUtil
using System;
using UnityEngine;

public static class tk2dUtil
{
	public static bool UndoEnabled
	{
		get
		{
			return tk2dUtil.undoEnabled;
		}
		set
		{
			tk2dUtil.undoEnabled = value;
		}
	}

	public static void BeginGroup(string name)
	{
		tk2dUtil.undoEnabled = true;
		tk2dUtil.label = name;
	}

	public static void EndGroup()
	{
		tk2dUtil.label = string.Empty;
	}

	public static void DestroyImmediate(UnityEngine.Object obj)
	{
		if (obj == null)
		{
			return;
		}
		UnityEngine.Object.DestroyImmediate(obj);
	}

	public static GameObject CreateGameObject(string name)
	{
		return new GameObject(name);
	}

	public static Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.MarkDynamic();
		return mesh;
	}

	public static T AddComponent<T>(GameObject go) where T : Component
	{
		return go.AddComponent<T>();
	}

	public static void SetActive(GameObject go, bool active)
	{
		if (active == go.activeSelf)
		{
			return;
		}
		go.SetActive(active);
	}

	public static void SetTransformParent(Transform t, Transform parent)
	{
		t.parent = parent;
	}

	public static void SetDirty(UnityEngine.Object @object)
	{
	}

	private static string label = string.Empty;

	private static bool undoEnabled;
}
