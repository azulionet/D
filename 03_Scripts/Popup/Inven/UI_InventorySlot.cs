using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
		Debug.LogError("m over");
	}

	public void OnClickInven_MouseOut()
	{
		Debug.LogError("m out");
	}

	public void OnClickInven()
	{
		Debug.LogError("Click inven");
	}

}
