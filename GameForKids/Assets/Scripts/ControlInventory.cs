using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlInventory : MonoBehaviour
{
    [SerializeField]
    public InventoryDataBase inventory;

    [SerializeField]
    public GameObject prefabButton;

    [SerializeField] 
    private ObjectDataBaseSO database;

    public void Start()
    {
       if(inventory != null) 
        { 
            GenerateItems(); 
        }
    }

    public void GenerateItems()
    {
        foreach (InventoryData obj in inventory.objectsData)
        {
            GameObject newItem = Instantiate(prefabButton);
            newItem.name = obj.ID.ToString();

            Button button = newItem.GetComponentInChildren<Button>();
            TextMeshProUGUI priceText = button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI amountText = newItem.GetComponentInChildren<TextMeshProUGUI>();

            Image imageComponent = newItem.GetComponentInChildren<Image>();

            button.name = obj.ID.ToString();
            imageComponent.sprite = database.objectsData[obj.ID].Sprite;
            priceText.text = obj.Amount.ToString();
            //priceText.gameObject.SetActive(false);

            //amountText.text = obj.Amount.ToString();
            amountText.gameObject.SetActive(false);
            newItem.transform.SetParent(GameObject.FindWithTag("stock").transform, false);
           
        }
    }

}
