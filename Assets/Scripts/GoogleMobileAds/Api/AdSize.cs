// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.AdSize
using System;

namespace GoogleMobileAds.Api
{
	public class AdSize
	{
		public AdSize(int width, int height)
		{
			this.isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		private AdSize(bool isSmartBanner) : this(0, 0)
		{
			this.isSmartBanner = isSmartBanner;
		}

		public int Width
		{
			get
			{
				return this.width;
			}
		}

		public int Height
		{
			get
			{
				return this.height;
			}
		}

		public bool IsSmartBanner
		{
			get
			{
				return this.isSmartBanner;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			AdSize adSize = (AdSize)obj;
			return this.width == adSize.width && this.height == adSize.height && this.isSmartBanner == adSize.isSmartBanner;
		}

		public static bool operator ==(AdSize a, AdSize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(AdSize a, AdSize b)
		{
			return !a.Equals(b);
		}

		public override int GetHashCode()
		{
			int num = 71;
			int num2 = 11;
			int num3 = num;
			num3 = (num3 * num2 ^ this.width.GetHashCode());
			num3 = (num3 * num2 ^ this.height.GetHashCode());
			return num3 * num2 ^ this.isSmartBanner.GetHashCode();
		}

		private bool isSmartBanner;

		private int width;

		private int height;

		public static readonly AdSize Banner = new AdSize(320, 50);

		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		public static readonly AdSize IABBanner = new AdSize(468, 60);

		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		public static readonly AdSize SmartBanner = new AdSize(true);

		public static readonly int FullWidth = -1;
	}
}
