# 🧰 Unity 인벤토리 & 장비 시스템
- **개발 엔진**: Unity `2022.3.17f1`
- **개발 기간**: 2025.06.06 ~ 2025.06.08

## 🎮 주요 기능
- 🧍 캐릭터 데이터 초기화 및 능력치 계산
- 🎒 인벤토리 UI 및 슬롯 관리
- 🗡️ 장비 아이템 장착/해제 
- ❤️ 아이템 사용 및 상태창 UI 연동

---

## 🧍 캐릭터 데이터 
 - 캐릭터는 `CharacterData` 클래스를 통해 생성되며, 초기 능력치와 인벤토리를 보유
<details>
<summary>🔽 캐릭터 데이터 코드 및 GIF 보기</summary>
<div align="center">

<img src="https://github.com/user-attachments/assets/6f860190-2d74-4fc4-87b4-1099ad700046" alt="캐릭터 데이터 " width="600"/>
</div>

  ### 1️⃣ 캐릭터 데이터 클래스

  ```csharp
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
```

  ### 2️⃣ 초기화 예시

  ```csharp
void Start()
{
    CharacterData data = CreateCharacterData();
    SetData(data);
    UpdatePlayerStats();
}
```
</details>

---

### 🎒 인벤토리 UI 시스템
 - 슬롯 수 초기화 및 UI 동적 생성
 - 인벤토리 아이템 UI 갱신
 - 장비 장착 여부 표시
 - 포션 사용 시 UI 반영
<details>
<summary>🔽 인벤토리 UI 시스템 코드 및 GIF 보기</summary>

  ### 1️⃣ 전체 슬롯 UI 갱신

  ```csharp
public void RefreshAllSlots()
{
    List<Item> items = GameManager.Instance.Player().Inventory;

    for (int i = 0; i < _slotCount; i++)
    {
        if (i < items.Count)
        {
            bool isEquipped = GameManager.Instance.Player().IsEquipped(items[i]);
            _inventory[i].SetItem(items[i], isEquipped);
        }
        else
        {
            _inventory[i].SetItem(null);
        }

        _inventory[i].gameObject.SetActive(true);
    }

    UpdateCurrentSlotCount();
}
```

 ### 2️⃣ 장비 장착 및 해제
<div align="center">
<img src="https://github.com/user-attachments/assets/7419b871-06de-493e-97e8-18c0e03ffee5" alt="장비 장착 및 해제" width="600"/>
</div>

  ```csharp
public void Equip(Item item)
{
    if (item.Type != ItemType.Equipment) return;
    if (!Inventory.Contains(item)) return;
    if (_equippedItems.Contains(item)) return;

    // 같은 타입 장비 해제
    Item checkItem = _equippedItems.FirstOrDefault(i => i.Type == item.Type);
    if (checkItem != null)
    {
        _equippedItems.Remove(checkItem);
    }

    _equippedItems.Add(item);
}
```

 ### 3️⃣ 슬롯 클릭시 장비 장착/해제 혹은 포션 사용
<div align="center">
<img src="https://github.com/user-attachments/assets/23545b53-c6c2-4f93-8534-bf656d63f6d9" alt="슬롯 클릭시 장비 장착/해제 혹은 포션 사용" width="600"/>
</div>

  ```csharp
private void ClickSlot()
{
    if (_item == null) return;

    Character player = GameManager.Instance.Player();

    if (_item.Type == ItemType.Equipment)
    {
        if (_isEquipped)
        {
            player.UnEquip(_item);
            _isEquipped = false;
        }
        else
        {
            player.Equip(_item);
            _isEquipped = true;
        }
    }
    else if (_item.Type == ItemType.Potion)
    {
        player.UseItem(_item);
        UIManager.Instance.UIInventory().ItemUsed(_item);
        return;
    }

    UIManager.Instance.UIInventory().RefreshAllSlots();
    UIManager.Instance.UIStatus().UpdateStatusUI(player);
}
```
</details>

---

## 💰 아이템 뽑기 기능
 - 아이템 뽑기는 캐릭터가 골드를 소모해 무작위 아이템을 뽑는 가챠 시스템 사용용
<details>
<summary>🔽 캐릭터 데이터 코드 및 GIF 보기</summary>
<div align="center">

<img src="https://github.com/user-attachments/assets/6f860190-2d74-4fc4-87b4-1099ad700046" alt="캐릭터 데이터 " width="600"/>
</div>

  ### 1️⃣ 가챠 핵심 로직

  ```csharp
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
```
</details>

