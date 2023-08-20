using System.Collections.Immutable;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var v1 = MyEnum.Value1;
        var v2 = MyEnum.Value2;

        Debug.Assert(v1 != v2);

        var v1_2 = MyEnum.Value1;

        Debug.Assert(v1 == v1_2);
    }
}

readonly partial struct MyEnum : IEquatable<MyEnum>
{
    public MyEnum(Int32 value)
    {
        Value = value;
    }

    public readonly Int32 Value;

    public static implicit operator Int32(MyEnum myEnum) => myEnum.Value;
    public static explicit operator MyEnum(Int32 value) => new MyEnum(value);

    public static MyEnum operator &(MyEnum a, MyEnum b) => new(a.Value & b.Value);
    public static MyEnum operator |(MyEnum a, MyEnum b) => new(a.Value | b.Value);

    public static Boolean operator ==(MyEnum left, MyEnum right) => left.Equals(right);
    public static Boolean operator !=(MyEnum left, MyEnum right) => !(left == right);

    public override Boolean Equals(Object? obj) => obj is MyEnum @enum && Equals(@enum);
    public Boolean Equals(MyEnum other) => Value == other.Value;
    public override Int32 GetHashCode() => HashCode.Combine(Value);
}

readonly partial struct MyEnum
{
    public static readonly MyEnum Value1 = new(1);
}

readonly partial struct MyEnum
{
    public static readonly MyEnum Value2 = new(2);
}