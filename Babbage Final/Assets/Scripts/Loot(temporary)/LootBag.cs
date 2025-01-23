using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    void Start()
    {
        lootList.Add(new LootBag("XP", 100));
        lootList.Add(new LootBag("Common Chest", 90));
        lootList.Add(new LootBag("Uncommon Chest", 85));
        lootList.Add(new LootBag("Rare Chest", 65));
        lootList.Add(new LootBag("Epic Chest", 40));
        lootList.Add(new LootBag("Mythic Chest ", 10));
        lootList.Add(new LootBag("Legendary Chest", 2));




    }
