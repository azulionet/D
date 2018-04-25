using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class NPCData
{
	public NPCData(int a_nID, string a_strName)
	{
		nID = a_nID;
		strName = a_strName;
	}

	public int		nID;
	public string	strName;
}

public static class NPCTable
{
	static Dictionary<eID, NPCData> m_mapNPC = new Dictionary<eID, NPCData>();

	static NPCTable()
	{
		m_mapNPC.Add(eID.N_VilleageChief,	new NPCData((int)eID.N_VilleageChief, "촌장"));
		m_mapNPC.Add(eID.N_Waepon,			new NPCData((int)eID.N_Waepon, "무기판매원"));
		m_mapNPC.Add(eID.N_SkillPoint,		new NPCData((int)eID.N_SkillPoint, "스킬"));
	}

	public static NPCData GetData(eID a_eNPC)
	{
		if( m_mapNPC.ContainsKey(a_eNPC) == false )
		{
			return null;
		}

		return m_mapNPC[a_eNPC];
	}
}
