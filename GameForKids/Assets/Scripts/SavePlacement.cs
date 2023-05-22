using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlacement : MonoBehaviour
{
    [SerializeField] 
    public PlacedObjectDatabase placementDatabase;

    [SerializeField]
    public ObjectDataBaseSO database;

    [SerializeField]
    public Grid grid;

    private List<Vector3Int> occupiedCells = new List<Vector3Int>();

    private float minX = -5;
    private float minY = 0;
    private float minZ = -5;
    private float maxX = 5;
    private float maxY;
    private float maxZ = 5;

    public List<GameObject> CreateObjectList()
    {
        // Code to create the object list within the specified coordinate range
        // Iterate through your objects and check if their coordinates fall within the range
        // Add the objects that satisfy the condition to a new list or perform desired operations
        

        // Example implementation:
        List<GameObject> objectList = new List<GameObject>();

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Vector3 objPosition = obj.transform.position;
            if (objPosition.x >= minX && objPosition.x <= maxX &&
                objPosition.y >= minY && objPosition.y <= maxY &&
                objPosition.z >= minZ && objPosition.z <= maxZ)
            {
                if (obj.name.Contains("new"))
                {
                    objectList.Add(obj);
                }
            }
        }
        Debug.Log("Objects within the specified range: " + objectList.Count);

        return objectList;
    }

    public void AddToDatabase()
    {
        int objectIndex;
        List<GameObject> currentObjects = CreateObjectList();
       
        foreach (GameObject obj in currentObjects)
        {
            objectIndex = int.Parse(GetStringBeforeDot(obj.name));
            //public PlacedObject(string name, int iD, Vector2Int size, Vector3Int position, GameObject prefab)
            PlacedObject newObject = new PlacedObject(obj.name, objectIndex, GetObjectSize(objectIndex), Vector3Int.RoundToInt(obj.transform.position), GetObject(objectIndex));
            if (Exist(newObject.Name))
            {
                placementDatabase.savedObjects.RemoveAll(obj => obj.Name == newObject.Name);
                placementDatabase.savedObjects.Add(newObject);

            }
            else
            {
                placementDatabase.savedObjects.Add(newObject);
            }
        }
       
    }

    public int PlacedNull()
    {
        return placementDatabase.savedObjects.Count;
    }

    public void RemoveFromDataBase(string name)
    {
        placementDatabase.savedObjects.RemoveAll(obj => obj.Name == name);
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }

    private void CalculateOccupiedCells()
    {
        occupiedCells.Clear();
        foreach (PlacedObject obj in placementDatabase.savedObjects)
        {
            List<Vector3Int> positionsToOccupy = CalculatePositions(obj.Position, obj.Size);
               foreach(Vector3Int position in positionsToOccupy)
            {
                occupiedCells.Add(position);
            }
            
        }
    }

  

    public bool CheckValidity(Vector3Int gridPosition, Vector2Int size)
    {
        CalculateOccupiedCells();
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, size);

        foreach(Vector3Int pos in positionToOccupy)
            {
                if (occupiedCells.Contains(pos))
                {
                    return false;
                }
            }
      
        return true;
    }


    
    public void InstantiatePrevious()
    {
        foreach(PlacedObject placedObject in placementDatabase.savedObjects)
        {
            GameObject instantiatedObject = Instantiate(placedObject.Prefab, placedObject.Position, Quaternion.identity);
            instantiatedObject.name = placedObject.Name;
        }
    }

    public bool Exist(string name)
    {
        foreach (PlacedObject obj in placementDatabase.savedObjects)
        {
            if (obj.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    public Vector2Int GetObjectSize(int ID)
    {
        return database.objectsData[ID].Size;
    }

    public GameObject GetObject(int ID)
    {
        return database.objectsData[ID].Prefab;
    }
    public string GetStringBeforeDot(string input)
    {
        int dotIndex = input.IndexOf('.');
        if (dotIndex >= 0)
        {
            return input.Substring(0, dotIndex);
        }
        else
        {
            return input; // If no dot is found, return the original input string
        }
    }

}
