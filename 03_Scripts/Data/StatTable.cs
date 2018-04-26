using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class StatData
{
	public Global_Define.eID eID;
	public eAttack eAtkTableID;

	public string strName;
	public string strInfo;

	public int nMaxHP;
	public int nMaxHungry; // 포만감
	public int nMaxDashCount;

	public int nDmg; // 공격력최소
	public int nDmgMax; // 공격력최대
	public float fAtkSpd; // 공격속도
	public int nPower; // 위력

	public int nDef; // 방어력
	public int nBlock; // 방어
	public int nFixDef; // 강인함

	public int nCri; // 크리 확률
	public int nCriDmg; //크리댐지

	public int nAvoid; // 회피
	public int nDashAtk; // 대쉬공격시 추가댐지
	public int nMaxJump;
	public float fMove; // 이동속도
	public float fJumpForce;

	public StatData()
	{
		Clear();
	}

	public StatData(StatData a_refData)
	{
		CopyStat(a_refData);
	}

	public virtual void Clear()
	{
		eID = eID.None;
		eAtkTableID = eAttack.None;

		strName = string.Empty;
		strInfo = string.Empty;

		ClearStat();
	}

	public void ClearStat()
	{
		nMaxHP = 0;
		nMaxHungry = 0;
		nMaxDashCount = 0;

		nDmg = 0;
		fAtkSpd = 0;
		nPower = 0;

		nDef = 0;
		nBlock = 0;
		nFixDef = 0;

		nCri = 0;
		nCriDmg = 0;

		nAvoid = 0;
		nDashAtk = 0;
		nMaxJump = 0;

		fMove = 0;
		fJumpForce = 0;
	}

	public void CopyStat(StatData a_refStat)
	{
		if( a_refStat == null ) { return; }

		nMaxHP		= a_refStat.nMaxHP;
		nMaxHungry	= a_refStat.nMaxHungry;
		nMaxDashCount	= a_refStat.nMaxDashCount;

		nDmg		= a_refStat.nDmg;
		nDmgMax		= a_refStat.nDmgMax;
		fAtkSpd		= a_refStat.fAtkSpd;
		nPower		= a_refStat.nPower;

		nDef		= a_refStat.nDef;
		nBlock		= a_refStat.nBlock;
		nFixDef		= a_refStat.nFixDef;

		nCri		= a_refStat.nCri;
		nCriDmg		= a_refStat.nCriDmg;

		nAvoid		= a_refStat.nAvoid;
		nDashAtk	= a_refStat.nDashAtk;
		nMaxJump	= a_refStat.nMaxJump;

		fMove		= a_refStat.fMove;
		fJumpForce	= a_refStat.fJumpForce;
	}

	public void AddStat(StatData a_refStat)
	{
		if (a_refStat == null) { return; }

		nMaxHP		+= a_refStat.nMaxHP;
		nMaxHungry	+= a_refStat.nMaxHungry;
		nMaxDashCount	+= a_refStat.nMaxDashCount;

		nDmg		+= a_refStat.nDmg;
		nDmgMax		+= a_refStat.nDmgMax;
		fAtkSpd		+= a_refStat.fAtkSpd;
		nPower		+= a_refStat.nPower;

		nDef		+= a_refStat.nDef;
		nBlock		+= a_refStat.nBlock;
		nFixDef		+= a_refStat.nFixDef;

		nCri		+= a_refStat.nCri;
		nCriDmg		+= a_refStat.nCriDmg;

		nAvoid		+= a_refStat.nAvoid;
		nDashAtk	+= a_refStat.nDashAtk;
		nMaxJump	+= a_refStat.nMaxJump;

		fMove		+= a_refStat.fMove;
		fJumpForce	+= a_refStat.fJumpForce;
	}
}

public static class StatTable
{
	public static Dictionary<eID, StatData> m_mapStat = new Dictionary<eID, StatData>();

	static StatTable()
	{
		StatData st = null;

		// 히어로		
		st = new StatData();
		st.eID = eID.H_Adventurer;

		st.eAtkTableID = eAttack.BasicFist;
		st.strName = "모험가";
		st.strInfo = "";

		st.nMaxHP = 100;
		st.nMaxHungry = 100;
		st.nMaxDashCount = 2;
		st.nMaxJump = 2;
		st.fMove = 15.0f;
		st.fJumpForce = 1200f;

		m_mapStat.Add(st.eID, st);

		// 도굴꾼
		// eID.H_Digger

		// 몹
		st = new StatData();
		st.eAtkTableID = eAttack.MonsterWaepon;
		st.eID = eID.M_Skeleton;
		st.strName = "해골병사";
		st.strInfo = "해골병사";

		st.nMaxHP = 50;
		st.nMaxJump = 1;
		st.fMove = 5.0f;
		st.fJumpForce = 100f;
		st.nDmg = 5;
		st.nDmgMax = 8;

		m_mapStat.Add(st.eID, st);

		st = new StatData();
		st.eID = eID.M_SkeletonArcher;
		st.strName = "해골궁수";
		st.strInfo = "해골궁수";

		st.nMaxHP = 50;
		st.nMaxJump = 1;
		st.fMove = 0.1f;
		st.fJumpForce = 100f;
		st.nDmg = 5;
		st.nDmgMax = 8;

		m_mapStat.Add(st.eID, st);

		st = new StatData();
		st.eID = eID.M_BulletMonster;
		st.strName = "총알 몹";
		st.strInfo = "총알 몹";
		
		st.fAtkSpd = 1.0f;
		m_mapStat.Add(st.eID, st);


		// eID.M_Ghost,
		// eID.M_Banshee,
		// eID.M_Bat,
		// eID.M_BigBat,

		// 보스
		st = new StatData();
		st.eID = eID.B_Belial;
		st.strName = "벨리얼";
		st.strInfo = "첫 보스";

		st.nMaxHP = 50;
		st.fMove = 0f;
		st.nDmg = 10;
		st.nDmgMax = 20;

		m_mapStat.Add(st.eID, st);


		// 장비
		st = new StatData();
		st.eID = eID.E_ShotSword;
		st.eAtkTableID = eAttack.ShortSword;
		st.strName = "숏 소드";
		st.strInfo = "숏 소드";

		st.fAtkSpd = 10;
		st.nDmg = 5;
		st.nDmgMax = 8;

		m_mapStat.Add(st.eID, st);

		st = new StatData();
		st.eID = eID.E_Gun;
		st.eAtkTableID = eAttack.Gun;
		st.strName = "총";
		st.strInfo = "총";

		st.fAtkSpd = 10;
		st.nDmg = 5;
		st.nDmgMax = 8;

		m_mapStat.Add(st.eID, st);


		// 악세사리
		st = new StatData();
		st.eID = eID.A_Gem;
		st.strName = "젬";
		st.strInfo = "젬";
		st.nDef = 5;

		m_mapStat.Add(st.eID, st);

		st = new StatData();
		st.eID = eID.A_Key;
		st.strName = "키";
		st.strInfo = "키";

		st.nPower = 5;
		m_mapStat.Add(st.eID, st);
	}

	public static StatData GetData(eID a_eID)
	{
		if (m_mapStat.ContainsKey(a_eID) == false)
		{
			return null;
		}

		return m_mapStat[a_eID];
	}
}
