using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class DungeonMng : MonoBehaviour, IFixedUpdate, ICleanUp
{
	#region SINGLETON

	public static bool destroyThis = false;

	static DungeonMng _instance = null;

	public static DungeonMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(DungeonMng)) as DungeonMng;

				if (_instance == null)
				{
					_instance = new GameObject("DungeonMng", typeof(DungeonMng)).GetComponent<DungeonMng>();	
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

	public int m_nMapCoordX = 0;
	public int m_nMapCoordY = 0;

	public GameObject m_objRoomRoot = null;
	public GameObject m_objCharacterRoot = null;

	// 현재 던전정보
	public ST_DungeonInfo m_stDungeon = new ST_DungeonInfo();

	// 현재 방 정보
	public Dictionary<int, Room> m_mapRoom = new Dictionary<int, Room>();
	public Room m_refActiveRoom = null;

	// 몬스터 풀
	public MonsterPool m_cMonsterPool = new MonsterPool();

	public void Clear()
	{
		m_stDungeon.Clear();
		m_mapRoom.Clear();
		m_refActiveRoom = null;
	}

	public void ChangeSceneCleanUp()
	{
		m_cMonsterPool.CleanUp();

		m_objRoomRoot = null;
		m_objCharacterRoot = null;
	}

	public void EnterTown(RoomInfo m_refInfo, GameObject a_objRootRoot, GameObject a_objCharacterRoot)
	{
		Clear();

		m_objRoomRoot = a_objRootRoot;
		m_objCharacterRoot = a_objCharacterRoot;

		Room townRoom = new Room(0, 0);
		townRoom.SetData_InTown(m_refInfo);

		m_refActiveRoom = townRoom;

		m_refActiveRoom.SetVisible(true);
		GameMng.Ins.m_refPlayer.transform.localPosition = m_refActiveRoom.GetSpawnPos(eDir.Left);
	}

	public void EnterDungeon(GameObject a_objRootRoot, GameObject a_objCharacterRoot)
	{
		Clear();

		m_objRoomRoot = a_objRootRoot;
		m_objCharacterRoot = a_objCharacterRoot;

		m_cMonsterPool.SetData(m_objCharacterRoot);

		FloorSetting(1); // 1층 진입

		// 현 플레이어의 맵상의 좌표
		m_nMapCoordX = 0;
		m_nMapCoordY = 0;

		// 좌표별 Room 생성
		CreateRoom();

		// 방 입장
		ChangeRoom(0); // 0, 0입장
	}

	public void FloorSetting(int a_nFloor)
	{
		FloorData floor = a_nFloor.GetFloorData();
		List<eID> liNPC = new List<eID>();

		if (floor.bHasDiningRoom == true)
		{
			liNPC.Add(eID.N_DungeonDining);
		}

		if (floor.bHasWaeponRoom == true)
		{
			liNPC.Add(eID.N_DungeonWaepon);
		}

		if (floor.eNPC != eID.None)
		{
			liNPC.Add((eID)floor.eNPC);
		}

		m_stDungeon.CreateDungeon(floor.nID, floor.nRoomCount, liNPC);

		// 룸 아이디 생성
		var node = m_stDungeon.m_mapRoom.GetEnumerator();

		while( node.MoveNext() )
		{
			var st = node.Current.Value;
			st.nRoomID = Rand.Range(1, 4); // 프리팹이 1~3개밖에없음
		}
	}

	public void CreateRoom()
	{
		var node = m_stDungeon.m_mapRoom.GetEnumerator();

		while ( node.MoveNext() )
		{
			var stInfo = node.Current.Value;
			
			int nX = stInfo.nX;
			int nY = stInfo.nY;

			var room = new Room(nX, nY);

			room.SetData(m_objRoomRoot, m_objCharacterRoot, stInfo);

			int nCount = room.nMonsterCount;

			// 풀에서 몬스터 가져옴. ( 가져온다고 풀에 있는 몬스터의 링크드 리스트가 줄지는 않음 )
			m_cMonsterPool.GetMonster(nCount, room.m_liMonster);

			m_mapRoom.Add(nX.GetCoordKey(nY), room);
		}
	}
	
	public void ChangeRoom(eDir a_eDir)
	{
		int nKey = m_refActiveRoom.nCoordKye;
		nKey += a_eDir.GetKeyGap();
		
		ChangeRoom(nKey);
	}

	public void ChangeRoom_withPortal(int a_nKey)
	{
		if( m_nMapCoordX.GetCoordKey(m_nMapCoordY) == a_nKey )
		{
			return;
		}

		ChangeRoom(a_nKey);
	}
	
	public void ChangeRoom(int a_nKey)
	{
		int nX = a_nKey % ST_DungeonInfo.nGap;
		int nY = a_nKey / ST_DungeonInfo.nGap;

		if (m_refActiveRoom != null)
		{
			m_refActiveRoom.SetVisible(false);
		}

		m_nMapCoordX = nX;
		m_nMapCoordY = nY;

		CSceneMng.Ins.FadeIn(
			() => {
				m_refActiveRoom = m_mapRoom[a_nKey];
				m_refActiveRoom.SetVisible(true);
				GameMng.Ins.m_refPlayer.transform.localPosition = m_refActiveRoom.GetSpawnPos(eDir.Left);
			}
		);
	}

	// IFixedUpdate
	public void DoFixedUpdate(float a_fDeltaTime)
	{
		if (m_refActiveRoom != null)
		{
			m_refActiveRoom.DoFixedUpdate(a_fDeltaTime);
		}
	}
}
