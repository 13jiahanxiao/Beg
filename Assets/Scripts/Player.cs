using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    int money = 1000;
    Text moneyText;

    private void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        moneyText.text = money.ToString();
    }


    void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int id = Random.Range(1, 11);
            Knapasck.Instance.StoreItem(id);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Knapasck.Instance.DisplaySwitch();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Chest.Instance.DisplaySwitch();
        }
	}

    public bool Consume(int i)
    {
        if (money >= i)
        {
            money -= i;
            moneyText.text = money.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Earn(int i)
    {
        money+=i;
        moneyText.text = money.ToString();
    }
}
