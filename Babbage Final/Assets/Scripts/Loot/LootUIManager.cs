using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootUIManager : MonoBehaviour
{
    public Loot loot;
    public TMP_Text lootText;      // Assign in Inspector
    public Image lootImage;    // Assign in Inspector
    public GameObject lootPanel; // The UI panel

    void Start()
    {
        lootPanel.SetActive(false);
    }
    public void ShowLoot(Loot loot)
    {
        if (loot == null)
        {
            Debug.LogError("Loot is null! Make sure you are passing a valid loot object.");
            return;
        }

        if (loot.lootSprite == null)
        {
            Debug.LogError($"Loot sprite is missing for {loot.lootName}!");
            return;
        }

        lootImage.sprite = loot.lootSprite;  // Set loot image
        lootImage.gameObject.SetActive(true); // Ensure the image is active

        lootText.text = "You got: " + loot.lootName; // Set loot name
        lootText.gameObject.SetActive(true); // Ensure the text is active
        lootPanel.SetActive(true);
        Debug.Log("opened lootbox");
    }

    public void HideLoot()
    {
        lootPanel.SetActive(false);
    }
}

