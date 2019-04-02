using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {
    public int Hp { set; get; }
    public int Mp { set; get; }

    public Consumable(int hp,int mp,int id, string name, ItemTypeEnum itemType, ItemQuality quality,
        string des, int capa, int buy, int sale, string sprite) : base(id, name, itemType, quality, des, capa, buy, sale,sprite)
    {
        this.Hp = hp;
        this.Mp = mp;
    }
}
