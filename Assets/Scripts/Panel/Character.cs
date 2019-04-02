using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Inventory {
    private static Character _instance;
    public static Character Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Character").GetComponent<Character>();
            }
            return _instance;
        }
    }
    public void PutOn(Item item)
    {
        Item exitItem = null;
        foreach(Slot slot in slotArray)
        {
            EquipmentSlot equipSlot = (EquipmentSlot)slot;
            if (equipSlot.IsRightItem(item))
            {
                if (equipSlot.transform.childCount > 0)
                {
                    exitItem = equipSlot.transform.GetChild(0).GetComponent<ItemUi>().ItemObject;
                    equipSlot.transform.GetChild(0).GetComponent<ItemUi>().SetItem(item);
                }
                else
                {
                    equipSlot.StoreItem(item);
                }
                break;
            }
        }
        if(exitItem!=null)
            Knapasck.Instance.StoreItem(exitItem);
    }

    public void PutOff(Item item)
    {
        Knapasck.Instance.StoreItem(item);
    }
}
