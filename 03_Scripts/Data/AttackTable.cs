using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class AttackData // 몸으로 삐비대는 바디어택은 여기에 없음
{
	public eAttack				eID;
	public string				strAniName; // 애니매이션 이름
	public bool					bIsBullet; // 총알일 경우 불렛 매니져에 넣음, 아니라면 컷당 공격
	
	public Rect					stRt; // 범위
	
	public int					nCutID; // 원거리공격시 eBullet 아니면 eWaeponCut
	public List<AttackCutData>	liCutData = new List<AttackCutData>();
}

public class AttackCutData
{
	public eWaeponCut			eID;
	public int					nCut;

	public string				strSprite;
	public int					nFrame; // 프레임 범위, 0이면 쭉
	public bool					bAtk; // 판정이 있는지 없는지
}

public class BulletData
{
	public eBullet				eID;
	public int					nAtlas;
	public string				strSpriteName;
	public float				fLifeTime;
	public float				fSpeed;
	public float				fWidth;
	public float				fHeight;
}

public static class AttackTable
{
	private static Dictionary<eAttack, AttackData>				m_mapAtk = new Dictionary<eAttack, AttackData>();
	private static Dictionary<eBullet, BulletData>				m_mapBullet = new Dictionary<eBullet, BulletData>();
	private static Dictionary<eWaeponCut, List<AttackCutData>>	m_mapAtkCut = new Dictionary<eWaeponCut, List<AttackCutData>>();

	static AttackTable()
	{
		// AttackData -----------------------------------------------------------------------------
		AttackData atkData = null;

		atkData = new AttackData();
		atkData.eID = eAttack.ShortSword;
		atkData.strAniName = "";
		atkData.bIsBullet = false; // 총알일 경우 불렛 매니져에 넣음, 아니라면 
		atkData.nCutID = (int)eWaeponCut.ShortSword;
		atkData.liCutData = null;
		atkData.stRt = new Rect(0, 0, 1.5f, 1.5f);

		m_mapAtk.Add(atkData.eID, atkData);

		atkData = new AttackData();
		atkData.eID = eAttack.Gun;
		atkData.bIsBullet = true; // 총알일 경우 불렛 매니져에 넣음, 아니라면 
		atkData.nCutID = (int)eBullet.Normal;
		atkData.liCutData = null;
		atkData.stRt = new Rect(0, 0, 1.5f, 1.5f);

		m_mapAtk.Add(atkData.eID, atkData);


		atkData = new AttackData();
		atkData.eID = eAttack.MonsterWaepon;
		atkData.bIsBullet = false; // 총알일 경우 불렛 매니져에 넣음, 아니라면 
		atkData.nCutID = (int)eWaeponCut.ShortSword;
		atkData.stRt = new Rect(0, 0, 1.5f, 1.5f);

		m_mapAtk.Add(atkData.eID, atkData);

		atkData = new AttackData();
		atkData.eID = eAttack.MonsterBullet;
		atkData.bIsBullet = true; // 총알일 경우 불렛 매니져에 넣음, 아니라면 
		atkData.nCutID = (int)eBullet.Normal;
		atkData.stRt = new Rect(0, 0, 1.5f, 1.5f);

		m_mapAtk.Add(atkData.eID, atkData);


		// BulletData -----------------------------------------------------------------------------
		BulletData bulletData = null;

		bulletData = new BulletData();
		bulletData.eID = eBullet.Normal;
		bulletData.strSpriteName = "coinBronze";
		bulletData.fLifeTime = 3.0f;
		bulletData.fSpeed = 5;
		bulletData.fWidth = 6;
		bulletData.fHeight = 6;

		m_mapBullet.Add(bulletData.eID, bulletData);
	}

	public static AttackData GetData(eAttack a_eID)
	{
		if (m_mapAtk.ContainsKey(a_eID) == false)
		{
			return null;
		}

		return m_mapAtk[a_eID];
	}
	
	public static BulletData GetData(eBullet a_eID)
	{
		if (m_mapBullet.ContainsKey(a_eID) == false)
		{
			return null;
		}

		return m_mapBullet[a_eID];
	}
}
