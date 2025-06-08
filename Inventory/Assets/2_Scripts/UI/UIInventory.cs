using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject _itemSlotPrefab;
    [SerializeField] private int _slotCount = 20;
    [SerializeField] private TextMeshProUGUI _curSlotText;
    [SerializeField] private TextMeshProUGUI _maxSlotText;
    [SerializeField] private List<UISlot> _inventory = new List<UISlot>();
    [SerializeField] private Transform _slotsParentTransform;
    [SerializeField] private Button _backButton;

    private void Start()
    {
        _maxSlotText.text = $"{_slotCount}";
        InitInventoryUI();
        _backButton.onClick.AddListener(BackButton);
    }

    public void UpdateCurrentSlotCount(int current)
    {
        _curSlotText.text = $"{current}";
    }

    public void InitInventoryUI()
    {
        _inventory.Clear();
        List<Item> items = GameManager.Instance.Player().Inventory;

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject slotobj = Instantiate(_itemSlotPrefab, _slotsParentTransform);
            UISlot slot = slotobj.GetComponent<UISlot>();
            if (slot != null)
            {
                _inventory.Add(slot);

                if(i < _inventory.Count)
                {
                    if (i < items.Count)
                    {
                        slot.SetItem(items[i], GameManager.Instance.Player().IsEquipped(items[i]));
                    }
                    else
                    {
                        slot.SetItem(null);
                    }
                }
            }
        }
    }
    public void BackButton()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMene().ToggleMainMenuButtons();
    }
}
