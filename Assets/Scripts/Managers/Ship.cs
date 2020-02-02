using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public enum PartType
    {
        LeftWing,
        RightWing,
        Cockpit,
        Reactor,
        Body
    }

    public int brokenPart = 0;
    public int twistedPart = 0;
    public int unpaintedPart = 0;
    public int repairedPart = 0;

    public List<PartType> partTypeToSetup;
    public List<Shippart> listOfParts;
    public List<GameObject> physicalParts;

    private void Awake()
    {
        brokenPart = 0;
        partTypeToSetup.Add(PartType.Body);
        partTypeToSetup.Add(PartType.LeftWing);
        partTypeToSetup.Add(PartType.RightWing);
        partTypeToSetup.Add(PartType.Reactor);
        partTypeToSetup.Add(PartType.Cockpit);
        GenerateBrokenSpaceshipNV(6, 3);
        //GenerateBrokenSpaceship(5, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateBrokenSpaceshipNV(int totalPart, int minBrokenPart)
    {
        var children = this.GetComponentsInChildren<Repairable>();

        foreach (var child in children)
        {
            var rand = Random.Range(0, 4);
            
            child.currentState = (RepairState)rand;

            switch (child.currentState)
            {
                case RepairState.Repaired:
                    repairedPart++;
                    break;
                case RepairState.Twisted:
                    twistedPart++;
                    break;
                case RepairState.Unpainted:
                    unpaintedPart++;
                    break;
                case RepairState.Damaged:
                    brokenPart++;
                    break;
                default:
                    break;
            }
        }

        if (twistedPart + brokenPart + unpaintedPart < minBrokenPart)
        {
            Debug.Log("NOT ENOUGH BROKE");
            var sum = twistedPart + brokenPart + unpaintedPart;
            foreach (var child in children)
            {
                if (child.currentState != RepairState.Repaired)
                {
                    Debug.Log("ADD BROKEN PART");
                    child.currentState = (RepairState)Random.Range(1, 4);
                    sum++;
                    if (sum >= minBrokenPart)
                    {
                        break;
                    }
                }
            }
        }

        //brokenPart = twistedPart + brokenPart + unpaintedPart;
        Debug.Log("BROKEN PART : " + brokenPart);
    }

    void GenerateBrokenSpaceship(int totalPart, int maxBrokenPart)
    {
        //Generate 5 part 
        for(var i = 0; i < totalPart; i++)
        {
            //var index = Random.Range(0, partTypeToSetup.Capacity - 1);
            var isBroken = false;
            //remove the random
            var type = partTypeToSetup[i];
            //partTypeToSetup.RemoveAt(index);
            //Debug.Log("TYPE : " + type.ToString());
            if(brokenPart < maxBrokenPart)
            {
                if (Random.Range(0, 2) == 1)
                {
                    isBroken = true;
                    brokenPart++;
                }
                //Debug.Log("IS BROKEN  : " + isBroken.ToString());

            }

            Shippart currentShipPart = ScriptableObject.CreateInstance<Shippart>();
            currentShipPart.init(type, isBroken);

            listOfParts.Add(currentShipPart);
        }

        if(brokenPart==0)
        {
            listOfParts[Random.Range(0, 5)].isBroken = true;
        }

        //SET THE GAMEOBJECT RESPECTIVLY
        foreach(Transform childs in this.transform)
        {
            physicalParts.Add(childs.gameObject);
        }
    }

}
