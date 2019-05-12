using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Instancia de callbacks abstractos de una ventana de confirmación.
public class ConfirmationWindowShopItem : IConfirmationWindowCallbacks
{
    public ShopItem sourceShopItem;

    //Implementación de set source. Llama a la base y la complementa
    public override void setSource(GameObject newGameObject)
    {
        base.setSource(newGameObject);
        sourceShopItem = sourceGameObject.GetComponent<ShopItem>();
    }
    public override void OnConfirm()
    {
        int newGold = sourceShopItem.getShop().playerInventory.goldAmmount - (sourceShopItem.getItem().itemPrice * sourceShopItem.getQuantity());

        if (newGold < 0)
        {
            //TODO: AÑADIR WARNING DE ESTO.
            //Abstract warning sign -> concrete?
            //o en su defecto no dejar sumar (implica llevar un contador de oro que va variando segun el slider, complejo)
            Debug.Log("no hay suficiente dinero");
            sourceShopItem.OnBuyCancel();
            return;
        }

        sourceShopItem.getShop().playerInventory.addItem(new InventoryItem(sourceShopItem.getItem(), sourceShopItem.getQuantity()));
        int newQuantity = sourceShopItem.getShop().sellerInventory.removeItem(new InventoryItem(sourceShopItem.getItem(), sourceShopItem.getQuantity()));
        sourceShopItem.getShop().playerInventory.goldAmmount = newGold;
        sourceShopItem.getShop().UpdateGold();

        if (newQuantity > 0) {
            EventSystem.current.SetSelectedGameObject(sourceShopItem.gameObject);
            sourceShopItem.Setup(new InventoryItem(sourceShopItem.getItem(), newQuantity), sourceShopItem.getShop());
        }
        else {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            sourceShopItem.getShop().UpdateDisplay();
        }

}

    public override void OnCancel()
    {
        sourceShopItem.OnBuyCancel();
    }
}
