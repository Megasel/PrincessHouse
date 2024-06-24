// dnSpy decompiler from Assembly-CSharp.dll class: Anima2D.IkCCD2D
using System;
using UnityEngine;

namespace Anima2D
{
	public class IkCCD2D : Ik2D
	{
		protected override IkSolver2D GetSolver()
		{
			return this.m_Solver;
		}

		protected override void OnIkUpdate()
		{
			base.OnIkUpdate();
			this.m_Solver.iterations = this.iterations;
			this.m_Solver.damping = this.damping;
		}

		public int iterations = 10;

		[Range(0f, 1f)]
		public float damping = 0.8f;

		[SerializeField]
		private IkSolver2DCCD m_Solver = new IkSolver2DCCD();
	}
}
