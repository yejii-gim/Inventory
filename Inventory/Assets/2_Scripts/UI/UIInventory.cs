using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("슬롯 설정")]
    [SerializeField] private GameObject _itemSlotPrefab;
    [SerializeField] private int _slotCount = 20;
    [SerializeField] private Transform _slotsParentTransform;
    [Header("슬롯 수 UI")]
    [SerializeField] private TextMeshProUGUI _curSlotText;
    [SerializeField] private TextMeshProUGUI _maxSlotText;
    [Header("버튼")]
    [SerializeField] private Button _backButton;
    [SerializeField] private List<UISlot> _inventorySlots = new List<UISlot>();

    private List<UISlot> _inventory = new();
    private void Start()
    {
        _maxSlotText.text = $"{_slotCount}";
        InitInventorySlotUI();
        _backButton.onClick.AddListener(BackButton);
    }

    // 현재 사용 중인 슬롯 수 갱신
    public void UpdateCurrentSlotCount()
    {
        int current = GameManager.Instance.Player().Inventory.Count;
        _curSlotText.text = $"{current}";
    }

    // 인벤토리 슬롯 UI 초기화
    public void InitInventorySlotUI()
    {
        _inventory.Clear();

        for (int i = 0; i < _slotCount; i++)
        {
            GameObject obj = Instantiate(_itemSlotPrefab, _slotsParentTransform);
            UISlot slot = obj.GetComponent<UISlot>();
            _inventory.Add(slot);
        }

        RefreshAllSlots();
    }

    // 인벤토리 최신화
    public void RefreshAllSlots()
    {
        List<Item> items = GameManager.Instance.Player().Inventory;

        for (int i = 0; i < _slotCount; i++)
        {
            if (i < items.Count)
            {
                bool isEquipped = GameManager.Instance.Player().IsEquipped(items[i]);
                _inventory[i].SetItem(items[i], isEquipped);
            }
            else
            {
                _inventory[i].SetItem(null);
            }
            //슬롯 UI는 보이게
            _inventory[i].gameObject.SetActive(true);
        }

        UpdateCurrentSlotCount();
    }

    // 아이템 사용 후 UI반영
    public void ItemUsed(Item item)
    {
        GameManager.Instance.Player().Inventory.Remove(item);
        RefreshAllSlots();
        UIManager.Instance.UIStatus().UpdateStatusUI(GameManager.Instance.Player());
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMene().ToggleMainMenuButtons();
    }
}
