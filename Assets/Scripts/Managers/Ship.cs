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

    public List<PartType> partTypeToSetup;
    public int brokenPart;
    public List<Shippart> listOfParts;
    public List<GameObject> physicalParts;

    // Start is called before the first frame update
    void Start()
    {
        brokenPart = 0;
        partTypeToSetup.Add(PartType.Body);
        partTypeToSetup.Add(PartType.LeftWing);
        partTypeToSetup.Add(PartType.RightWing);
        partTypeToSetup.Add(PartType.Reactor);
        partTypeToSetup.Add(PartType.Cockpit);

        GenerateBrokenSpaceship(5,3);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateBrokenSpaceship(int totalPart, int maxBrokenPart)
    {
        //Generate 5 part 
        for(var i = 0; i < totalPart; i++)
        {
            var index = Random.Range(0, partTypeToSetup.Capacity - 1);
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

    //public void RemoveShipPart(PartType value)
    //{
    //    Debug.Log("VALUE REMOVE : " + value);
    //    switch (value)
    //    {
    //        case PartType.Body:
    //            this.physicalParts[0].SetActive(false);
    //            break;
    //        case PartType.LeftWing:
    //            this.physicalParts[1].SetActive(false);
    //            break;
    //        case PartType.RightWing:
    //            this.physicalParts[2].SetActive(false);
    //            break;
    //        case PartType.Reactor:
    //            this.physicalParts[3].SetActive(false);
    //            break;
    //        case PartType.Cockpit:
    //            this.physicalParts[4].SetActive(false);
    //            break;
    //        default:
    //            break;
    //    }
    //}

}
