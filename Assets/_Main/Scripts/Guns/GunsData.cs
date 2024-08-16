using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "GunsCreations")]
public class GunsData : ScriptableObject
{
    public string Name;

    public GameObject SelectionModel;
    public GameObject GamePlayModel;

    public bool IsUnlocked;

    public bool locked;

    public int Price;
    public bool Selected;


    [Header("Sniper Specifications")]
    public float Damage;
    public float FireRate;
    public float Mobility;
    public float Ammo;
    public float Range;

    





}
