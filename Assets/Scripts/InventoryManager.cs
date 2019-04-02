using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    #region Instance
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get {
            if (_instance == null)
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            return _instance;
        }
    }
    #endregion

    #region ToolTip

    ToolTip toolTip;
    bool toolTipShow = false;
    Canvas canvas;
    Vector2 toolTipPositionOffSet=new Vector2(10,-20);
    Vector2 mousePosition;

    public void ShowToolTip(string content)
    {
        if (isPickedItem) return;
        toolTip.Show(content);
        toolTipShow = true;
    }
    public void HideToolTip()
    {
        toolTip.Hide();
        toolTipShow = false;
    }

    #endregion

    #region PickItem
    ItemUi _pickedItem;
    public ItemUi PickedItem
    {
        get { return _pickedItem; }
        set { _pickedItem = value; }
    }
    bool isPickedItem = false;
    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }
    }

    public void PickUpItem(Item item,int amount=1)
    {
        PickedItem.SetItem(item,amount);
        isPickedItem = true;

        PickedItem.Show();
        this.toolTip.Hide();
    }

    #endregion

    private List<Item> itemList;

    private void Awake()
    {
        ParseItemJson();
        toolTip = GameObject.FindObjectOfType<ToolTip>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        PickedItem = GameObject.Find("PickedItem").GetComponent<ItemUi>();
        PickedItem.Hide();
    }

    private void Update()
    {
        if (isPickedItem)
        {
            PickedItem.SetPosition(MousePosition());
        }
        else if (toolTipShow)
        {
            toolTip.SetLocalPosition(MousePosition()+toolTipPositionOffSet);
        }
        if(isPickedItem && Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }

    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText =Resources.Load<TextAsset>("ItemJson");
        string itemStr = itemText.text;
        JSONObject j = new JSONObject(itemStr);
        foreach(JSONObject temp in j.list)
        {
            int id = (int)temp["Id"].n;
            string name = temp["Name"].str;
            Item.ItemQuality quality = (Item.ItemQuality)System.Enum.Parse(typeof(Item.ItemQuality), temp["Quality"].str);
            string description = temp["Description"].str;
            int capacity = (int)temp["Capacity"].n;
            int buyPrice = (int)temp["BuyPrice"].n;
            int salePrice = (int)temp["SalePrice"].n;
            string sprite = temp["Sprite"].str;
            string typeStr = temp["ItemType"].str;
            Item.ItemTypeEnum itemType = (Item.ItemTypeEnum)System.Enum.Parse(typeof(Item.ItemTypeEnum), typeStr);
            Item item = null;
            switch (itemType)
            {
                case Item.ItemTypeEnum.Consumable:
                    int hp = int.Parse(temp["Hp"].ToString());
                    int mp = int.Parse(temp["Mp"].ToString());
                    item = new Consumable(hp,mp,id, name, itemType, quality,description,capacity, buyPrice, salePrice, sprite);
                    break;
                case Item.ItemTypeEnum.Equipment:
                    int strength = int.Parse(temp["Strength"].ToString());
                    int Intelligence = int.Parse(temp["Intelligence"].ToString());
                    int agility = int.Parse(temp["Agility"].ToString());
                    int stamina = int.Parse(temp["Stamina"].ToString());
                    Equipment.EquipType equipmentType = (Equipment.EquipType)System.Enum.Parse(typeof(Equipment.EquipType), temp["EquipmentType"].str);
                    item = new Equipment(strength, Intelligence, agility, stamina, equipmentType, id, name, itemType, quality, description, capacity, buyPrice, salePrice, sprite);
                    break;
                case Item.ItemTypeEnum.Weapon:
                    int damage = int.Parse(temp["Damage"].ToString());
                    Weapon.WeaponEnum weaponType = (Weapon.WeaponEnum)System.Enum.Parse(typeof(Weapon.WeaponEnum), temp["WeaponType"].str);
                    item = new Weapon(damage, weaponType, id, name, itemType, quality, description, capacity, buyPrice, salePrice, sprite);
                    break;
                case Item.ItemTypeEnum.Material:
                    break;
                default:
                    break;
            }
            itemList.Add(item);
        }
    }

    public Item GetItemById(int id)
    {
        foreach(Item item in itemList)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return null;
    }

    public Vector2 MousePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out mousePosition);
        return mousePosition;
    }

    public void RemoveItem(int amount=1)
    {
        amount = -amount;
        PickedItem.AddAmount(amount);
        if (PickedItem.Amount <= 0)
        {
            isPickedItem = false;
            PickedItem.Hide();
        }
    }
}
