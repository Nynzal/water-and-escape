using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelDisplay : MonoBehaviour
{
    private InventoryManager _inventory;

    [SerializeField] private TextMeshProUGUI _waterNumber; 
    
    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _waterNumber.text = "" + (int)_inventory.GetCurrentWater();
    }
}
