using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoSingleton<PackageManager> {

    public int maxCapacity;
    private int currentCapacity;
    public int CurrentCapacity
    {
        get { return currentCapacity; }
        set
        {
            EventDispatcher.Outer.DispatchEvent(EventConst.EVENT_OnCapacityChanges, value);
            currentCapacity = value;
            if (currentCapacity > maxCapacity)
                currentCapacity = maxCapacity;
        }
    }

    public void Init()
    {
        currentCapacity = maxCapacity;
    }

    public bool HasEnoughCapacity(int capacityNeed)
    {
        if (CurrentCapacity < capacityNeed)
            return false;
        return true;
    }

    public void OnPackageUse(int usedCapacity)
    {
        CurrentCapacity -= usedCapacity;
    }

    public void ResetCapacity()
    {
        CurrentCapacity = maxCapacity;
    }

}
