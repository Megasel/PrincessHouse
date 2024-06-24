// dnSpy decompiler from Assembly-CSharp.dll class: Area730.MoreAppsPage.Utils
using System;
using System.IO;
using UnityEngine;

namespace Area730.MoreAppsPage
{
	internal class Utils
	{
		public static string GetSettingsFilePath()
		{
			return Path.Combine(Application.persistentDataPath, "Area730_MoreApps.json");
		}

		public static bool ConfigFileExists()
		{
			return File.Exists(Utils.GetSettingsFilePath());
		}

		public static Texture2D LoadPNG(string filePath)
		{
			Texture2D texture2D = null;
			if (File.Exists(filePath))
			{
				byte[] data = File.ReadAllBytes(filePath);
				texture2D = new Texture2D(2, 2);
				texture2D.LoadImage(data);
			}
			return texture2D;
		}

		public static Sprite SpriteFromTex2d(Texture2D tex)
		{
			Texture2D texture2D = new Texture2D(tex.width, tex.height, tex.format, false);
			Color[] pixels = tex.GetPixels(0, 0, tex.width, tex.height);
			texture2D.SetPixels(pixels);
			texture2D.Apply();
			return Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 40f);
		}

		public static string GetImagePath(int index)
		{
			return Path.Combine(Application.persistentDataPath, "MoreAppIcon_" + index + ".png");
		}

		public static Sprite GetSprite(int index)
		{
			return Utils.SpriteFromTex2d(Utils.LoadPNG(Utils.GetImagePath(index)));
		}

		public const string MORE_APPS_FILENAME = "Area730_MoreApps.json";
	}
}
