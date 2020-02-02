using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipPart
{  
    LeftWing, RightWing, Reactor, Cockpit
}

public class ShipSocket : MonoBehaviour
{
    public ShipPart shipPart;
    public GameObject toReactivate;

    public void FillSocket(ShipElement shipElement)
    {
        toReactivate.GetComponent<Repairable>().SpawnVersion(shipElement.GetComponent<Repairable>().currentState);
        Destroy(shipElement.gameObject);
        toReactivate.SetActive(true);
    }
}
