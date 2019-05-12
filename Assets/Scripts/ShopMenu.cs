using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class ShopMenu : MonoBehaviour
{
    public ShopList shop;

    public async void onShopClicked()
    {
        shop.panel.gameObject.SetActive(true);
        await Task.Delay(System.TimeSpan.FromSeconds(0.25));


        Button firstShopButton = shop.panel.gameObject.GetComponentInChildren<Button>();
        EventSystem.current.SetSelectedGameObject(firstShopButton.gameObject);
    }
}
