using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class UI_InventorySlot : MonoBehaviour
{
	#region INSPECTOR

	public SpriteRenderer m_sprWaepon;

	#endregion

	ItemData m_refData = null;

	public void SetData(ItemData a_refData)
	{
		m_refData = a_refData;
		Refresh();
	}

	void Refresh()
	{
		m_sprWaepon.enabled = (m_refData != null);

		if( m_refData != null )
		{
			// m_sprWaepon.spriteName = m_refData.strSpriteName;
		}
	}

	// 메세지 처리기
	public void OnClickInven_MouseOver()
	{
		Define.Log("m over");
	}

	public void OnClickInven_MouseOut()
	{
		Define.Log("m out");
	}

	public void OnClickInven()
	{
		Define.Log("Click inven");
	}

}
