using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Shippart : ScriptableObject
{
   

    public bool isBroken;
    public Ship.PartType partType;

    public void init(Ship.PartType type, bool broken)
    {

        isBroken = broken;
        partType = type;


    }

}
