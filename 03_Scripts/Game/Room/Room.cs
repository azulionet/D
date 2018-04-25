using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class Room : IFixedUpdate
{
	#region UI

	RoomInfo	m_refRoom = null;

	#endregion

	ST_Room m_stRoomData = null;
	
	int m_nX = 0;
	int m_nY = 0;
	int m_nKey = 0;

	public List<FunctionWall> m_refWall;
	public LinkedList<BasicMonster> m_liMonster = new LinkedList<BasicMonster>();

	public GameObject m_objPoolRoot;
	bool m_bSettingFlag = false;

	public int nCoordKye { get { return m_nKey; } }
	public int nMonsterCount
	{
		get
		{
			if( m_refRoom == null ) { return 0; }
			else					{ return m_refRoom.m_objMosnterPos.Count; }
		}
	}

	public Room(int a_nMapCoordX, int a_nMapCoordY)
	{
		m_nX = a_nMapCoordX;
		m_nY = a_nMapCoordY;

		m_nKey = m_nX.GetCoordKey(m_nY);
	}

	public void SetData_InTown(RoomInfo a_refRoomInfo)
	{
		m_refRoom = a_refRoomInfo;
		m_stRoomData = new ST_Room();
		m_refRoom.SetData(m_stRoomData);

		InitRoomObject();
	}

	public void SetData(GameObject a_objRoot, GameObject a_objCharacterRoot, ST_Room a_stRoom)
	{
		m_stRoomData = a_stRoom;
		
		// 타일 구성
		string strPrefabName = string.Format("Rooms/Room_{0:000}", m_stRoomData.nRoomID);

		var objRoom = a_objRoot.Instantiate_asChild(strPrefabName);
		objRoom.name = string.Format("{0}_{1}_{2}", objRoom.name, a_stRoom.nX, a_stRoom.nY);

		m_refRoom = objRoom.GetComponent<RoomInfo>();
		m_refRoom.SetData(m_stRoomData);

		// 몬스터 루트 설정
		m_objPoolRoot = a_objCharacterRoot;

		InitRoomObject();
		SetVisible(false);
	}

	public void InitRoomObject()
	{
		// 몬스터 위치
		var liMonster = m_refRoom.m_objMosnterPos;

		for (int i = 0; i < liMonster.Count; ++i)
		{
			liMonster[i].bIsDie = false;
		}

		// 벽
		m_refWall = m_refRoom.m_liWall;

		// SetVisible(true) 일 때 세팅관련 플래그
		m_bSettingFlag = false;
	}

	// 몬스터의 transform.parent를 바꿈
	public void SetVisible(bool a_bVisible)
	{
		m_refRoom.SetVisible(a_bVisible);

		if( a_bVisible == true )
		{
			if (m_liMonster.Count > 0)
			{
				var node = m_liMonster.GetEnumerator();
				int nIndex = 0;

				while (node.MoveNext())
				{
					var monster = node.Current;

					var spawnData = m_refRoom.m_objMosnterPos[nIndex];

					if (spawnData.bIsDie == false)
					{
						monster.SetData(spawnData.eID, nIndex);
						monster.gameObject.SetActive(true);
					}
					else
					{
						monster.gameObject.SetActive(false);
					}

					monster.gameObject.transform.localPosition = spawnData.vcLocalPos;

					++nIndex;
				}
			}
		}
		else
		{
			if (m_liMonster.Count > 0)
			{
				var node = m_liMonster.GetEnumerator();

				while (node.MoveNext())
				{
					node.Current.gameObject.SetActive(false);
				}
			}
		}
	}

	public void DoFixedUpdate(float a_fDeltaTime)
	{
		if( m_refWall != null )
		{
			for( int i=0; i<m_refWall.Count; ++i )
			{
				m_refWall[i].DoFixedUpdate(a_fDeltaTime);
			}
		}

		if( m_liMonster.Count > 0 )
		{
			var node = m_liMonster.GetEnumerator();

			while( node.MoveNext() )
			{
				node.Current.DoFixedUpdate(a_fDeltaTime);
			}
		}
	}

	public Vector3 GetSpawnPos(eDir a_eDir)
	{
		return m_refRoom.m_arrSpawnRoot[a_eDir.ToIndex()].transform.localPosition;
	}
}
