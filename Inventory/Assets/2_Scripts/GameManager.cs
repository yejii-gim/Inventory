using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private Character _player;
    public Character Player() => _player;
    private List<Item> _items = new List<Item>();

    // 게임 시작 시 초기 데이터 생성 및 설정
    private void Start()
    {
        CharacterData characterData = CreateCharacterData();
        SetData(characterData);
        UpdatePlayerStats();
    }
    // 캐릭터 데이터를 바탕으로 플레이어 객체 설정
    public void SetData(CharacterData data)
    {
        _player = new Character(data);
    }
    // 초기 캐릭터 데이터를 생성
    private CharacterData CreateCharacterData()
    {
        // 장비 및 포션 아이템을 생성하여 리스트에 저장
        _items = new List<Item>
        {
            CreateItem(ItemType.Equipment, "Axe", "도끼", "강력한 베기 무기", 15, 2, 0, 1),
            CreateItem(ItemType.Equipment, "Baton", "곤봉", "단단한 나무 곤봉", 8, 4, 0, 0),
            CreateItem(ItemType.Equipment, "BigHammer", "대형 망치", "무겁지만 강력한 일격", 20, 1, 0, 0),
            CreateItem(ItemType.Equipment, "Bow", "활", "원거리 무기", 10, 0, 0, 5),
            CreateItem(ItemType.Equipment, "Cleaver", "클리버", "날카로운 대형 칼", 13, 1, 0, 2),
            CreateItem(ItemType.Equipment, "Duelsword", "이도류", "양손에 칼을 쥔 전투 방식", 14, 1, 0, 3),
            CreateItem(ItemType.Potion, "RedPotion", "빨간 포션", "체력을 회복시켜주는 마법의 약", 0, 0, 50, 0),
            CreateItem(ItemType.Equipment, "Goldensword", "황금검", "빛나는 고급 검", 18, 2, 0, 4),
            CreateItem(ItemType.Equipment, "Hammer", "망치", "기본 철제 망치", 12, 2, 0, 0),
            CreateItem(ItemType.Equipment, "Katana", "카타나", "날렵하고 빠른 베기", 16, 1, 0, 6),
            CreateItem(ItemType.Equipment, "Magicstaff", "마법 지팡이", "마나를 증폭시키는 지팡이", 8, 0, 20, 5),
            CreateItem(ItemType.Equipment, "Sword", "검", "기본적인 검", 10, 1, 0, 1),
            CreateItem(ItemType.Potion, "RedPotion", "빨간 포션", "체력을 회복시켜주는 마법의 약", 0, 0, 50, 0)
        };
        return new CharacterData
        {
            Name = "12시간 노예",
            Level = 5,
            Gold = 1200,
            AttackPower = 30,
            Defense = 15,
            Health = 100,
            Critical = 10,
            Inventory = _items
        };
    }
    // 플레이어 정보 최신화
    public void UpdatePlayerStats()
    {
        UIManager.Instance.UIMainMene().UpdateMainMenuUI(_player);
        UIManager.Instance.UIStatus().UpdateStatusUI(_player);
    }

    private Item CreateItem(ItemType type, string iconName, string name, string description, int attack, int defense, int health, int critical)
    {
        Sprite icon = Resources.Load<Sprite>($"Icons/{iconName}");
        return new Item(type, icon, name, description, attack, defense, health, critical);
    }
}
