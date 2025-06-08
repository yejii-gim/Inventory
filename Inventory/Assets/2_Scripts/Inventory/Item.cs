using System.Collections;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Sprite Icon { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int EffectAttack {  get; private set; }
    public int EffectDefense { get; private set; }
    public int EffectHealth { get; private set; }
    public int EffectCritical { get; private set; }


    public Item(Sprite icon, string name, string description, int effectAttack, int effectDefense, int effectHealth, int effectCritical)
    {
        Icon = icon;
        Name = name;
        Description = description;
        EffectAttack = effectAttack;
        EffectDefense = effectDefense;
        EffectHealth = effectHealth;
        EffectCritical = effectCritical;
    }
}
