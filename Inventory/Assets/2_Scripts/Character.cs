using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string Name;
    public int Level;
    public int Gold;
    public int AttackPower;
    public int Defense;
    public int Health;
    public int Critical;
    public List<Item> Inventory;
}
public class Character
{
    // 기본 정보
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Gold { get; private set; }
    // 기본 능력치
    public int AttackPower { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; private set; }
    public int Critical { get; private set; }
    public List<Item> Inventory { get; private set; }

    // 현재 장착된 아이템 목록
    private List<Item> _equippedItems = new();

    // 장비 적용 후 최종 능력치
    public int TotalAttack => AttackPower + _equippedItems.Sum(i => i.EffectAttack);
    public int TotalDefense => Defense + _equippedItems.Sum(i => i.EffectDefense);
    public int TotalHealth => Health + _equippedItems.Sum(i => i.EffectHealth);
    public int TotalCritical => Critical + _equippedItems.Sum(i => i.EffectCritical);

    private int _maxHealth = 200;
    public Character(CharacterData data)
    {
        Name = data.Name;
        Level = data.Level;
        Gold = data.Gold;
        AttackPower = data.AttackPower;
        Defense = data.Defense;
        Health = data.Health;
        Critical = data.Critical;
        Inventory = data.Inventory;
    }

    public void AddItem(Item item)
    {
        if (item == null) return;
        Inventory.Add(item);
    }

    // 장비 장착(같은 타입 장비는 해제 후 장착)
    public void Equip(Item item)
    {
        if (item.Type != ItemType.Equipment) return;
        if (!Inventory.Contains(item)) return;
        if (_equippedItems.Contains(item)) return;

        // 같은 타입이 이미 있는지 확인
        Item checkItem = _equippedItems.FirstOrDefault(i => i.Type == item.Type);

        // 있다면 기존 장비 해제
        if (checkItem != null)
        {
            _equippedItems.Remove(checkItem);
        }

        // 새 아이템 장착
        _equippedItems.Add(item);
    }

    // 장비 해제
    public void UnEquip(Item item)
    {
        if (_equippedItems.Contains(item))
            _equippedItems.Remove(item);
    }

    // 해당 아이템이 장착 중인지 확인
    public bool IsEquipped(Item item)
    {
        return _equippedItems.Contains(item);
    }

    // 아이템 사용
    public void UseItem(Item item)
    {
        if (!Inventory.Contains(item)) return;

        if (item.Type == ItemType.Potion)
        {
            // 체력 회복
            Health = Mathf.Min(Health + item.EffectHealth, _maxHealth);
            Inventory.Remove(item); // 사용 후 인벤토리에서 제거
        }
    }

    public void UseGold(int num)
    {
        Gold -= num;
    }
}