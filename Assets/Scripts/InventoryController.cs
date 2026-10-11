using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIPage inventoryUI;

    public int inventorySize = 10;

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.eKey.wasPressedThisFrame || kb.ctrlKey.wasPressedThisFrame)
        {
            if (!inventoryUI.isActiveAndEnabled)
                inventoryUI.Show();
            else
                inventoryUI.Hide();
        }
    }
    void Start()
    {
        inventoryUI.Initialize(inventorySize);
    }
    
}