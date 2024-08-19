using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    public static InGameMenuController Instance;

    [Header("References")]
    [SerializeField] private GameObject menuBackground; // not a raycast target
    [SerializeField] private UIInventoryPanel InventoryPage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } // TODO: destroy others if present
    }

    private void Start()
    {
        HideMenuBackground();
    }

    public void HideMenuBackground()
    {
        menuBackground.SetActive(false);
    }
    
    public void ShowMenuBackground()
    {
        menuBackground.SetActive(true);
    }
}
