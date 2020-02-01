using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    private Tool usedTool;

    public void SetTool(Transform prefab)
    {
        usedTool = Instantiate(prefab, transform).GetComponent<Tool>();
    }

    public void DropTool()
    {
        Instantiate(usedTool.droppedVersionPrefab, transform.position, transform.rotation);
        Destroy(usedTool.gameObject);
    }

    public bool hasTool
    {
        get 
        {
            return usedTool != null;
        }
    }

    public Tool.ToolType usedToolType
    {
        get
        {
            return usedTool != null ? usedTool.toolType : Tool.ToolType.None;
        }
    }
}
