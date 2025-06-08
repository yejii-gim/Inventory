using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{

    private Character _player;
    public Character Player() => _player;
    private List<Item> items = new List<Item>();
    private void Start()
    {
        CharacterData characterData = CreateCharacterData();
        SetData(characterData);
        UpdatePlayerStats();
    }

    public void SetData(CharacterData data)
    {
        _player = new Character(data);
    }

    private CharacterData CreateCharacterData()
    {
        SetItem();
        return new CharacterData
        {
            Name = "Piggy",
            Level = 5,
            Gold = 1200,
            AttackPower = 30,
            Defense = 15,
            Health = 200,
            Critical = 10,
            Inventory = items
        };
    }

    public void UpdatePlayerStats()
    {
        UIManager.Instance.UIMainMene().SetMainMenu(_player);
        UIManager.Instance.UIStatus().SetStatus(_player);
    }

    public void SetItem()
    {
        // 아이템 생성
        items.Add(new Item(null, "강철 검", "공격력이 상승하는 검", 10, 0, 0, 5));
        items.Add(new Item(null, "튼튼한 방패", "방어력이 상승하는 방패", 0, 8, 20, 0));
        UIManager.Instance.UIInventory().UpdateCurrentSlotCount(items.Count);
    }
}
