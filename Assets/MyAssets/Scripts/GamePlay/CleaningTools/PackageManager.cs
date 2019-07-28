using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoSingleton<PackageManager> {

    public int maxCapcity;
    private int currentCapcity;
    public int CurrentCapcity
    {
        get { return currentCapcity; }
        set
        {
            currentCapcity = value;
            if (currentCapcity > maxCapcity)
                currentCapcity = maxCapcity;
        }
    }

    public bool IsFull
    {
        get { return currentCapcity >= maxCapcity; }
    }

}
