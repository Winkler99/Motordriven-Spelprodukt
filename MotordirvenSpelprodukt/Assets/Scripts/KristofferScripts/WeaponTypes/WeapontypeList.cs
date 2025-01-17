using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponType/WeapontypeList")]
public class WeapontypeList : ScriptableObject
{
    [SerializeField] private List<Weapontype> weapontypes;

    public List<Weapontype> GetTypeList()
    {
        return weapontypes;
    }
}
