using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ShopList : MonoBehaviour
{
    public Inventory sellerInventory;
    public Inventory playerInventory;
    public Transform panel;
    public SimpleObjectPool itemPool;
    public Text goldText;
    public Text descriptionText;
    public GameObject confirmationWindow;

    private List<InventoryItem> itemsToSell;
    private int playerGold;
    private ShopItem lastSelected = null;
   
    private void Start()
    {
        itemsToSell = sellerInventory.inventory;
        playerGold = playerInventory.goldAmmount;

        confirmationWindow.SetActive(false);
        panel.gameObject.SetActive(false);
        
        UpdateDisplay();
        UpdateGold();
        
    }
    public void UpdateDisplay()
    {
        ClearButtons();
        AddButtons();
    }
    private void AddButtons()
    {
        for (int i = 0; i < itemsToSell.Count; i++)
        {
            InventoryItem item = itemsToSell[i];
            GameObject newButton = itemPool.GetObject();
            newButton.transform.SetParent(panel);
            RectTransform rt = newButton.GetComponent<RectTransform>();
            rt.localScale = new Vector3(1, 1, 1);
            ShopItem tempShopItem = newButton.GetComponent<ShopItem>();
            tempShopItem.Setup(item, this);
        }
    }
    public void UpdateGold()
    {
        playerGold = playerInventory.goldAmmount;
        goldText.text = playerGold.ToString()+"  G";
    }
    public void UpdateDescription(string descritpion)
    {
        descriptionText.text = descritpion;
    }
    private void ClearButtons()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    private void Update()
    {
        GameObject currentGameObject = EventSystem.current.currentSelectedGameObject;
        ShopItem selected = currentGameObject?.GetComponent<ShopItem>();

        if (selected != null && selected != lastSelected)
        {
            UpdateDescription(selected.GetDescription());
            lastSelected = selected;
        }
        if (selected == null)
        {
            UpdateDescription("");
        }
    }
}
