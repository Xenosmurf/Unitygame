
using UnityEngine;
using UnityEngine.UI;

public class PrefabManger : MonoBehaviour
{
    public static PrefabManger prefabManger;

    [SerializeField] public GameObject placeForPrefab;
    private bool placeForPrefabAct = false;
    [SerializeField] 
    public Camera sceneCamera;
    [SerializeField] 
    public ScrollRect scroll;
    [SerializeField] 
    public Material materialPlace;
    [SerializeField] 
    public GameObject scrollObject;
    private Vector3 pos = new Vector3Int(14, -1, -13);
    [SerializeField]
    public PlacedObjectDatabase placementDatabase;
    [SerializeField]
    public ObjectDataBaseSO database;
    [SerializeField]
    public InventoryDataBase inventoryData;

    public int exists = 0;
   
    public void ActivateScroll(bool active)
    {
        scrollObject.SetActive(active);
    }

    public void Start()
    {
        exists = placementDatabase.savedObjects.Count;
        prefabManger = this;
    }

    public GameObject GetPref(int ID)
    {
        //Debug.Log("PR%^ : " + ID);
        return database.objectsData[ID].Prefab;
    }

    public void GetAmount(int ID)
    {
        
        for (int i = 0; i < inventoryData.objectsData.Count; i++)
        {
            if (inventoryData.objectsData[i].ID == ID)
            {
                inventoryData.objectsData[i].Amount--;
                return;
            }
        }
     
    }

    public int GetCurrentAmount(int ID)
    {
        int amount = 0;

        for (int i = 0; i < inventoryData.objectsData.Count; i++)
        {
            if (inventoryData.objectsData[i].ID == ID)
            {
                amount =  inventoryData.objectsData[i].Amount;
                
            }
        }
        return amount;
    }



    public void ActivateObjects(Vector3 defaultPosition, Vector3 newPosition )
    {
        if (!placeForPrefabAct)
        {
            GameObject place = Instantiate( placeForPrefab, pos, Quaternion.identity);
            place.name = placeForPrefab.name;
            Renderer renderer = place.GetComponentInChildren<Renderer>();
            renderer.material = materialPlace;
            scroll.gameObject.SetActive(true);
            placeForPrefabAct = true;
            sceneCamera.transform.position = newPosition;
        }
        else
        {
            Destroy(placeForPrefab);
            scroll.gameObject.SetActive(false);
            placeForPrefabAct = false;
            sceneCamera.transform.position = defaultPosition;
        }
    }

}
