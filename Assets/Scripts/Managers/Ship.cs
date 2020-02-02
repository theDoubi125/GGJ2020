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

    public int brokenPart;
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

        GenerateBrokenSpaceship(5, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
      


        
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
