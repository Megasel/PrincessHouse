// dnSpy decompiler from Assembly-CSharp.dll class: Exceptions.PropertyOrFieldNotFoundException
using System;
using System.Runtime.Serialization;

namespace Exceptions
{
	[Serializable]
	public class PropertyOrFieldNotFoundException : Exception
	{
		public PropertyOrFieldNotFoundException()
		{
		}

		public PropertyOrFieldNotFoundException(string message) : base(message)
		{
		}

		public PropertyOrFieldNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		protected PropertyOrFieldNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
