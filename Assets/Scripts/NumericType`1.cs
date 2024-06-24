// dnSpy decompiler from Assembly-CSharp.dll class: NumericType`1
using System;
using Exceptions;
using Utilities;

public class NumericType<T> : IEquatable<NumericType<T>>
{
	public NumericType(T obj)
	{
		if (!typeof(T).IsNumbericType())
		{
			throw new NumericTypeExpectedException("The type inputted into the NumericType generic must be a numeric type.");
		}
		this.type = typeof(T);
		this.value = obj;
	}

	public T GetValue()
	{
		return this.value;
	}

	public object GetValueAsObject()
	{
		return this.value;
	}

	public void SetValue(T newValue)
	{
		this.value = newValue;
	}

	public bool Equals(NumericType<T> other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		return (obj == null || obj is NumericType<T>) && this.Equals(obj);
	}

	public override int GetHashCode()
	{
		T t = this.GetValue();
		return t.GetHashCode();
	}

	public override string ToString()
	{
		T t = this.GetValue();
		return t.ToString();
	}

	public static bool operator <(NumericType<T> left, NumericType<T> right)
	{
		object valueAsObject = left.GetValueAsObject();
		object valueAsObject2 = right.GetValueAsObject();
		switch (Type.GetTypeCode(left.type))
		{
		case TypeCode.SByte:
			return (int)((sbyte)valueAsObject) < (int)((sbyte)valueAsObject2);
		case TypeCode.Byte:
			return (byte)valueAsObject < (byte)valueAsObject2;
		case TypeCode.Int16:
			return (short)valueAsObject < (short)valueAsObject2;
		case TypeCode.UInt16:
			return (ushort)valueAsObject < (ushort)valueAsObject2;
		case TypeCode.Int32:
			return (int)valueAsObject < (int)valueAsObject2;
		case TypeCode.UInt32:
			return (uint)valueAsObject < (uint)valueAsObject2;
		case TypeCode.Int64:
			return (long)valueAsObject < (long)valueAsObject2;
		case TypeCode.UInt64:
			return (ulong)valueAsObject < (ulong)valueAsObject2;
		case TypeCode.Single:
			return (float)valueAsObject < (float)valueAsObject2;
		case TypeCode.Double:
			return (double)valueAsObject < (double)valueAsObject2;
		case TypeCode.Decimal:
			return (decimal)valueAsObject < (decimal)valueAsObject2;
		default:
			throw new NumericTypeExpectedException("Please compare valid numeric types with numeric generics.");
		}
	}

	public static bool operator >(NumericType<T> left, NumericType<T> right)
	{
		object valueAsObject = left.GetValueAsObject();
		object valueAsObject2 = right.GetValueAsObject();
		switch (Type.GetTypeCode(left.type))
		{
		case TypeCode.SByte:
			return (int)((sbyte)valueAsObject) > (int)((sbyte)valueAsObject2);
		case TypeCode.Byte:
			return (byte)valueAsObject > (byte)valueAsObject2;
		case TypeCode.Int16:
			return (short)valueAsObject > (short)valueAsObject2;
		case TypeCode.UInt16:
			return (ushort)valueAsObject > (ushort)valueAsObject2;
		case TypeCode.Int32:
			return (int)valueAsObject > (int)valueAsObject2;
		case TypeCode.UInt32:
			return (uint)valueAsObject > (uint)valueAsObject2;
		case TypeCode.Int64:
			return (long)valueAsObject > (long)valueAsObject2;
		case TypeCode.UInt64:
			return (ulong)valueAsObject > (ulong)valueAsObject2;
		case TypeCode.Single:
			return (float)valueAsObject > (float)valueAsObject2;
		case TypeCode.Double:
			return (double)valueAsObject > (double)valueAsObject2;
		case TypeCode.Decimal:
			return (decimal)valueAsObject > (decimal)valueAsObject2;
		default:
			throw new NumericTypeExpectedException("Please compare valid numeric types.");
		}
	}

	public static bool operator ==(NumericType<T> left, NumericType<T> right)
	{
		return !(left > right) && !(left < right);
	}

	public static bool operator !=(NumericType<T> left, NumericType<T> right)
	{
		return !(left > right) || !(left < right);
	}

	public static bool operator <=(NumericType<T> left, NumericType<T> right)
	{
		return left == right || left < right;
	}

	public static bool operator >=(NumericType<T> left, NumericType<T> right)
	{
		return left == right || left > right;
	}

	private T value;

	private Type type;
}
