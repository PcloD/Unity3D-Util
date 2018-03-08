using UnityEngine;
using System.Collections;
using System;

//继承自定义基数的类
public class StringValue : System.Attribute
{
    private string _value;
    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }
}

//使用枚举的值DataTypeId.Money，获取对应的Money字符串
public class StringEnum
{
    public static string GetStringValue(Enum value)
    {
        string output = null;
        Type type = value.GetType();

        System.Reflection.FieldInfo fi = type.GetField(value.ToString());
        StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
        if (attrs.Length > 0)
        {
            output = attrs[0].Value;
        }
        return output;
    }

}
