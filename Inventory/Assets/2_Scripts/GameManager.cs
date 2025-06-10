using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private Character _player;
    public Character Player() => _player;

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
        return new CharacterData
        {
            Name = "12시간 노예",
            Level = 5,
            Gold = 1200,
            AttackPower = 30,
            Defense = 15,
            Health = 100,
            Critical = 10,
            Inventory =  new List<Item>()
        };
    }
    // 플레이어 정보 최신화
    public void UpdatePlayerStats()
    {
        UIManager.Instance.UIMainMene().UpdateMainMenuUI(_player);
        UIManager.Instance.UIStatus().UpdateStatusUI(_player);
    }
}
