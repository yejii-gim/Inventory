using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _gold;

    [SerializeField] Button _statusButton;
    [SerializeField] Button _inventoryButton;

    private void Start()
    {
        _statusButton.onClick.AddListener(OpenStatus);
        _inventoryButton.onClick.AddListener(OpenInventory);
    }

    public void SetMainMenu(Character player)
    {
        _name.text = $"{player.Name}";
        _level.text = $"{player.Level}";
        _gold.text = $"{player.Gold}";
    }

    public void OpenMainMenu()
    {
        UIManager.Instance.UIMainMene().gameObject.SetActive(true);
        ToggleMainMenuButtons();
    }

    public void OpenStatus()
    {
        UIManager.Instance.UIStatus().gameObject.SetActive(true);
        ToggleMainMenuButtons();
    }

    public void OpenInventory()
    {
        UIManager.Instance.UIInventory().gameObject.SetActive(true);
        ToggleMainMenuButtons();
    }

    public void ToggleMainMenuButtons()
    {
        _statusButton.gameObject.SetActive(!_statusButton.gameObject.activeSelf);
        _inventoryButton.gameObject.SetActive(!_inventoryButton.gameObject.activeSelf);
    }
}
