using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUi : MonoBehaviour {
    #region Data
    public Item ItemObject{ get; set; }
    public int Amount { get; set; }
    #endregion

    #region Ui Component
    private Image itemImage;
    private Text itemText;

    private Image ItemImage
    {
        get
        {
            if(itemImage==null)
                itemImage = GetComponent<Image>();
            return itemImage;
        }
    }
    private Text ItemText
    {
        get
        {
            if(itemText==null)
                itemText = GetComponentInChildren<Text>();
            return itemText;
        }
    }
    #endregion

    private float targetScale = 1f;

    private Vector3 animationScale = new Vector3(1.4f, 1.4f, 1.4f);

    private void Update()
    {
        if(transform.localScale.x != targetScale)
        {
            float scale = Mathf.Lerp(transform.localScale.x, targetScale,Time.deltaTime);
            transform.localScale = new Vector3(scale, scale, scale);
            if (Mathf.Abs(transform.localScale.x - targetScale) < 0.02)
            {
                transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            }
        }
    }

    public void SetItem(Item item,int amount=1)
    {
        transform.localScale = animationScale;
        this.ItemObject = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(ItemObject.Sprite);
        RemainedItemText();
    } 

    public void AddAmount(int amount=1)
    {
        transform.localScale = animationScale;
        this.Amount += amount;
        RemainedItemText();
    }

    public void SetAmount(int amount)
    {
        this.Amount = amount;
        RemainedItemText();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetPosition(Vector3 vec)
    {
        transform.localPosition = vec;
    }
    public void RemainedItemText()
    {
        if (ItemObject.Capacity > 1)
        {
            ItemText.text = Amount.ToString();
        }
        else
        {
            ItemText.text = "";
        }
    }

    public void Exchange(ItemUi itemUi)
    {
        Item item = itemUi.ItemObject;
        int amount = itemUi.Amount;
        itemUi.SetItem(this.ItemObject, this.Amount);
        this.SetItem(item, amount);
    }
}
