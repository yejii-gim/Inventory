using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGacha : MonoBehaviour
{
    [SerializeField] private Button _rollButton;
    private GachaSystem _gachaSystem;
    private void Start()
    { 
        _gachaSystem = new GachaSystem();
        _rollButton.onClick.AddListener(OnClickRoll);
    }

    private void OnClickRoll()
    {
        var rolledItem = _gachaSystem.Roll();

        if (rolledItem != null)
        {
            // 인벤토리에 추가
            GameManager.Instance.Player().AddItem(rolledItem);

            // UI 갱신
            UIManager.Instance.UIInventory().RefreshAllSlots();
        }
    }
}
