// dnSpy decompiler from Assembly-CSharp.dll class: Anima2D.IkLimb2D
using System;
using UnityEngine;

namespace Anima2D
{
	public class IkLimb2D : Ik2D
	{
		protected override IkSolver2D GetSolver()
		{
			return this.m_Solver;
		}

		protected override void Validate()
		{
			base.numBones = 2;
		}

		protected override int ValidateNumBones(int numBones)
		{
			return 2;
		}

		protected override void OnIkUpdate()
		{
			base.OnIkUpdate();
			this.m_Solver.flip = this.flip;
		}

		private void OnValidate()
		{
			base.numBones = 2;
		}

		public bool flip;

		[SerializeField]
		private IkSolver2DLimb m_Solver = new IkSolver2DLimb();
	}
}
