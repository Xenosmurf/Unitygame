using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlacedObjectDatabase : ScriptableObject
{
   public  List<PlacedObject> savedObjects = new(); 
}

[Serializable]
public class PlacedObject
{
    public PlacedObject(string name, int iD, Vector2Int size, Vector3Int position, GameObject prefab)
    {
        Name = name;
        ID = iD;
        Size = size;
        Position = position;
        Prefab = prefab;
    }

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public Vector2Int Size { get; set; } = Vector2Int.one;

    [field: SerializeField]
    public Vector3Int Position { get;  set; } = Vector3Int.zero;

    [field: SerializeField]
    public GameObject Prefab { get;  set; }
}
