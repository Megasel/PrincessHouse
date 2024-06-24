// dnSpy decompiler from Assembly-CSharp.dll class: Exceptions.NumericTypeExpectedException
using System;
using System.Runtime.Serialization;

namespace Exceptions
{
	[Serializable]
	public class NumericTypeExpectedException : Exception
	{
		public NumericTypeExpectedException()
		{
		}

		public NumericTypeExpectedException(string message) : base(message)
		{
		}

		public NumericTypeExpectedException(string message, Exception inner) : base(message, inner)
		{
		}

		protected NumericTypeExpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
