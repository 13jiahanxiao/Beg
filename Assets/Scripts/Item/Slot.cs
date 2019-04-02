using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler{

    public GameObject itemPrefabs;

	public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefabs) as GameObject;
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localScale = Vector3.one;
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUi>().SetItem(item);
        }
        else
        {
            transform.GetChild(0).GetComponent<ItemUi>().AddAmount();
        }
    }
    public int GetItemId()
    {
        return transform.GetChild(0).GetComponent<ItemUi>().ItemObject.Id;
    }
    #region 鼠标与tooltip位置
    public bool IsFiled()
    {
        ItemUi itemUi = transform.GetChild(0).GetComponent<ItemUi>();
        return itemUi.Amount >= itemUi.ItemObject.Capacity;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUi>().ItemObject.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(transform.childCount>0)
        InventoryManager.Instance.HideToolTip();
    }
    #endregion

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (transform.childCount > 0)
            {
                ItemUi currentItem = transform.GetChild(0).GetComponent<ItemUi>();
                if (!InventoryManager.Instance.IsPickedItem)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        int amountPicked = (currentItem.Amount + 1) / 2;
                        InventoryManager.Instance.PickUpItem(currentItem.ItemObject, amountPicked);
                        int amountRemained = currentItem.Amount - amountPicked;
                        if (amountRemained <= 0)
                        {
                            Destroy(currentItem.gameObject);
                        }
                        else
                        {
                            currentItem.SetAmount(amountRemained);
                        }
                    }
                    else
                    {
                        InventoryManager.Instance.PickUpItem(currentItem.ItemObject, currentItem.Amount);
                        Destroy(currentItem.gameObject);
                    }
                }
                else
                {
                    if (currentItem.ItemObject.Id == InventoryManager.Instance.PickedItem.ItemObject.Id)
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            if (currentItem.ItemObject.Capacity > currentItem.Amount)
                            {
                                currentItem.AddAmount();
                                InventoryManager.Instance.RemoveItem();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (currentItem.ItemObject.Capacity > currentItem.Amount)
                            {
                                int amountRemain = currentItem.ItemObject.Capacity - currentItem.Amount;
                                if (amountRemain > InventoryManager.Instance.PickedItem.Amount)
                                {
                                    currentItem.AddAmount(InventoryManager.Instance.PickedItem.Amount);
                                    InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                                }
                                else
                                {
                                    currentItem.SetAmount(currentItem.ItemObject.Capacity);
                                    InventoryManager.Instance.RemoveItem(amountRemain);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        currentItem.Exchange(InventoryManager.Instance.PickedItem);
                    }
                }
            }
            else
            {
                if (InventoryManager.Instance.IsPickedItem == true)
                {
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        DisplayItemInEmpty();
                    }
                    else
                    {
                        for (int i = 0; i < InventoryManager.Instance.PickedItem.Amount; i++)
                        {
                            this.StoreItem(InventoryManager.Instance.PickedItem.ItemObject);
                        }
                        InventoryManager.Instance.RemoveItem(InventoryManager.Instance.PickedItem.Amount);
                    }
                }
                else return;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (transform.childCount > 0 && InventoryManager.Instance.IsPickedItem == false)
            {
                ItemUi currentItem = transform.GetChild(0).GetComponent<ItemUi>();
                if (currentItem.ItemObject is Equipment || currentItem.ItemObject is Weapon)
                {
                    Item current = currentItem.ItemObject;
                    currentItem.AddAmount(-1);
                    if (currentItem.Amount <= 0)
                    {
                        DestroyImmediate(currentItem.gameObject);
                        InventoryManager.Instance.HideToolTip();
                    }
                    Character.Instance.PutOn(current);
                }
            }
        }
    }

    public void DisplayItemInEmpty()
    {
        this.StoreItem(InventoryManager.Instance.PickedItem.ItemObject);
        InventoryManager.Instance.RemoveItem();
    }
}
