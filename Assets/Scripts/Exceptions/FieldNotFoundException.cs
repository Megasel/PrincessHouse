// dnSpy decompiler from Assembly-CSharp.dll class: Exceptions.FieldNotFoundException
using System;
using System.Runtime.Serialization;

namespace Exceptions
{
	[Serializable]
	public class FieldNotFoundException : Exception
	{
		public FieldNotFoundException()
		{
		}

		public FieldNotFoundException(string message) : base(message)
		{
		}

		public FieldNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		protected FieldNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
