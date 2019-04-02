using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : Inventory {
    public int[] itemId;
    Player player;

    public void Awake()
    {
        itemId = new int[10];
        for(int i = 1; i < 11; i++)
        {
            itemId[i - 1] = i ;
        }
    }
    protected override void Start()
    {
        base.Start();
        InitShop();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void InitShop()
    {
        foreach(int i in itemId)
        {
            StoreItem(i);
        }
    }

    public void BuyItem(Item item)
    {
        bool isSuccess = player.Consume(item.BuyPrice);
        if (isSuccess)
        {
            Knapasck.Instance.StoreItem(item);
        }
    }
    public void SaleItem()
    {
        int sellAmount = 1;
        if (Input.GetKey(KeyCode.LeftControl)) sellAmount = 1;
        else sellAmount = InventoryManager.Instance.PickedItem.Amount;
        int money = InventoryManager.Instance.PickedItem.ItemObject.SalePrice * sellAmount;
        player.Earn(money);
        InventoryManager.Instance.RemoveItem(sellAmount);
    }
}
