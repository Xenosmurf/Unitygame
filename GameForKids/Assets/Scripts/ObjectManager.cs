using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ObjectManager : MonoBehaviour
{
    public static ObjectManager om;



    [SerializeField] public GameObject content;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private GameObject place;

    [SerializeField]
    public InventoryDataBase inventoryData;

    [SerializeField]
    public GameObject scroll;
   
    //[SerializeField]
    //public GameObject panel;

    [SerializeField]
    public Grid grid;

    [HideInInspector]
    public string selectedObject;

    [SerializeField]
    public ObjectDataBaseSO database;

    [SerializeField] 
    public PlacedObjectDatabase placementDatabase;

    //[SerializeField]
    //public InputManager inputManager;

    [SerializeField] public LayerMask placementLayerMask;

    [SerializeField] public Camera sceneCamera;
    [SerializeField] public GameObject cellIndicator;

    [SerializeField] public SavePlacement savePlacement;

    [SerializeField]
    private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;
    //[SerializeField]
    //private PreviewSystem preview;

    // public string objectName;
    public bool placeMode = false;

    //public void ChangeAmount(int ID)
    //{
    //    Rect content = scrollView.content;

    //    GameObject[] cnildren = content.GetComponent
       
    //}

    void Start()
    {
        //panel.SetActive(false);
        ActivateGridVisualiasion(false);
        cellIndicator.SetActive(false);
        scroll.SetActive(true);
       
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        //cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        if(savePlacement.placementDatabase != null)
        {
            savePlacement.InstantiatePrevious();
        }
        om = this;
    }

    public int GetCount()
    {
        return placementDatabase.savedObjects.Count;
    }
    public void ChangeAmount(int ID)
    {

        for (int i = 0; i < inventoryData.objectsData.Count; i++)
        {
            if (inventoryData.objectsData[i].ID == ID)
            {
                inventoryData.objectsData[i].Amount++;
                return;
            }
        }

    }

    public void UpdateTheContent(int ID)
    {
        Transform[] objects = content.GetComponentsInChildren<Transform>();
        if (objects != null) {
            for (int i = 0; i < inventoryData.objectsData.Count; i++)
            {
                foreach (Transform obj in objects)
                {
                    if (obj.GetComponentInChildren<Button>() != null)
                    {
                        Button button = obj.GetComponentInChildren<Button>();
                        if (inventoryData.objectsData[i].ID == ID && inventoryData.objectsData[i].ID == int.Parse(GetStringBeforeDot(button.name)))
                        {
                            inventoryData.objectsData[i].Amount++;
                            TextMeshProUGUI amountText = obj.GetComponentInChildren<TextMeshProUGUI>();
                            amountText.text = inventoryData.objectsData[i].Amount.ToString();
                            return;
                        }
                    }
                }
            }
        }
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


    public int Count()
    {
        return placementDatabase.savedObjects.Count;
    }

    public void ActScroll(bool active)
    {
        scroll.SetActive(active);

    }

    public void DeletePlace()
    {
        try
        {
            //HideObjectTool();
           
            Destroy(GameObject.Find(place.name));
        }
        catch (Exception)
        {
            Debug.Log("Object not found!");
        }
    }

    public void PreparePreview(GameObject previewObject)
    {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }

        // Remove the collider from the clone
      
    }

    public void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    public void ApplyFeedbackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        cellIndicatorRenderer.material.color = c;
        c.a = 0.5f;

    }



    //public void ActivatePanel(bool active)
    //{
    //    panel.SetActive(active);
    //}

    public void ActivateCell(bool active)
    {
        cellIndicator.SetActive(active);
    }

    public void ActivateGridVisualiasion(bool active)
    {
        gridVisualization.SetActive(active);
    }

  

    public void RotateLeft()
    {
        try
        {
            GameObject.Find(selectedObject).transform.Rotate(0.0f, -45f, 0.0f, Space.World);
        }
        catch (Exception )
        {
            Debug.Log("Object not found!");
        }
    }

    public void RotateRight()
    {
        try
        {
            GameObject.Find(selectedObject).transform.Rotate(0.0f, 45f, 0.0f, Space.World);
        }
        catch (Exception )
        {
            Debug.Log("Object not found!");
        }
    }

    public void RemoveObject()
    {
        try
        {
            //HideObjectTool();
            savePlacement.RemoveFromDataBase(selectedObject);
            Destroy(GameObject.Find(selectedObject));
        }
        catch (Exception )
        {
            Debug.Log("Object not found!");
        }
    }

    //public void ShowObjectChooser()
    //{
    //    objectChooser.SetActive(true);
    //}

    //public void HideObjectChooser()
    //{
    //    objectChooser.SetActive(false);
    //}

    //public void ShowObjectTool()
    //{
    //    objectTool.SetActive(true);
    //}

    //public void HideObjectTool()
    //{
    //    objectTool.SetActive(false);
    //}

    //public void AddObjectToScene()
    //{
    //    GameObject newObjectForScene = Instantiate(Resources.Load(objectName) as GameObject);
    //    newObjectForScene.name = newObjectForScene.name + UnityEngine.Random.Range(111, 999);
    //}

    //private void Update()
    //{
    //    if (selectedObjectIndex < 0) { return; }
    //    Vector3 mousePosition = inputManager.GetSelectedMapPosition(database.objectsData[selectedObjectIndex].Size);
    //    Vector3Int gridPosition = grid.WorldToCell(mousePosition);
    //    Vector2Int objectSize = database.objectsData[selectedObjectIndex].Size;

    //    if (lastDetectedPosition != gridPosition)
    //    {
    //        Debug.Log("deb 1");
    //        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

    //        //mouse.transform.position = mousePosition;
    //        preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    //        lastDetectedPosition = gridPosition;
    //    }
    //}

}
