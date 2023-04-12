using System.Collections;
using System.Collections.Generic;

public class ItemParameter
{
    /// <summary>
    /// 0~5
    /// </summary>
    public int typeIndex;

    public int intValue { get; set; }
    public int[] intArrayValue { get; set; }
    public float floatValue { get; set; }
    public float[] floatArrayValue { get; set; }
    public string stringValue { get; set; }
    public string[] stringArrayValue { get; set; }

    private ItemParameter() { }

    public ItemParameter(int value)
    {
        typeIndex = 0;
        intValue = value;
    }



    public string ToPrefsString()
    {
        switch (typeIndex)
        {
            case 0:
                return $"{typeIndex}:{intValue}";
            case 1:
                return $"{typeIndex}:{intArrayValue}";
            case 2:
                return $"{typeIndex}:{floatValue}";
            case 3:
                return $"{typeIndex}:{floatArrayValue}";
            case 4:
                return $"{typeIndex}:{stringValue}";
            case 5:
                return $"{typeIndex}:{stringArrayValue}";
            default:
                DLog.LogError($"wrong type {typeIndex}");
                return "";

        }
    }

    public ItemParameter(string[] prefsA)
    {
        typeIndex = int.Parse(prefsA[1]);
        switch (typeIndex)
        {
            case 0:
                intValue = int.Parse(prefsA[2]);
                break;
            case 1:
                var arr = prefsA[2].Split(';');
                intArrayValue = new int[arr.Length]; 
                for (int i = 0, len = arr.Length; i < 0; i++)
                {
                    intArrayValue[i] = int.Parse(arr[i]);
                }
                break;
            case 2:
                floatValue = float.Parse(prefsA[2]);
                break;
            case 3:
                arr = prefsA[2].Split(';');
                floatArrayValue = new float[arr.Length];
                for (int i = 0, len = arr.Length; i < 0; i++)
                {
                    floatArrayValue[i] = float.Parse(arr[i]);
                }
                break;
            case 4:
                stringValue = prefsA[2];
                break;
            case 5:
                stringArrayValue = prefsA[2].Split(';');
                break;
            default:
                DLog.LogError($"wrong type {prefsA[1]}");
                break;

        }

    }
}
