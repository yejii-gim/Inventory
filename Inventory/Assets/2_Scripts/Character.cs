using System.Collections.Generic;
using System.Linq;

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
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int Gold { get; private set; }
    public int AttackPower { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; private set; }
    public int Critical { get; private set; }
    public List<Item> Inventory { get; private set; }

    private List<Item> _equippedItems = new();

    public int TotalAttack => AttackPower + _equippedItems.Sum(i => i.EffectAttack);
    public int TotalDefense => Defense + _equippedItems.Sum(i => i.EffectDefense);
    public int TotalHealth => Health + _equippedItems.Sum(i => i.EffectHealth);
    public int TotalCritical => Critical + _equippedItems.Sum(i => i.EffectCritical);

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
        Inventory.Add(item);
    }

    public void Equip(Item item)
    {
        if (!Inventory.Contains(item)) return;
        if (_equippedItems.Contains(item)) return;

        _equippedItems.Add(item);
    }

    public void UnEquip(Item item)
    {
        if (_equippedItems.Contains(item))
            _equippedItems.Remove(item);
    }

    public bool IsEquipped(Item item)
    {
        return _equippedItems.Contains(item);
    }
}