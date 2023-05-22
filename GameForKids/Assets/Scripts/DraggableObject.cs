using UnityEngine.EventSystems;
using UnityEngine;


public class DraggableObject : MonoBehaviour
{
    //private bool isDragging = false;
    private Vector3 screenPoint;
    private Vector3 offset;

    private int count = 0;
    private GameObject instantiatedObject; // Reference to the instantiated object
    private ObjectData selectedObjectData;
    private Vector2Int objectSize;

    private bool validInUp = true;

    private Vector3 lastposition;

    private float doubleClickTimeThreshold = 0.5f;
    private float lastClickTime = 0.1f;


    //private GameObject cellIndicator;


    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Time.time - lastClickTime < doubleClickTimeThreshold)
            {
                // Double-click detected
                HandleDoubleClick();
                return;
            }
            //cellIndicator.SetActive(true);
            // Instantiate a new instance of the prefab
            ObjectManager.om.DeletePlace();
            ObjectManager.om.ActivateCell(true);
            ObjectManager.om.ActScroll(false);
            //cellIndicator = ObjectManager.om.cellIndicator;
            
            instantiatedObject = Instantiate(gameObject, transform.position, transform.rotation);
            //ObjectManager.om.PreparePreview(instantiatedObject);
            if (ObjectManager.om.savePlacement.Exist(gameObject.name))
            {
                //instantiatedObject = Instantiate(gameObject, transform.position, transform.rotation);
                instantiatedObject.name = gameObject.name;
            }
            else
            {
                //instantiatedObject = Instantiate(gameObject, transform.position, transform.rotation);
                count = ObjectManager.om.GetCount();
                instantiatedObject.name = gameObject.name + ".new" + count.ToString();
            }

            selectedObjectData = ObjectManager.om.database.objectsData.Find(data => data.ID.ToString() == GetStringBeforeDot(instantiatedObject.name));
            int ID = selectedObjectData.ID;

            objectSize = ObjectManager.om.database.objectsData[ID].Size;

            //ObjectManager.om.preview.StartShowingPlacementPreview(instantiatedObject, objectSize);

            // Disable the collider of the instantiated object to prevent interference during dragging
            instantiatedObject.GetComponent<Collider>().enabled = false;

            // Store the position and rotation of the original prefab
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            
            count++;
        }
    }

    private void HandleDoubleClick()
    {
        ObjectManager.om.ApplyFeedback(false);
        ObjectManager.om.UpdateTheContent(int.Parse(GetStringBeforeDot(gameObject.name)));
        ObjectManager.om.RemoveObject();
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


    private void OnMouseDrag()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && instantiatedObject != null)
        {
            //isDragging = true;

            int minX = -5;
            int maxX = 5 - objectSize.x;
            int minZ = -5;
            int maxZ = 5 - objectSize.y;
            float minY = 0.4f;
            float maxY = 0.4f;


            ObjectManager.om.PreparePreview(gameObject);

            // Calculate the new position based on mouse movement
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            // Snap the position to the nearest grid cell
            Vector3Int snappedPosition = ObjectManager.om.grid.WorldToCell(newPosition);
            newPosition = ObjectManager.om.grid.GetCellCenterWorld(snappedPosition);

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            

            newPosition.x = Mathf.RoundToInt(newPosition.x);
            newPosition.y = Mathf.RoundToInt(newPosition.y);
            newPosition.z = Mathf.RoundToInt(newPosition.z);

            Vector3 cellPosition = newPosition;

            cellPosition.y = Mathf.RoundToInt(newPosition.y+1);


            // Update the position of the instantiated object
            //instantiatedObject.transform.position = newPosition;
            bool validity = ObjectManager.om.savePlacement.CheckValidity(Vector3Int.RoundToInt(newPosition), objectSize);
           // ObjectManager.om.ApplyFeedback(validity);
           
            validInUp = validity;

            if (validity)
            {



               // Debug.Log("Valid positon :" + newPosition.ToString());
        }
        else
        {
            //gameObject.transform.position = newPosition;
            //instantiatedObject.transform.position = newPosition;
            //    ObjectManager.om.ApplyFeedback(validity);
            //    Debug.Log("InValid positon :" + newPosition.ToString());
        }
            //instantiatedObject.transform.position = newPosition;
            gameObject.transform.position = newPosition;
            instantiatedObject.transform.position = newPosition;
            ObjectManager.om.ApplyFeedback(validity);
            //Debug.Log("InValid positon :" + newPosition.ToString());
            ObjectManager.om.ApplyFeedback(validity);
            //gameObject.transform.position = newPosition;

            ObjectManager.om.cellIndicator.transform.localScale = new Vector3(objectSize.x, 1, objectSize.y);
            ObjectManager.om.cellIndicator.transform.position = newPosition;
            ObjectManager.om.ApplyFeedbackToCursor(validity);


            lastposition = newPosition;
            ObjectManager.om.ActivateGridVisualiasion(true);
        }
    }

    private void OnMouseUp()
    {
        //isDragging = false;
        ObjectManager.om.ActScroll(true);

        if (instantiatedObject != null)
        {
            ObjectManager.om.cellIndicator.SetActive(false);
            ObjectManager.om.ActivateGridVisualiasion(false);
            // Enable the collider of the instantiated object
            if (!validInUp)
            {
                ObjectManager.om.UpdateTheContent(int.Parse(GetStringBeforeDot(gameObject.name)));
                ObjectManager.om.savePlacement.RemoveFromDataBase(instantiatedObject.name);
                Destroy(instantiatedObject);
                //instantiatedObject.transform.position = lastposition;
                Debug.Log("Invalid positon :" + lastposition.ToString());
                Destroy(gameObject);
                

            }
            else
            {
                instantiatedObject.GetComponent<Collider>().enabled = true;
                selectedObjectData = null;
                objectSize = Vector2Int.one;
                lastposition = Vector3.one;
                // Destroy the original prefab


                ObjectManager.om.savePlacement.AddToDatabase();

                Destroy(gameObject);
            }

        }
    }



    //private void Update()
    //{
    //    if (isDragging)
    //    {

    //    }
    //}
}

