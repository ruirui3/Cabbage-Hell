using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoot", menuName = "Loot System/Loot Item")]
public class Loot : ScriptableObject
{
    public Sprite lootSprite;
    public string lootName;
    [Range(0, 100)] public int dropChance; // Drop rate in %
}