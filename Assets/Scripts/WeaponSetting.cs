using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/WeaponSetting")]
public class WeaponSetting : ScriptableObject
{
    [SerializeField] private float _weaponRange = 100f;
    [SerializeField] private int _ammo = 0;
    [SerializeField] private float _damage = 100f;

    public float Range { get { return _weaponRange; }  }
    public int Ammo { get { return _ammo; } set { _ammo = value; } }

    public float Damage { get { return _damage; } }


}
