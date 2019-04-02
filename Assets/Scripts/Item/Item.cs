using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int Id { get; set; }
    public string Name { get; set; }
    public ItemTypeEnum ItemType { get; set; }
    public ItemQuality Quality { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int BuyPrice { get; set; }
    public int SalePrice { get; set; }
    public string Sprite { get; set; }
    public enum ItemTypeEnum
    {
        Consumable,
        Equipment,
        Weapon,
        Material
    }
    public enum ItemQuality
    {
        Common,
        Rare,
        Excellent,
        Epic,
        Legend
    }

    public Item()
    {
        this.Id = -1;
    }

    public Item(int id,string name,ItemTypeEnum itemType,ItemQuality quality,
        string des,int capa,int buy,int sale,string sprite)
    {
        this.Id = id;
        this.Name = name;
        this.ItemType = itemType;
        this.Quality = quality;
        this.Description = des;
        this.Capacity = capa;
        this.BuyPrice = buy;
        this.SalePrice = sale;
        this.Sprite = sprite;
    }

    public virtual string GetToolTipText()
    {
        string color = "";
        switch (Quality)
        {
            case ItemQuality.Common:
                color = "white";
                break;
            case ItemQuality.Rare:
                color = "lime";
                break;
            case ItemQuality.Excellent:
                color = "aqua";
                break;
            case ItemQuality.Epic:
                color = "magenta";
                break;
            case ItemQuality.Legend:
                color = "orange";
                break;
        }
        string des = string.Format("<color={4}>{0}</color>\n购买价格:{1} 出售价格:{2}<color=yellow>\n</color>{3}", Name, BuyPrice, SalePrice, Description, color);
        return des;
    }
}
