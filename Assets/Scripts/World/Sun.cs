using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun
{
    public static Vector2 direction = new Vector2(-1, 2).normalized;
    public static float angleMultiplier = 0.7f;
    public static bool generateShadows = true;

    // Display effect of being in the sun
    public static float sunBurnActive = 0;
    
    // The current intensity of the sun
    public static int sunIntensity = 1;
}
