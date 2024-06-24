// dnSpy decompiler from Assembly-CSharp.dll class: GoogleMobileAds.Api.Mediation.MediationExtras
using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api.Mediation
{
	public abstract class MediationExtras
	{
		public MediationExtras()
		{
			this.Extras = new Dictionary<string, string>();
		}

		public Dictionary<string, string> Extras { get; protected set; }

		public abstract string AndroidMediationExtraBuilderClassName { get; }

		public abstract string IOSMediationExtraBuilderClassName { get; }
	}
}
