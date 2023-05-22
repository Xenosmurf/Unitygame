using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryDataBase : ScriptableObject
{
        public List<InventoryData> objectsData;
    }

[Serializable]
public class InventoryData
{
    public InventoryData(string name, int iD, int amount, GameObject buttonPrefab)
    {
        Name = name;
        ID = iD;
        Amount = amount;
        this.ButtonPrefab = buttonPrefab;
    }

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public int Amount { get; set; }

    [field: SerializeField]
    public GameObject ButtonPrefab { get; private set; }
    }

