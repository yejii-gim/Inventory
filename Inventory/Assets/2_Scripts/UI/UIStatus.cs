using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [Header("스탯 텍스트")]
    [SerializeField] private TextMeshProUGUI _attackPowerText;
    [SerializeField] private TextMeshProUGUI _defenseText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _criticalText;

    private void Start()
    {
        _backButton.onClick.AddListener(BackButton);
    }

    // 상태창 최신화
    public void UpdateStatusUI(Character player)
    {
        _attackPowerText.text = $"{player.TotalAttack}";
        _defenseText.text = $"{player.TotalDefense}";
        _healthText.text = $"{player.TotalHealth}";
        _criticalText.text = $"{player.TotalCritical}";
    }

    public void BackButton()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMene().ToggleMainMenuButtons();
    }
}
