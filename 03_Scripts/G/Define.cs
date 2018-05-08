using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.U2D;

namespace Global_Define
{
	// const 값, enum  -----------------------------------------------------------
	static public partial class Define
	{
		public const int nPLAYER_PIXEL_SIZE = 32;
		public const int nRANDOM_POOL = 1024;
	}

	static public class Path
	{
		public const string PREFAB_PATH		= "Prefab\\";
		public const string PREFAB_PATH_ADD	= "Prefab\\{0}";

		public const string ATLAS_PATH = "Atlas\\";
		public const string ATLAS_PATH_ADD = "Atlas\\{0}";
	}

	public enum eLog
	{
		Log,
		Warning,
		Error,
	}
	
    public enum eScene
	{
		None = -1,

		[Description("Start")]
		Start = 0,

		[Description("Town")]
		Town = 1,

		[Description("Dungeon")]
		Dungeon = 2,
	}

	public enum eEquipPos
	{
		None = -1,

		eMainHand01,
		eSubHand01,

		eMainHand02,
		eSubHand02,

		eAccessary01,
		eAccessary02,
		eAccessary03,
		eAccessary04,
		eAccessary05,

		Max,
	}

	public enum eSwap
	{
		None = -1,
		First = 0,
		Second,

		Max
	}
	
	[Flags]
	public enum eDir
	{
		None = 0,

		Left = 1,
		Top = 2,
		Right = 4,
		Bottom = 8,
	}

	public enum eRoomState
	{
		None = -1,

		First,
		Last,
		NPC,
		Boss,
	}

	public enum eLandType
	{
		Land,
		Flying,
		Ghost,
	}

	public enum eDamageSource
	{
		Player,
		Monster,

		PlayerSkill,
		MonsterSkill,
	}

	public enum eID
	{
		CategoryGap = 10000,
		None = -1,

		___Category___ = 0,

		Hero		= 1,
		NPC			= 2,
		Dungeon_NPC = 3,
		Monster		= 4,
		MiddleBoss	= 5,
		Boss		= 6,
		Equip		= 7,
		Accessary	= 8,
		Attack		= 9,
		Bullet		= 10,

		CategoryLast,
		CategoryCount = CategoryLast-1,
		
		___Hero___	= Hero * CategoryGap,

		H_Adventurer,
		H_Digger,


		___NPC___ = NPC * CategoryGap,

		N_VilleageChief,
		N_Waepon,
		N_SkillPoint,


		___DUNGEON_NPC___ = Dungeon_NPC * CategoryGap,

		N_DungeonWaepon,
		N_DungeonDining,


		___MONSTER___ = Monster * CategoryGap,

		M_Skeleton,
		M_SkeletonArcher,
		M_Ghost,
		M_Banshee,
		M_Bat,
		M_BigBat,

		M_BulletMonster,

		___MIDDLE_BOSS___ = MiddleBoss * CategoryGap,


		___BOSS___ = Boss * CategoryGap,

		B_Belial,

		
		___EQUIP___ = Equip * CategoryGap,

		E_ShotSword,
		E_Gun,


		___ACCESSARY___ = Accessary * CategoryGap,

		A_Gem,
		A_Key,
	}

	public enum eAttack
	{
		None = -1,

		BasicFist = 0,	// 맨주먹
		ShortSword = 1,	// 숏소드
		Gun = 2,		// 총

		// 몹 공격
		MonsterWaepon = 101,
		MonsterBullet = 102,
	}

	public enum eBullet
	{
		None = -1,

		Normal = 1,
	}

	public enum eWaeponCut
	{
		BasicFist = 0,
		ShortSword = 1,
	}

	public enum eAtlas
	{
		CategoryGap = 100,

		___Hero___ = eID.Hero * CategoryGap,
		
		___NPC___ = eID.NPC * CategoryGap,

		___Dungeon_NPC___ = eID.Dungeon_NPC * CategoryGap,

		___Monster___ = eID.Monster * CategoryGap,

		___MiddleBoss___ = eID.MiddleBoss * CategoryGap,

		___Boss___ = eID.Boss * CategoryGap,

		___Equip___ = eID.Equip * CategoryGap,

		Weapon01,

		___Accessary___ = eID.Accessary * CategoryGap,
		
		___Attack___ = eID.Attack * CategoryGap,
		
		___Bullet___ = eID.Bullet * CategoryGap,

		Bullet01,
		
	}
	
	// 인터페이스 -----------------------------------------------------------

	public interface ICleanUp
	{
		void			ChangeSceneCleanUp();
	}

	public interface IUpdate
	{
		void			DoUpdate(float a_fDelta);
	}

	public interface IFixedUpdate
	{
		void			DoFixedUpdate(float a_fDelta);
	}

    public interface IAttackInfo
    {
		bool			GetDir();
		ST_AttackInfo	AttackInfo();
    }

	public interface IAtlas
	{
		SpriteAtlas		GetAtlas(eAtlas a_eAtlas);
		Sprite			GetSprite(eAtlas a_eAtlas, string a_strSpriteName);
	}
	
	// 유틸 함수 -----------------------------------------------------------

	static public partial class Define
	{
		public static int GetCoordKey(int a_nX, int a_nY)
		{
			return ((a_nY * ST_DungeonInfo.nGap) + a_nX);
		}

		public static bool IsInRange(Vector3 a_vcPos1, Vector3 a_vcPos2, float a_fRange) // 2D라서 z축이 필요없습니다.
		{
			float fX = a_vcPos1.x - a_vcPos2.x;
			float fY = a_vcPos1.y - a_vcPos2.y;

			return ((a_fRange * a_fRange) > (fX * fX + fY * fY));
		}

		public static void Attack_ToMonster(ref ST_GameStat a_refMonsterStat, ST_AttackInfo a_refAttackData)
		{
			a_refMonsterStat.nNowHP -= a_refAttackData.m_nDamage;
		}

		public static void Attack_ToPlayer(ref ST_GameStat a_refPlayerStat, ST_AttackInfo a_refAttackData)
		{
			a_refPlayerStat.nNowHP -= a_refAttackData.m_nDamage;
		}
		
		[System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void Log(string a_strLog, eLog a_eLogType = eLog.Log) // 로그성 데이터
		{
			switch (a_eLogType)
			{
				case eLog.Log:
				{
					Debug.Log(a_strLog);
				} break;
				case eLog.Warning:
				{
					Debug.LogWarning(a_strLog);
				} break;
				case eLog.Error:
				{
					Debug.LogError(a_strLog);
				} break;
			}
		}

		public static void LogError(string a_strLog) // 에러는 무조건 출력하는게 맞음
		{
			Debug.LogError(a_strLog);
		}
	}

	static public class Rand // 만분율 기준 0~9999까지 저장
	{
		private static int m_nIndex = 0;
		private static int[] m_nArr = new int[Define.nRANDOM_POOL];

		static Rand()
		{
			for (int i = 0; i < Define.nRANDOM_POOL; ++i)
			{
				m_nArr[i] = UnityEngine.Random.Range(0, 10000);
			}
		}

		public static void CreatePool() { }

		private static int nIndex
		{
			get
			{
				int nTemp = m_nIndex++;

				if( m_nIndex >= Define.nRANDOM_POOL ) { m_nIndex = 0; }

				return nTemp;
			}
		}

		public static int Random()						{ return m_nArr[nIndex]; }

		public static float Percent()					{ return m_nArr[nIndex] * 0.0001f; }
		public static bool Percent(int a_nPercent)		{ return m_nArr[nIndex] <= (a_nPercent * 100); }
		public static bool Permile(int a_nPermile)		{ return m_nArr[nIndex] <= (a_nPermile * 10); }
		public static bool Permilad(int a_nPermilad)	{ return m_nArr[nIndex] <= a_nPermilad; }
		public static int Range(int a_nStart, int a_nEnd) // start : inclusive, end : exclusive
		{
			if (a_nStart > a_nEnd)
			{
				int nTemp = a_nStart;
				a_nStart = a_nEnd;
				a_nEnd = nTemp;
			}

			return (Random() % (a_nEnd - a_nStart)) + a_nStart; 
		}
	}
	
	// 구조체성 클래스 -----------------------------------------------------------

	// Save, Load 및 기본 플레이어 데이터
	public class ST_Player
	{
		public List<eID>	liRescuedNPC = new List<eID>();
		public string		strFloor;
		public int			nMoney;
	}

	public class ST_GameStat : StatData
	{
		public int			nNowHP;
		public int			nNowHungry;
		public int			nNowDash;
		public int			nNowJumpCount;

		public float		fAtkTerm;

		public ST_GameStat(StatData a_refData) : base(a_refData)
		{
			nNowHP = nMaxHP;
			nNowHungry = nMaxHungry;
			nNowDash = nMaxDashCount;
			nNowJumpCount = 0;

			fAtkTerm = 1.0f;
		}

		public ST_GameStat(ST_GameStat a_refData)
		{
			CopyStat(a_refData);
		}

		public ST_GameStat()
		{
			Clear();
		}

		public override void Clear()
		{
			base.Clear();

			nNowHP = 90;
			nNowHungry = 0;
			nNowDash = 0;
			nNowJumpCount = 0;
		}

		public void CopyStat(ST_GameStat a_refStat)
		{
			base.CopyStat(a_refStat);

			nNowHP			= a_refStat.nNowHP;
			nNowHungry		= a_refStat.nNowHungry;
			nNowDash		= a_refStat.nNowDash;
			nNowJumpCount	= a_refStat.nNowJumpCount;
		}

		public void AddStat(ST_GameStat a_refStat)
		{
			base.AddStat(a_refStat);

			nNowHP			+= a_refStat.nNowHP;
			nNowHungry		+= a_refStat.nNowHungry;
			nNowDash		+= a_refStat.nNowDash;
			nNowJumpCount	+= a_refStat.nNowJumpCount;
		}
	}

	public class ST_AttackInfo
    {
		public eDamageSource eSource;
        public int m_nDamage;
		public bool m_bRight;
    }

	public class ST_EquipInfo // 실제 장비한 장비정보
	{
		List<eID> vecEquip = new List<eID>();
		eSwap eNowSwapSlot = eSwap.First;
		StatData stStat = new StatData();

		bool bChange = false;

		public ST_EquipInfo()
		{
			Clear();
		}

		public eSwap eNowSwap	{ get { return eNowSwapSlot; } }

		public eID eMainHand1 { get { return vecEquip[(int)eEquipPos.eMainHand01]; } }
		public eID eSubHand1 { get { return vecEquip[(int)eEquipPos.eSubHand01]; } }

		public eID eMainHand2 { get { return vecEquip[(int)eEquipPos.eMainHand02]; } }
		public eID eSubHand2 { get { return vecEquip[(int)eEquipPos.eSubHand02]; } }

		public bool bIsFirst	{ get { return (eNowSwap == eSwap.First); } }

		public eID eMainHand	{ get { return (bIsFirst ? eMainHand1 : eMainHand2); } }
		public eID eSubHand		{ get { return (bIsFirst ? eSubHand1 : eSubHand2); } }
		
		public eID GetID(eEquipPos ePos) { return vecEquip[(int)ePos]; }
		public eID GetID(int nPos) { return vecEquip[nPos]; }

		public void Clear()
		{
			vecEquip.Clear();

			for (int i = 0; i < (int)eEquipPos.Max; ++i)
			{
				vecEquip.Add(0);
			}

			stStat.Clear();

			eNowSwapSlot = eSwap.First;
			bChange = true;
		}

		public void SetEquip(eEquipPos a_ePos, eID a_eID)
		{
			vecEquip[(int)a_ePos] = a_eID;

			bChange = true;
		}

		public StatData Swap()
		{
			return GetStat((eNowSwapSlot == eSwap.First) ? eSwap.Second : eSwap.First);
		}

		public StatData GetStat(eSwap a_eSlot = eSwap.None)
		{
			if(a_eSlot != eNowSwapSlot || bChange == true )
			{
				stStat.Clear();

				eSwap eSwapSlot = (a_eSlot == eSwap.None) ? eNowSwapSlot : a_eSlot;

				if (eSwapSlot == eSwap.First)
				{
					if (vecEquip[(int)eEquipPos.eMainHand01] > 0)
					{
						stStat.AddStat(eMainHand1.GetStatData());
					}
					if (vecEquip[(int)eEquipPos.eSubHand01] > 0)
					{
						stStat.AddStat(eSubHand1.GetStatData());
					}
				}
				else
				{
					if (vecEquip[(int)eEquipPos.eMainHand02] > 0)
					{
						stStat.AddStat(eMainHand2.GetStatData());
					}
					if (vecEquip[(int)eEquipPos.eSubHand02] > 0)
					{
						stStat.AddStat(eSubHand2.GetStatData());
					}
				}

				for ( int i=(int)eEquipPos.eAccessary01; i<(int)eEquipPos.Max; ++i )
				{
					if( vecEquip[i] > 0 )
					{
						stStat.AddStat(vecEquip[i].GetStatData());
					}
				}

				eNowSwapSlot = eSwapSlot;
				bChange = false;
			}

			return stStat;
		}
	}
	
	public class ST_Room
	{
		public int nX;
		public int nY;

		public int nRoomID;
		public eRoomState eState;

		public eID	eExistNPC;
		public eDir	eOpenDir;
		public bool bPortal;

		public ST_Room()
		{
			Clear();
		}

		public void Clear()
		{
			nX = 0;
			nY = 0;

			nRoomID = 0;
			eState = eRoomState.None;

			eExistNPC = eID.None;
			eOpenDir = eDir.None;
			bPortal = false;
		}

		public bool bNoNPC
		{ 
			get
			{
				return ( (eState == eRoomState.First) ||
				(eState == eRoomState.Last) ||
				(eState == eRoomState.Boss) );
			} 
		}
		public bool bHasNPC { get { return (eExistNPC != eID.None); } }
		public bool bHasPortal { get { return bPortal; } }
	}

	public class ST_DungeonInfo
	{
		public const int nGap = 1000;
		
		public Dictionary<int, ST_Room> m_mapRoom = new Dictionary<int, ST_Room>();
		public List<int> m_liCoordKey = new List<int>();
		
		public int m_nFloor = 0;
		public int m_nRoomCount = 0;
		public int m_nStartRoomKey = 0;
		
		public void CreateDungeon(int a_nFloor, int a_nRoomCount, List<eID> a_liNPC)
		{
			Clear();
			m_nFloor = a_nFloor;
			m_nRoomCount = a_nRoomCount;
			m_nStartRoomKey = Define.GetCoordKey(0, 0);

			RoomMaker.CreateRoom(ref m_mapRoom, ref m_liCoordKey, a_nFloor, a_nRoomCount);

			m_mapRoom[m_nStartRoomKey].eState = eRoomState.First;
			m_mapRoom[m_nStartRoomKey].bPortal = true;

			// 마지막 방 설정
			var node = m_mapRoom.GetEnumerator();

			ST_Room lastRoom = null;
			while ( node.MoveNext() )
			{
				lastRoom = node.Current.Value;
			}
			
			lastRoom.eState = eRoomState.Last;
			lastRoom.bPortal = true;
			
			m_liCoordKey.Shuffle();

			int nIndex = 0;
			for( int i=0 ; i<a_liNPC.Count; ++i )
			{
				var room = m_mapRoom[m_liCoordKey[nIndex]];

				if( (room.bHasNPC == true) ||
					(room.bNoNPC == true) )
				{
					++nIndex;
					--i;

					continue;
				}

				room.eExistNPC = a_liNPC[i];
				
				// 식당, 무기상인한테는 무조건 포탈 존재
				if(room.eExistNPC == eID.N_DungeonDining || room.eExistNPC == eID.N_DungeonWaepon)
				{
					room.bPortal = true;
				}
			} 
		}

		public void Clear()
		{
			m_mapRoom.Clear();
			m_liCoordKey.Clear();

			m_nFloor = 0;
			m_nRoomCount = 0;
			m_nStartRoomKey = 0;
		}
	}

	static public class RoomMaker
	{
		// 방생성시 필요한 인자
		private static int m_nRoomCheck = 0;
		private static Dictionary<int, ST_Room> m_refMapRoom = null;
		private static List<int> m_refLiCoord = null;

		public static void CreateRoom(ref Dictionary<int, ST_Room> a_refInfo, ref List<int> a_liCoord, int a_nFloor, int a_nRoomCount)
		{
			Clear();
			a_refInfo.Clear();
			a_liCoord.Clear();
			
			m_refMapRoom = a_refInfo;
			m_refLiCoord = a_liCoord;
			m_nRoomCheck = a_nRoomCount;

			ST_Room stRoom = AddRoom(0, 0);
			
			// 방 생성
			while( true )
			{
				RecursiveCreateRoom(ref stRoom);

				if( m_nRoomCheck <= 0 )
				{
					break;
				}
			}
			
			Clear();
		}

		public static void Clear()
		{
			m_nRoomCheck = 0;
			m_refMapRoom = null;
		}

		public static void RecursiveCreateRoom(ref ST_Room a_stRoom, eDir a_eDir = eDir.None)
		{
			if( a_eDir != eDir.None ) { a_stRoom.eOpenDir |= a_eDir; }

			// 위
			if ( Rand.Percent(50) == true )
			{
				var room = AddRoom(a_stRoom.nX, a_stRoom.nY + 1);

				if( room != null )
				{
					a_stRoom.eOpenDir |= eDir.Top;
					RecursiveCreateRoom(ref room, eDir.Bottom);
				}
			}

			// 왼쪽
			if( (a_stRoom.nX > 0) &&
				(Rand.Percent(50) == true) )
			{
				var room = AddRoom(a_stRoom.nX - 1, a_stRoom.nY);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Left;
					RecursiveCreateRoom(ref room, eDir.Right);
				}
			}

			// 오른쪽
			if( Rand.Percent(50) == true )
			{
				var room = AddRoom(a_stRoom.nX + 1, a_stRoom.nY);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Right;
					RecursiveCreateRoom(ref room, eDir.Left);
				}
			}

			// 아래
			if( (a_stRoom.nY > 0) &&
				( Rand.Percent(50) == true ) )
			{
				var room = AddRoom(a_stRoom.nX, a_stRoom.nY - 1);

				if (room != null)
				{
					a_stRoom.eOpenDir |= eDir.Bottom;
					RecursiveCreateRoom(ref room, eDir.Top);
				}
			}
		}

		public static ST_Room AddRoom(int a_nX, int a_nY)
		{
			if( m_nRoomCheck <= 0 ) { return null; }

			ST_Room stReturn = null;

			int nKey = a_nX.GetCoordKey(a_nY);

			if (m_refMapRoom.ContainsKey(nKey) == true)
			{
				return m_refMapRoom[nKey];
			}

			stReturn = new ST_Room();
			stReturn.nX = a_nX;
			stReturn.nY = a_nY;

			// 랜덤하게 포탈 세팅. 좌표가 아닌 방의 속성을 정하는 것이기에 여기보단 위쪽에서 하는게 맞지만 반복문이 여기서 돌아 걍 여기서함
			stReturn.bPortal = Rand.Percent(30);

			m_refLiCoord.Add(nKey);
			m_refMapRoom.Add(nKey, stReturn); 

			--m_nRoomCheck;

			return stReturn;
		}
	}

	// 
	public class DBinaryTree
	{
		int nID;



	}


}
