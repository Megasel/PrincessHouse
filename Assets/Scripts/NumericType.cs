// dnSpy decompiler from Assembly-CSharp.dll class: NumericType
using System;
using Exceptions;
using Utilities;

public class NumericType : IEquatable<NumericType>
{
	public NumericType(object obj)
	{
		if (!obj.IsNumbericType())
		{
			throw new NumericTypeExpectedException("The type of object in the NumericType constructor must be numeric.");
		}
		this.value = obj;
		this.type = obj.GetType();
	}

	public object GetValue()
	{
		return this.value;
	}

	public void SetValue(object newValue)
	{
		this.value = newValue;
	}

	public bool Equals(NumericType other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (!(obj is NumericType))
		{
			return this.GetValue() == obj;
		}
		return this.Equals(obj);
	}

	public override int GetHashCode()
	{
		return this.GetValue().GetHashCode();
	}

	public override string ToString()
	{
		return this.GetValue().ToString();
	}

	public static bool operator <(NumericType left, NumericType right)
	{
		object obj = left.GetValue();
		object obj2 = right.GetValue();
		switch (Type.GetTypeCode(left.type))
		{
		case TypeCode.SByte:
			return (int)((sbyte)obj) < (int)((sbyte)obj2);
		case TypeCode.Byte:
			return (byte)obj < (byte)obj2;
		case TypeCode.Int16:
			return (short)obj < (short)obj2;
		case TypeCode.UInt16:
			return (ushort)obj < (ushort)obj2;
		case TypeCode.Int32:
			return (int)obj < (int)obj2;
		case TypeCode.UInt32:
			return (uint)obj < (uint)obj2;
		case TypeCode.Int64:
			return (long)obj < (long)obj2;
		case TypeCode.UInt64:
			return (ulong)obj < (ulong)obj2;
		case TypeCode.Single:
			return (float)obj < (float)obj2;
		case TypeCode.Double:
			return (double)obj < (double)obj2;
		case TypeCode.Decimal:
			return (decimal)obj < (decimal)obj2;
		default:
			throw new NumericTypeExpectedException("Please compare valid numeric types.");
		}
	}

	public static bool operator >(NumericType left, NumericType right)
	{
		object obj = left.GetValue();
		object obj2 = right.GetValue();
		switch (Type.GetTypeCode(left.type))
		{
		case TypeCode.SByte:
			return (int)((sbyte)obj) > (int)((sbyte)obj2);
		case TypeCode.Byte:
			return (byte)obj > (byte)obj2;
		case TypeCode.Int16:
			return (short)obj > (short)obj2;
		case TypeCode.UInt16:
			return (ushort)obj > (ushort)obj2;
		case TypeCode.Int32:
			return (int)obj > (int)obj2;
		case TypeCode.UInt32:
			return (uint)obj > (uint)obj2;
		case TypeCode.Int64:
			return (long)obj > (long)obj2;
		case TypeCode.UInt64:
			return (ulong)obj > (ulong)obj2;
		case TypeCode.Single:
			return (float)obj > (float)obj2;
		case TypeCode.Double:
			return (double)obj > (double)obj2;
		case TypeCode.Decimal:
			return (decimal)obj > (decimal)obj2;
		default:
			throw new NumericTypeExpectedException("Please compare valid numeric types.");
		}
	}

	public static bool operator ==(NumericType left, NumericType right)
	{
		return !(left > right) && !(left < right);
	}

	public static bool operator !=(NumericType left, NumericType right)
	{
		return !(left > right) || !(left < right);
	}

	public static bool operator <=(NumericType left, NumericType right)
	{
		return left == right || left < right;
	}

	public static bool operator >=(NumericType left, NumericType right)
	{
		return left == right || left > right;
	}

	private object value;

	private Type type;
}
