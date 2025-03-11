using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;

public class ItemManager : MonoBehaviour
{
    Action<string> _createItemsCallback;

    void Start()
    {
        _createItemsCallback = (jsonArrayString) =>
        { 
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.Instance.userInfo.UserID;
        StartCoroutine(Main.Instance.web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for(int i = 0; i< jsonArray.Count; i++) 
        {
            bool isDone = false;
            string itemId = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            StartCoroutine(Main.Instance.web.GetItems(itemId, getItemInfoCallback));

            yield return new WaitUntil(() => isDone == true);

            GameObject itemGO = Instantiate(Resources.Load("Prefabs/Item 1") as GameObject);
            Item item = itemGO.AddComponent<Item>();
            item.ID = id;
            item.itemID = itemId;
            itemGO.transform.SetParent(this.transform);
            itemGO.transform.localScale = Vector3.one;
            itemGO.transform.localPosition = Vector3.zero;

            itemGO.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = itemInfoJson["name"];
            itemGO.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = itemInfoJson["price"];
            itemGO.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = itemInfoJson["description"];

            itemGO.transform.Find("Sell").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInInventory = id;
                string iId = itemId;
                string userId = Main.Instance.userInfo.UserID;

                StartCoroutine(Main.Instance.web.SellItem(idInInventory, userId, iId));
            });
            itemGO.transform.Find("Sell").GetComponent<Button>().onClick.AddListener(() => itemGO.transform.Find("Sell").GetComponentInParent<SellDelete>().Sell());
        }

        yield return null;
    }
}
