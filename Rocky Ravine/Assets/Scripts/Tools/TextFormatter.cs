using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextFormatter
{
    public static string GetDistance (int i) {
        if (i >= 1000) {
            return (i / 1000f).ToString("0.0") + " km";
        }
        return i.ToString() + " m";
    }
}
