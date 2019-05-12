using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Elemento de una tienda. Es un "Boton".
public class ShopItem : MonoBehaviour
{
    public Button ItemButton;
    public Text ItemName;
    public Text ItemPrice;
    public Image ItemImage;
    public Image ItemQuantityBackground;
    public Text ItemStock;
    public Text ItemQuantity;
    public Slider ItemQuantitySlider;
    public IConfirmationWindowCallbacks ConfirmationWindowCallbacks;

    private GameItem item;
    private ShopList shop;
    private int buyQuantity;

    public GameItem getItem()
    {
        return item;
    }
    public int getQuantity()
    {
        return buyQuantity;
    }
    public ShopList getShop()
    {
        return shop;
    }

    public void Setup(InventoryItem sourceItem, ShopList sourceShop)
    {
        item = sourceItem.item;
        shop = sourceShop;
        buyQuantity = 0;

        ItemName.text = item.itemName;
        ItemPrice.text = item.itemPrice+"G";
        ItemImage.sprite = item.itemIcon;
        ItemStock.text = sourceItem.quantity.ToString();
        ItemQuantity.text = buyQuantity.ToString();

        ItemQuantitySlider.minValue = 0;
        ItemQuantitySlider.value = 0;
        ItemQuantitySlider.maxValue = sourceItem.quantity;

        ConfirmationWindowCallbacks.setSource(this.gameObject);
    }

    public void onSliderValueChange(int value)
    {
        ItemQuantity.text = this.ItemQuantitySlider.value.ToString();
        this.buyQuantity = Mathf.CeilToInt(this.ItemQuantitySlider.value);
    }

    public string GetDescription()
    {
        return item.itemDescription;
    }
    
    public void increaseQuantity()
    {
        buyQuantity++;
        ItemQuantity.text = buyQuantity.ToString();
    }

    //Al pulsar "submit" sobre este objeto
    public void OnItemClick()
    {
        EventSystem.current.SetSelectedGameObject(ItemQuantitySlider.gameObject);
        ItemQuantityBackground.gameObject.SetActive( true );
    }
    //Al pulsar "cancel" sobre este objeto
    public void OnItemCancel()
    {
        this.shop.panel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
        ItemQuantityBackground.gameObject.SetActive(false);
    }
    public void OnBuyClick()
    {
        GameObject confirmationWindow = shop.confirmationWindow;
        confirmationWindow.SetActive(true);
        confirmationWindow.GetComponent<ConfirmationWindow>().setCancelElement(ItemButton.gameObject);
        confirmationWindow.GetComponent<ConfirmationWindow>().setCallbacks(this.ConfirmationWindowCallbacks);
        ItemQuantityBackground.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(confirmationWindow.GetComponentInChildren<Button>().gameObject);
    }
    public void OnConfirmationTrue()
    {
        shop.playerInventory.addItem(new InventoryItem(item, buyQuantity));
    }
    public void OnBuyCancel()
    {
        ItemQuantity.text = "0";
        buyQuantity = 0;
        ItemQuantitySlider.value = 0;
        EventSystem.current.SetSelectedGameObject(ItemButton.gameObject);
        ItemQuantityBackground.gameObject.SetActive(false);
    }
}
