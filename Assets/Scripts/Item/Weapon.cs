using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    public int Damage { get; set; }
    public WeaponEnum WeaponType { get; set; }

    public enum WeaponEnum
    {
        None,
        MainHand,
        OffHand,
    }

    public Weapon(int damage,WeaponEnum weaponType, int id, string name, ItemTypeEnum itemType, ItemQuality quality,
        string des, int capa, int buy, int sale, string sprite) : base(id, name, itemType, quality, des, capa, buy, sale,sprite)
    {
        this.Damage = damage;
        this.WeaponType = weaponType;
    }
}
