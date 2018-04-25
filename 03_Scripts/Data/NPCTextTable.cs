using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class NPCTextData
{
	public NPCTextData(int a_nScene, int a_nCut, string a_strText)
	{
		nScene = a_nScene;
		nCut = a_nCut;

		strText = a_strText;
	}

	public int nScene;
	public int nCut;
	public string strText;
}

public static class NPCTextTable
{
	static Dictionary<int, List<NPCTextData>> m_mapText = new Dictionary<int, List<NPCTextData>>();

	static NPCTextTable()
	{
		List<NPCTextData> strText = new List<NPCTextData>();

		strText.Add(new NPCTextData(1, 0, "내가 이 마을 촌장"));
		strText.Add(new NPCTextData(1, 1, "가서 마을 사람을 구출해줘"));

		m_mapText.Add(1, strText);

	}

	public static List<NPCTextData> GetData(int a_nGetData)
	{
		if (m_mapText.ContainsKey(a_nGetData) == false)
		{
			return null;
		}

		return m_mapText[a_nGetData];
	}
}
