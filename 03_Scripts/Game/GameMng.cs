using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class GameMng : MonoBehaviour, ICleanUp
{
	#region SINGLETON
	public static bool destroyThis = false;

	static GameMng _instance = null;

	public static GameMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(GameMng)) as GameMng;
				if (_instance == null)
				{
					_instance = new GameObject("GameMng", typeof(GameMng)).GetComponent<GameMng>();
				}
			}

			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	#endregion

	// 플레이어
	public Player m_refPlayer = null;

	// 플레이어 스탯
	public ST_GameStat m_PlayerStat = new ST_GameStat();

	// 인벤토리
	public LinkedList<eID> m_liInvenItems = new LinkedList<eID>();

	// 현재 이큅된 장비 정보
	public ST_EquipInfo m_stEquip = new ST_EquipInfo();

	public eSwap eNowSwap
	{
		get { return m_stEquip.eNowSwap; }
	}

	public void ChangeSceneCleanUp() { }

	public void EnterTown()
	{
		SetDefaultPlayerStat();
	}

	public void EnterDungeon()
	{
		SetDefaultPlayerStat();
	}
	
	public void SetPlayer(Player a_refPlayer)
	{
		m_refPlayer = a_refPlayer;
	}

	public void SetDefaultPlayerStat()
	{
		// 최초 인벤, 기본칼
		m_liInvenItems.Clear();
		
		m_stEquip.SetEquip(eEquipPos.eMainHand01, ItemTable.nDefaultWaeponID);
		
		// 최초 기본 스탯 세팅
		SetHeroStat();
		
		// 스탯 적용
		StatData stStat = m_stEquip.GetStat(eSwap.First);

		m_refPlayer.SetStat(m_PlayerStat, stStat, m_stEquip);
	}

	void SetHeroStat()
	{
		// 기본 히어로 능력
		m_PlayerStat.Clear();
		m_PlayerStat.CopyStat(eID.H_Adventurer.GetStatData());

		// 스킬 적용
	}
}
