using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _equippedImage;

    private Item _item;
    private bool _isEquipped;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickSlot);
    }

    // 슬롯에 아이템과 장착 여부 정보를 설정하고 UI를 갱신
    public void SetItem(Item item, bool isEquipped = false)
    {
        _item = item;
        _isEquipped = isEquipped;
        RefreshUI();
    }

    // 슬롯 UI 최산화(아이템 이미지와 장착 상태 반영)
    public void RefreshUI()
    {
        // 아이템 아이콘을 설정하고 아이템이 없으면 이미지 비활성화
        _itemImage.sprite = _item?.Icon;
        _itemImage.enabled = (_item != null);

        // 장착 여부에 따라 표시 이미지 토글
        _equippedImage.gameObject.SetActive(_isEquipped);  // 장착 여부 반영
    }

    // 슬롯 클릭시 아이템 장착/해제 처리
    private void ClickSlot()
    {
        if (_item == null) return;

        Character player = GameManager.Instance.Player();

        if (_item.Type == ItemType.Equipment)
        {
            if (_isEquipped)
            {
                player.UnEquip(_item);
                _isEquipped = false;
            }
            else
            {
                player.Equip(_item);
                _isEquipped = true;
            }
        }
        else if (_item.Type == ItemType.Potion)
        {
            player.UseItem(_item);

            // 아이템 사용 후 UIInventory 재정렬
            UIManager.Instance.UIInventory().ItemUsed(_item);
            return; 
        }

        UIManager.Instance.UIInventory().RefreshAllSlots();
        UIManager.Instance.UIStatus().UpdateStatusUI(player); // 상태 UI 갱신
    }
}
