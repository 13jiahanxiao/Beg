using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot {

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right
            && InventoryManager.Instance.IsPickedItem==false)
        {
            if (transform.childCount > 0)
            {
                Item current = transform.GetChild(0).GetComponent<ItemUi>().ItemObject;
                transform.parent.parent.SendMessage("BuyItem", current);
            }
        }
        else if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left
            && InventoryManager.Instance.IsPickedItem == true)
        {
            transform.parent.parent.SendMessage("SaleItem");
        }
    }
}
