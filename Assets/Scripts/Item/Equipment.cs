 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment :Item {
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipType EquipmentType { get; set; }

    public enum EquipType
    {
        None,
        Head,
        Clothes,
        Trousers,
        Shoes,
        Ornament
    }

    public Equipment(int str,int inte,int agi,int sta,EquipType equip, int id, string name, ItemTypeEnum itemType, ItemQuality quality,
        string des, int capa, int buy, int sale, string sprite) : base(id, name, itemType, quality, des, capa, buy, sale,sprite)
    {
        this.Strength = str;
        this.Intelligence = inte;
        this.Agility = agi;
        this.Stamina = sta;
        this.EquipmentType = equip;
    }
}
