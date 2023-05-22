
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem shopSystem;

    [SerializeField] private InventoryDataBase inventoryData;

    [SerializeField] private ObjectDataBaseSO database;

    [SerializeField] private GameObject prefabButton;

    //[SerializeField] private TextMeshProUGUI amountField;
    //[SerializeField] private TextMeshProUGUI priceField;
   // [SerializeField] private int itemID;
    public int objIndex;

    private void Start()
    {
        objIndex = 0;
        shopSystem = this;
        GenerateItems();
       // amountField.gameObject.SetActive(false);
        //ShowPrice(itemID);
    }

    //public void ShowPrice(int ID)
    //{
    //    ObjectData objectShop = database.objectsData[ID];
    //    priceField.text = objectShop.Price.ToString() + '$';

    //}
    public void BuyItem(int ID)
    {
        int balance = ScorePlayer.scorePlayer.GetCurrent();

        Debug.Log(ID.ToString());
        ObjectData objectToInventory = database.objectsData[ID];
        
        bool exists = inventoryData.objectsData.Exists(item => item.ID == ID);
        if(balance < objectToInventory.Price)
        {
            
            ScorePlayer.scorePlayer.notAffordable = true;
            return;
        }
        if (exists)
        {
           
            for(int i = 0; i < inventoryData.objectsData.Count; i++)
            {
                if (inventoryData.objectsData[i].ID == ID)
                {
                    inventoryData.objectsData[i].Amount++;
                    ScorePlayer.scorePlayer.Bought(objectToInventory.Price);
                    return;
                }
            }
           
            return;
        }
        
        inventoryData.objectsData.Add(new InventoryData(objectToInventory.Name, objectToInventory.ID, 1, prefabButton));
        ScorePlayer.scorePlayer.Bought(objectToInventory.Price);
    }

    public void GenerateItems()
    {
        foreach(ObjectData obj in database.objectsData)
        {
            GameObject newItem = Instantiate(prefabButton);
            newItem.name = objIndex.ToString();

            Button button = newItem.GetComponentInChildren<Button>();
            TextMeshProUGUI priceText = button.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI amountText = newItem.GetComponentInChildren<TextMeshProUGUI>();

            Image imageComponent = newItem.GetComponentInChildren<Image>();

            button.name = objIndex.ToString();
            imageComponent.sprite = obj.Sprite;
            priceText.text = "Price: " + obj.Price.ToString();

            amountText.gameObject.SetActive(false);
            newItem.transform.SetParent(GameObject.FindWithTag("shop").transform, false);
            objIndex++;
        }
    }

}
