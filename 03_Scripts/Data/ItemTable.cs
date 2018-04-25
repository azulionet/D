using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

// 인벤토리에 들어오는 아이템 테이블
public class ItemData
{
	public Global_Define.eID	eID;
	public string				strName;
	public string				strSpriteName;
}

public static class ItemTable
{
	public static eID nDefaultWaeponID = eID.E_Gun;
	
	static Dictionary<eID, ItemData> m_mapItem = new Dictionary<eID, ItemData>();

	static ItemTable()
	{
		ItemData data = null;

		// 장비 ----------------------------------------------------------
		data = new ItemData();
		data.eID = eID.E_ShotSword;
		data.strName = "숏소드";
		data.strSpriteName = "flagBlue_down";
		m_mapItem.Add(data.eID, data);

		data = new ItemData();
		data.eID = eID.E_Gun;
		data.strName = "총";
		data.strSpriteName = "keyGreen";
		m_mapItem.Add(data.eID, data);

		// 악세사리 ----------------------------------------------------------
		data = new ItemData();
		data.eID = eID.A_Gem;
		data.strName = "젬";
		data.strSpriteName = "gemBlue";	
		m_mapItem.Add(data.eID, data);

		data = new ItemData();
		data.eID = eID.A_Key;
		data.strName = "키";
		data.strSpriteName = "keyBlue";
		m_mapItem.Add(data.eID, data);

		// coinBronze, coinGold, coinSilver, flagBlue1,
		// , flagGreen1, flagRed_down, flagRed1,
		// flagYellow_down, flagYellow1
		// , gemGreen, gemRed, gemYellow,
		// keyBlue, keyGreen, keyRed, keyYellow, star
	}

	public static ItemData GetData(eID a_eID)
	{
		if( m_mapItem.ContainsKey(a_eID) == false )
		{
			return null;
		}

		return m_mapItem[a_eID];
	}
}
