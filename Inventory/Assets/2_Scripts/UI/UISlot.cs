using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Image _equippedImage;

    private Item _item;
    private Sprite _itemSprite;
    private bool _isEquipped;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickSlot);
    }

    public void SetItem(Item item, bool isEquipped = false)
    {
        _item = item;
        _isEquipped = isEquipped;
        RefreshUI();
    }

    public void RefreshUI()
    {
        _itemImage.sprite = _item?.Icon;
        _itemImage.enabled = (_item != null);
        _equippedImage.gameObject.SetActive(_isEquipped);  // 장착 여부 반영
    }

    private void ClickSlot()
    {
        if (_item == null) return;

        Character player = GameManager.Instance.Player();

        if (_isEquipped)
        {
            player.UnEquip(_item);
            _isEquipped = false;
            Debug.Log($"{_item.Name} 장착 해제");
        }
        else
        {
            player.Equip(_item);
            _isEquipped = true;
            Debug.Log($"{_item.Name} 장착");
        }

        RefreshUI();
        UIManager.Instance.UIStatus().SetStatus(player); // 상태 UI 갱신
    }
}
