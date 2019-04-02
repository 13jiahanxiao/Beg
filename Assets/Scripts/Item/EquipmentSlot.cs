using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : Slot
{
    public Equipment.EquipType equipType;
    public Weapon.WeaponEnum weaponType;

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Left)
        {
            if (InventoryManager.Instance.IsPickedItem)
            {
                if (transform.childCount > 0)
                {
                    ItemUi currentItem = transform.GetChild(0).GetComponent<ItemUi>();
                    if (IsRightItem(InventoryManager.Instance.PickedItem.ItemObject))
                    {
                        InventoryManager.Instance.PickedItem.Exchange(currentItem);
                    }
                }
                else
                {
                    if (IsRightItem(InventoryManager.Instance.PickedItem.ItemObject))
                    {
                        DisplayItemInEmpty();
                    }
                }
            }
            else
            {
                if (transform.childCount > 0)
                {
                    ItemUi item = transform.GetChild(0).GetComponent<ItemUi>();
                    InventoryManager.Instance.PickUpItem(item.ItemObject);
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }
        else if (eventData.button == UnityEngine.EventSystems.PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)
            {
                ItemUi currentItem = transform.GetChild(0).GetComponent<ItemUi>();
                Item current = currentItem.ItemObject;
                Destroy(currentItem.gameObject);
                transform.parent.parent.SendMessage("PutOff",current);
                InventoryManager.Instance.HideToolTip();
            }
                
        }
    }
    public bool IsRightItem(Item item)
    {
        if (item is Equipment && ((Equipment)(item)).EquipmentType == equipType
                    || item is Weapon && ((Weapon)(item)).WeaponType == weaponType)
        {
            return true;
        }
        else return false;
    }
}
