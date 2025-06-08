using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIMainMenu _uIMainMenu;
    [SerializeField] private UIStatus _uIStatus;
    [SerializeField] private UIInventory _uIInventory;

    public UIMainMenu UIMainMene() => _uIMainMenu;
    public UIStatus UIStatus() => _uIStatus;
    public UIInventory UIInventory() => _uIInventory;
}
