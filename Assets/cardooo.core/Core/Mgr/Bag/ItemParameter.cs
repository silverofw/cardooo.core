using System.Collections;
using System.Collections.Generic;
namespace cardooo.core
{
    public class ItemParameter
    {
        /// <summary>
        /// 0~5
        /// </summary>
        public int typeIndex { get; private set; }
        public BagParamType Type { get { return (BagParamType)typeIndex; } }

        public int intValue { get; private set; }
        public int[] intArrayValue { get; private set; }
        public float floatValue { get; private set; }
        public float[] floatArrayValue { get; private set; }
        public string stringValue { get; private set; }
        public string[] stringArrayValue { get; private set; }

        private ItemParameter() { }

        public ItemParameter(int value)
        {
            typeIndex = (int)BagParamType.INT;
            intValue = value;
        }
        public ItemParameter(int[] value)
        {
            typeIndex = (int)BagParamType.INT_ARR;
            intArrayValue = value;
        }
        public ItemParameter(float value)
        {
            typeIndex = (int)BagParamType.FLOAT;
            floatValue = value;
        }
        public ItemParameter(float[] value)
        {
            typeIndex = (int)BagParamType.FLOAT_ARR;
            floatArrayValue = value;
        }
        public ItemParameter(string value)
        {
            typeIndex = (int)BagParamType.STRING;
            stringValue = value;
        }
        public ItemParameter(string[] value)
        {
            typeIndex = (int)BagParamType.STRING_ARR;
            stringArrayValue = value;
        }

        public bool update(int value)
        {
            if (Type != BagParamType.INT)
                return false;
            intValue = value;
            return true;
        }
        public bool update(int[] value)
        {
            if (Type != BagParamType.INT_ARR)
                return false;
            intArrayValue = value;
            return true;
        }

        public bool update(float value)
        {
            if (Type != BagParamType.FLOAT)
                return false;
            floatValue = value;
            return true;
        }
        public bool update(float[] value)
        {
            if (Type != BagParamType.FLOAT_ARR)
                return false;
            floatArrayValue = value;
            return true;
        }

        public bool update(string value)
        {
            if (Type != BagParamType.STRING)
                return false;
            stringValue = value;
            return true;
        }
        public bool update(string[] value)
        {
            if (Type != BagParamType.STRING_ARR)
                return false;
            stringArrayValue = value;
            return true;
        }

        public string ToPrefsString()
        {
            switch ((BagParamType)typeIndex)
            {
                case BagParamType.INT:
                    return $"{typeIndex}:{intValue}";
                case BagParamType.INT_ARR:
                    var arrStr = "";
                    for (int i = 0,len = intArrayValue.Length; i<len; i++)
                    {
                        arrStr += intArrayValue[i];
                        if (i != len - 1)
                        {
                            arrStr += ';';
                        }
                    }
                    return $"{typeIndex}:{arrStr}";
                case BagParamType.FLOAT:
                    return $"{typeIndex}:{floatValue}";
                case BagParamType.FLOAT_ARR:
                    arrStr = "";
                    for (int i = 0, len = floatArrayValue.Length; i < len; i++)
                    {
                        arrStr += floatArrayValue[i];
                        if (i != len - 1)
                        {
                            arrStr += ';';
                        }
                    }
                    return $"{typeIndex}:{arrStr}";
                case BagParamType.STRING:
                    return $"{typeIndex}:{stringValue}";
                case BagParamType.STRING_ARR:
                    arrStr = "";
                    for (int i = 0, len = stringArrayValue.Length; i < len; i++)
                    {
                        arrStr += stringArrayValue[i];
                        if (i != len - 1)
                        {
                            arrStr += ';';
                        }
                    }
                    return $"{typeIndex}:{arrStr}";                    
                default:
                    DLog.LogError($"wrong type {typeIndex}");
                    return "";

            }
        }

        public ItemParameter(int typeIndex, string valueStr)
        {
            this.typeIndex = typeIndex;
            switch ((BagParamType)typeIndex)
            {
                case BagParamType.INT:
                    intValue = int.Parse(valueStr);
                    break;
                case BagParamType.INT_ARR:
                    var arr = valueStr.Split(';');
                    intArrayValue = new int[arr.Length];
                    for (int i = 0, len = arr.Length; i < len; i++)
                    {
                        intArrayValue[i] = int.Parse(arr[i]);
                    }
                    break;
                case BagParamType.FLOAT:
                    floatValue = float.Parse(valueStr);
                    break;
                case BagParamType.FLOAT_ARR:
                    arr = valueStr.Split(';');
                    floatArrayValue = new float[arr.Length];
                    for (int i = 0, len = arr.Length; i < len; i++)
                    {
                        floatArrayValue[i] = float.Parse(arr[i]);
                    }
                    break;
                case BagParamType.STRING:
                    stringValue = valueStr;
                    break;
                case BagParamType.STRING_ARR:
                    stringArrayValue = valueStr.Split(';');
                    break;
                default:
                    DLog.LogError($"wrong type {typeIndex}");
                    break;

            }

        }
    }
}