using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;

using Global_Define;

public static class ExtensionMethod
{
	// GameObject
	public static GameObject Instantiate_asChild(this GameObject a_objParent, string a_strPrefabName)
	{
		if (string.IsNullOrEmpty(a_strPrefabName) == true)
		{
			Debug.LogError("arg error");
			return null;
		}

		string FileName_withPath = string.Format(Path.PREFAB_PATH_ADD, a_strPrefabName);

		GameObject objPrefab = Resources.Load(FileName_withPath) as GameObject;

		if( objPrefab == null )
		{
			Debug.LogError(string.Format("logic error - no prefab name {0}", FileName_withPath));
			return null;
		}
		
		return a_objParent.Instantiate_asChild(objPrefab);
	}

	public static GameObject Instantiate_asChild(this GameObject a_objParent, GameObject a_objPrefab)
	{
		GameObject objChild = GameObject.Instantiate(a_objPrefab) as GameObject;

		if( objChild != null )
		{
			objChild.transform.parent			= a_objParent.transform;
			objChild.transform.localPosition	= Vector3.zero;
			objChild.transform.localRotation	= Quaternion.identity;
			objChild.transform.localScale		= Vector3.one;
			objChild.layer						= a_objParent.layer;
		}

		return objChild;
	}

	// transform
	public static void SetTag_AllChildren(this Transform a_objParent, string a_strTag)
	{
		a_objParent.tag = a_strTag;

		if( a_objParent.childCount > 0 )
		{
			foreach( Transform t in a_objParent )
			{
				SetTag_AllChildren(t, a_strTag);
			}
		}
	}

	// enum
	public static string ToDesc(this System.Enum a_eEnumVal)
	{
		var da = (DescriptionAttribute[])(a_eEnumVal.GetType().GetField(a_eEnumVal.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
		return da.Length > 0 ? da[0].Description : a_eEnumVal.ToString();
	}

	public static int ToIndex(this eDir a_eDir)
	{
		int i = 0;
		int nVal = (int)a_eDir;

		while ((nVal & 1) != 1)
		{
			nVal >>= 1;
			++i;

			if (nVal <= 0)
			{
				break;
			}
		}

		return i;
	}

	public static int GetKeyGap(this eDir a_eDir)
	{
		int a_nReturn = 0;

		switch (a_eDir)
		{
			case eDir.Left:		{ a_nReturn = -1; } break;
			case eDir.Right:	{ a_nReturn = 1; } break;

			case eDir.Top:		{ a_nReturn = (ST_DungeonInfo.nGap); } break;
			case eDir.Bottom:	{ a_nReturn = (-ST_DungeonInfo.nGap); } break;
		}

		if( a_nReturn == 0 ) { Debug.LogError("logic error"); }

		return a_nReturn;
	}
	
	public static bool AndOperation(this eDir a_eDir, eDir a_eCheck)
	{
		return ( (a_eDir & a_eCheck) == a_eCheck );
	}
	
	// 리스트
	private static System.Random rand = new System.Random();
	public static void Shuffle<T>(this IList<T> a_list)
	{
		int n = a_list.Count;

		while (n > 1)
		{
			--n;
			int k = rand.Next(n + 1);
			T val = a_list[k];
			a_list[k] = a_list[n];
			a_list[n] = val;
		}
	}

	// int
	public static int GetCoordKey(this int a_nX, int a_nY)
	{
		return Define.GetCoordKey(a_nX, a_nY);
	}

	// 테이블
	public static StatData GetStatData(this eID a_nStatID)
	{
		return StatTable.GetData(a_nStatID);
	}

	public static MonsterData GetMonsterData(this eID a_eMonsterID)
	{
		return MonsterTable.GetData(a_eMonsterID);
	}

	public static ItemData GetItemData(this eID a_eItemID)
	{
		return ItemTable.GetData(a_eItemID);
	}

	public static List<NPCTextData> GetNPCTextData(this int a_nScene)
	{
		return NPCTextTable.GetData(a_nScene);
	}

	public static NPCData GetNPCData(this eID a_eNPC)
	{
		return NPCTable.GetData(a_eNPC);
	}

	public static AttackData GetAttackData(this eID a_eID)
	{
		return AttackTable.GetData(a_eID.GetStatData().eAtkTableID);
	}

	public static AttackData GetAttackData(this eAttack a_eID)
	{
		return AttackTable.GetData(a_eID);
	}

	public static BulletData GetBulletData(this eBullet a_eBullet)
	{
		return AttackTable.GetData(a_eBullet);
	}

	public static FloorData GetFloorData(this int a_nFloor)
	{
		return FloorTable.GetData(a_nFloor);
	}
}
