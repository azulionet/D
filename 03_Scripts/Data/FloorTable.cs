using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;


public class FloorData
{
	public FloorData(int a_nFloor, int a_nRoomCount, bool a_bWaepon, bool a_bDining, eID a_eNPC = eID.None, eID a_eBossID = eID.None)
	{
		nID = a_nFloor;
		nRoomCount = a_nRoomCount;

		bHasWaeponRoom = a_bWaepon;
		bHasDiningRoom = a_bDining;

		eNPC = a_eNPC;
		eBossID = a_eBossID;
	}

	public int nID; // 층수와 동일
	public int nRoomCount;
	
	public bool bHasWaeponRoom;
	public bool bHasDiningRoom;

	public eID eNPC;
	public eID eBossID;
}

public static class FloorTable
{
	static Dictionary<int, FloorData> m_mapFloor = new Dictionary<int, FloorData>();

	static FloorTable()
	{
		FloorData data = null;

		data = new FloorData(1, 15, true, true, eID.N_Waepon);
		m_mapFloor.Add(data.nID, data);
		
		data = new FloorData(2, 12, true, true, eID.N_SkillPoint);
		m_mapFloor.Add(data.nID, data);

		data = new FloorData(3, 3, false, false, eID.None, eID.B_Belial);
		m_mapFloor.Add(data.nID, data);
	}

	public static FloorData GetData(int a_nFloor)
	{
		if (m_mapFloor.ContainsKey(a_nFloor) == false)
		{
			return null;
		}

		return m_mapFloor[a_nFloor];
	}
}