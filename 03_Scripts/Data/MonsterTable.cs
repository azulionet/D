using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class MonsterData
{
    public Global_Define.eID eID;
    public string		strName;
	public string		strSpriteName;

	public eLandType	eLand;
	public float		fAttackRange; // 0일경우 몸으로 삐비대는 애들
	public eAttack		eAtkID;
	public float		fDetectRange;
	public bool			bFirstAttack; // 선공, 비선공...
}

public static class MonsterTable
{
    static Dictionary<eID, MonsterData> m_mapMob = new Dictionary<eID, MonsterData>();

    static MonsterTable()
    {
		MonsterData data = null;
		eID eID = eID.None;
		
		// 스켈레톤
		data = new MonsterData();
		data.eID = eID.M_Skeleton;

		data.strName = "해골 병사";
		data.strSpriteName = "Mob_001";
		
		data.eLand = eLandType.Land;
		data.fAttackRange = 10;
		data.fDetectRange = 400;
		data.bFirstAttack = true;
		
		m_mapMob.Add(data.eID, data);

		// 해골 궁수
		data = new MonsterData();
		eID = eID.M_SkeletonArcher;

		data.eID = eID;
		data.strName = "해골 궁수";
		data.strSpriteName = "Mob_002";

		data.eLand = eLandType.Land;
		data.fAttackRange = 100;
		data.fDetectRange = 200;
		data.bFirstAttack = true;
		
		m_mapMob.Add(data.eID, new MonsterData());

		// 유령
		data = new MonsterData();
		eID = eID.M_Ghost;

		data.eID = eID;
		data.strName = "유령";
		data.strSpriteName = "Mob_003";
		
		data.eLand = eLandType.Ghost;
		data.fAttackRange = 0;
		data.fDetectRange = 200;
		data.bFirstAttack = true;

		m_mapMob.Add(data.eID, new MonsterData());

		// 밴시
		data = new MonsterData();
		eID = eID.M_Banshee;

		data.eID = eID;
		data.strName = "밴시";
		data.strSpriteName = "Mob_004";
		
		data.eLand = eLandType.Ghost;
		data.fAttackRange = 80;
		data.fDetectRange = 200;
		data.bFirstAttack = true;

		m_mapMob.Add(data.eID, new MonsterData());
		
		// 박쥐
		data = new MonsterData();
		eID = eID.M_Bat;

		data.eID = eID;
		data.strName = "박쥐";
		data.strSpriteName = "Mob_005";
		
		data.eLand = eLandType.Flying;
		data.fAttackRange = 80;
		data.fDetectRange = 200;
		data.bFirstAttack = true;

		m_mapMob.Add(data.eID, new MonsterData());

		// 큰 박쥐
		data = new MonsterData();
		eID = eID.M_BigBat;

		data.eID = eID;
		data.strName = "큰 박쥐";
		data.strSpriteName = "Mob_006";
		
		data.eLand = eLandType.Flying;
		data.fAttackRange = 80;
		data.fDetectRange = 200;
		data.bFirstAttack = true;

		m_mapMob.Add(data.eID, new MonsterData());
	}

    public static MonsterData GetData(eID a_eMob)
    {
        if (m_mapMob.ContainsKey(a_eMob) == false)
        {
            return null;
        }

        return m_mapMob[a_eMob];
    }
}
