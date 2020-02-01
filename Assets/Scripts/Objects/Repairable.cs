using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour
{
    public Tool.ToolType toolType;
    public Transform repairedPrefab;

    public void Repair()
    {
        Instantiate(repairedPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
