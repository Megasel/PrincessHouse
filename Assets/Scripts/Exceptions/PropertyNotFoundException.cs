// dnSpy decompiler from Assembly-CSharp.dll class: Exceptions.PropertyNotFoundException
using System;
using System.Runtime.Serialization;

namespace Exceptions
{
	[Serializable]
	public class PropertyNotFoundException : Exception
	{
		public PropertyNotFoundException()
		{
		}

		public PropertyNotFoundException(string message) : base(message)
		{
		}

		public PropertyNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		protected PropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
