using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStockScr : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 defaultPosition = new Vector3(1, 15, 10);
    private Vector3 newPosition = new Vector3(1, 12, 10);
    public void Act()
    {
        PrefabManger.prefabManger.ActivateObjects(defaultPosition, newPosition);
    }
}
