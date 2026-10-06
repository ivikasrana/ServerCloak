using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCloak2
{
    public static class ServerCloakExt
    {
        private static T ChangeType<T>(object val) where T : struct, IConvertible
        {
            return (T)Convert.ChangeType(val, typeof(T));
        }

        public static T ToType<T>(this object val, T alt = default(T)) where T : struct, IConvertible
        {
            try
            {
                return val.ToTypeOrNull<T>().GetValueOrDefault(alt);
            }
            catch
            {
                return alt;
            }
        }

        public static T? ToTypeOrNull<T>(this object val, T? alt = null) where T : struct, IConvertible
        {
            var strVal = (Convert.ToString(val) ?? string.Empty).Trim();
            switch (typeof(T).Name)
            {
                case "Boolean":
                    bool vBool;
                    if (bool.TryParse(strVal, out vBool))
                        return ChangeType<T>(vBool);
                    break;
                case "Byte":
                    byte vByte;
                    if (byte.TryParse(strVal, out vByte))
                        return ChangeType<T>(vByte);
                    break;
                case "Char":
                    char vChar;
                    if (char.TryParse(strVal, out vChar))
                        return ChangeType<T>(vChar);
                    break;
                case "SByte":
                    sbyte vSByte;
                    if (sbyte.TryParse(strVal, out vSByte))
                        return ChangeType<T>(vSByte);
                    break;
                case "Int16":
                    short vShort;
                    if (short.TryParse(strVal, out vShort))
                        return ChangeType<T>(vShort);
                    break;
                case "Int32":
                    int vInt;
                    if (int.TryParse(strVal, out vInt))
                        return ChangeType<T>(vInt);
                    break;
                case "Int64":
                    long vLong;
                    if (long.TryParse(strVal, out vLong))
                        return ChangeType<T>(vLong);
                    break;
                case "Float":
                    float vFloat;
                    if (float.TryParse(strVal, out vFloat))
                        return ChangeType<T>(vFloat);
                    break;
                case "Double":
                    double vDouble;
                    if (double.TryParse(strVal, out vDouble))
                        return ChangeType<T>(vDouble);
                    break;
                case "Decimal":
                    decimal vDecimal;
                    if (decimal.TryParse(strVal, out vDecimal))
                        return ChangeType<T>(vDecimal);
                    break;
                case "DateTime":
                    DateTime vDateTime;
                    if (DateTime.TryParse(strVal, out vDateTime))
                        return ChangeType<T>(vDateTime);
                    break;
                case "TimeSpan":
                    TimeSpan vTimeSpan;
                    if (TimeSpan.TryParse(strVal, out vTimeSpan))
                        return ChangeType<T>(vTimeSpan);
                    break;
            }
            try
            {
                return ChangeType<T>(strVal);
            }
            catch { }
            return alt;
        }

    }
}
