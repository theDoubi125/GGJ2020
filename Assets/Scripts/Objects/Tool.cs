using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tool : MonoBehaviour
{
    public enum ToolType {None, Wrench, Hammer}
    public ToolType toolType;
    public Transform droppedVersionPrefab;
}
