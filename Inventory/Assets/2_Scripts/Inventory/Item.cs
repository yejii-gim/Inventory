using UnityEngine;
public enum ItemType
{
    Equipment,
    Potion,
}
// 게임 내 아이템 정보를 담는 클래스
[System.Serializable]
public class Item
{
    public ItemType Type { get; private set; } // 아이템 타입
    public Sprite Icon { get; private set; } // 아이템 아이콘
    public string Name { get; private set; } // 아이템 이름
    public string Description { get; private set; } // 아이템 설명
    public int EffectAttack {  get; private set; } // 공격력
    public int EffectDefense { get; private set; } // 방어력
    public int EffectHealth { get; private set; } // 체력
    public int EffectCritical { get; private set; } // 치망타 확률


    public Item(ItemType type, Sprite icon, string name, string description, int effectAttack, int effectDefense, int effectHealth, int effectCritical)
    {
        Type = type;
        Icon = icon;
        Name = name;
        Description = description;
        EffectAttack = effectAttack;
        EffectDefense = effectDefense;
        EffectHealth = effectHealth;
        EffectCritical = effectCritical;
    }
}
