using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    protected Slot[] slotArray;

    float targetAlpha = 1;
    float smoothing = 4;
    CanvasGroup canvasGroup;

    protected virtual void Start()
    {
        slotArray = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if(Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public bool StoreItem( int id)
    {
        Item item = InventoryManager.Instance.GetItemById(id);
        return StoreItem(item);
    }
    public bool StoreItem(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("要存储的物品id不存在");
            return false;
        }
        if (item.Capacity == 1)
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.LogWarning("没有空的物品槽");
                return false;
            }
            else slot.StoreItem(item);
        }
        else
        {
            Slot slot = FindSameIdSlot(item);
            if (slot != null)
            {
                slot.StoreItem(item);//应增加物品数量
            }
            else
            {
                slot = FindEmptySlot();
                if (slot == null)
                {
                    Debug.LogWarning("没有空的物品槽");
                    return false;
                }
                else slot.StoreItem(item);
            }
        }
        return true;
    }
    //寻找空的物品槽
    private Slot FindEmptySlot()
    {
        foreach(Slot slot in slotArray)
        {
            if (slot.transform.childCount == 0)
                return slot;
        }
        return null;
    }
    //寻找相同物品的物品槽
    private Slot FindSameIdSlot(Item item)
    {
        foreach (Slot slot in slotArray)
        {
            if (slot.transform.childCount >= 1 && slot.GetItemId() == item.Id  && slot.IsFiled() == false)
                return slot;
        }
        return null;
    }

    public void DisplaySwitch()
    {
        if (targetAlpha == 0)
        {
            canvasGroup.blocksRaycasts = true;
            targetAlpha = 1;
        }
        else if (targetAlpha == 1)
        {
            canvasGroup.blocksRaycasts = false;
            targetAlpha = 0;
        }
    }
}
