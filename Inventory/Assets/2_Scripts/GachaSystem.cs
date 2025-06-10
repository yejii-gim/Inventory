using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GachaSystem 
{
    private List<Item> _pool;  // 가챠 아이템 풀

    public GachaSystem()
    {
        // 생성자에서 아이템 풀 초기화
        _pool = CreateItemPool();
    }

    // 아이템 뽑기 함수
    public Item Roll()
    {
        // 골드가 부족하면 null 반환
        if (GameManager.Instance.Player().Gold < 100) return null;


        // 확률 기반으로 희귀도 결정
        float rand = Random.value;
        RarityType rarity = rand switch
        {
            < 0.5f => RarityType.Common,
            < 0.8f => RarityType.Rare,
            < 0.95f => RarityType.Epic,
            _ => RarityType.Legendary
        };
        // 이미 가지고 있는 아이템 제외
        var owned = GameManager.Instance.Player().Inventory;
        var selected = _pool.Where(i => i.Rarity == rarity && !owned.Contains(i)).ToList();
        if (selected.Count == 0) return null;

        // 슬롯 UI 및 골드 정보 갱신
        UIManager.Instance.UIInventory().UpdateCurrentSlotCount();
        GameManager.Instance.Player().UseGold(100);
        UIManager.Instance.UIMainMene().UpdateGoldText(GameManager.Instance.Player());
        return selected[Random.Range(0, selected.Count)];
    }

    private Item CreateItem(RarityType rarity, ItemType type, string iconName, string name, string description, int attack, int defense, int health, int critical)
    {
        Sprite icon = Resources.Load<Sprite>($"Icons/{iconName}");
        return new Item(rarity, type, icon, name, description, attack, defense, health, critical);
    }

    private List<Item> CreateItemPool()
    {
        return new List<Item>
        {
            CreateItem(RarityType.Common, ItemType.Equipment, "Axe", "도끼", "평범한 도끼", 4, 0, 0, 0),
            CreateItem(RarityType.Common, ItemType.Equipment, "Baton", "곤봉", "나무 곤봉", 2, 1, 0, 0),
            CreateItem(RarityType.Rare, ItemType.Equipment, "BigHammer", "큰 망치", "무겁고 강력한 망치", 8, 2, 0, 1),
            CreateItem(RarityType.Rare, ItemType.Equipment, "Bow", "짧은 활", "중거리 무기", 6, 0, 0, 2),
            CreateItem(RarityType.Epic, ItemType.Equipment, "Cleaver", "큰 도끼", "도살용 무기", 10, 1, 0, 1),
            CreateItem(RarityType.Epic, ItemType.Equipment, "Duelsword", "결투검", "빠르고 날렵한 검", 12, 0, 0, 3),
            CreateItem(RarityType.Legendary, ItemType.Equipment, "Goldensword", "황금검", "전설의 황금검", 18, 3, 0, 5),
            CreateItem(RarityType.Common, ItemType.Equipment, "Hammer", "망치", "튼튼한 쇠망치", 5, 1, 0, 0),
            CreateItem(RarityType.Rare, ItemType.Equipment, "Katana", "카타나", "일본식 장검", 9, 1, 0, 2),
            CreateItem(RarityType.Epic, ItemType.Equipment, "Magicstaff", "마법봉", "마법 에너지 담긴 지팡이", 7, 0, 0, 4),
            CreateItem(RarityType.Legendary, ItemType.Equipment, "Sword", "전설의 검", "빛나는 고대의 검", 20, 2, 0, 5),

            CreateItem(RarityType.Common, ItemType.Potion, "RedPotion", "작은 빨간 포션", "체력을 소량 회복", 0, 0, 30, 0),
            CreateItem(RarityType.Rare, ItemType.Potion, "RedPotion", "중간 빨간 포션", "체력을 중간 회복", 0, 0, 60, 0),
            CreateItem(RarityType.Epic, ItemType.Potion, "RedPotion", "큰 빨간 포션", "체력을 대량 회복", 0, 0, 100, 0)
        };
    }
}
