
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPrefabToScene : MonoBehaviour
{
    //public GameObject prefabToAdd;

    private float currentZ = 0f;
    private static int i = 0;

    private Vector3 defaultPosition = new Vector3(1, 15, 10);
    private Vector3 newPosition = new Vector3(1, 12, 10);
    public void OnButtonClick()
    {
        //PlayerPrefs.SetInt("Name", 1);

        if (PrefabManger.prefabManger != null)
        {
            PrefabManger.prefabManger.ActivateScroll(false);
            TextMeshProUGUI amountText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (PrefabManger.prefabManger.GetCurrentAmount(int.Parse(GetStringBeforeDot(gameObject.name))) == 0) return;
            PrefabManger.prefabManger.GetAmount(int.Parse(GetStringBeforeDot(gameObject.name)));

            string oldAmount = amountText.text;
            int curAmount = int.Parse(oldAmount);

            if (curAmount == 0)
            {
                PrefabManger.prefabManger.ActivateScroll(true);
                return;
            }
            if (PrefabManger.prefabManger.exists != 0)
            {

                i = PrefabManger.prefabManger.exists;
                SetNameNumber(1);
            }
            curAmount--;
            amountText.text = curAmount.ToString();
            i += 2;
            Vector3 position = new Vector3(14, 0, -9);
            Vector3Int truPosition = Vector3Int.zero;
            currentZ = position.z;

            int name = PlayerPrefs.GetInt("Name", 0);

            Debug.Log("PR : " + gameObject.name);

           

            GameObject instantiatedObject = Instantiate(PrefabManger.prefabManger.GetPref(int.Parse(GetStringBeforeDot(gameObject.name))), position, Quaternion.identity); 

            instantiatedObject.name = gameObject.name + "." + name.ToString();

            //PrefabManger.prefabManger.ActivateObjects(defaultPosition, newPosition);
        }
        else
        {
            
                ShopSystem.shopSystem.BuyItem(int.Parse(GetStringBeforeDot(gameObject.name)));
                Debug.Log(GetStringBeforeDot(gameObject.name));
            
           
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
            return input;
        }
    }

    public int GetNameNumber()
    {
        return PlayerPrefs.GetInt("Name", 0);
    }

    public void SetNameNumber(int score)
    {
        int oldScore = PlayerPrefs.GetInt("Score", 0);

        PlayerPrefs.SetInt("Score", score + oldScore);
    }


}
