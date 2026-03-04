using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Simple RPG/Weapons/Weapons Settings")]
public class WeaponsSettings : ScriptableObject
{
    [SerializeField] private WeaponContainer[] _weapons;

    public WeaponContainer[] Weapons => _weapons;
}

[Serializable]
public class WeaponContainer
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _animationDuration;
    [SerializeField] private WeaponInfo _weaponInfo;

    public GameObject Prefab => _prefab;
    public float AnimationDuration => _animationDuration;
    public WeaponInfo WeaponInfo => _weaponInfo;
}

[Serializable]
public class WeaponInfo
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float damage;
    [SerializeField] private float _price;

    public string Name => _name;
    public string Description => _description;
    public float Damage => damage;
    public float Price => _price;
}