using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class UI_Inventory : MonoBehaviour
{
	#region INSPECTOR

	public SpriteRenderer m_sprSlotHighlight1;
	public SpriteRenderer m_sprSlotHighlight2;

	public UI_InventorySlot[] m_objEquipSlot;
	public UI_InventorySlot[] m_objInvenSlot;

	#endregion

	public const int nINVEN_MAX = 15;
	
	ST_EquipInfo	m_refEquipInfo = null;
	LinkedList<eID>	m_refList = null;

	public void SetData(ST_EquipInfo a_stEquipInfo, LinkedList<eID> a_liItems)
	{
		m_refEquipInfo = a_stEquipInfo;
		m_refList = a_liItems;
	}

	public void SetVisible(bool a_bVisible)
	{
		if( gameObject.activeSelf != a_bVisible )
		{
			gameObject.SetActive(a_bVisible);
		}

		if( a_bVisible == true )
		{
			Refresh();
		}
	}

	void Refresh()
	{
		for( int i=0; i<(int)eEquipPos.Max; ++i )
		{
			eID eID = m_refEquipInfo.GetID(i);
		
			if( eID != eID.None )
			{
				m_objEquipSlot[i].SetData(eID.GetItemData());
			}
		}
		
		var node = m_refList.GetEnumerator();

		for( int i=0 ; i<m_objInvenSlot.Length; ++i )
		{
			if( i < m_refList.Count )
			{
				node.MoveNext();

				eID eID = node.Current;

				if( eID != eID.None )
				{
					m_objInvenSlot[i].SetData(eID.GetItemData());
				}
			}
			else
			{
				m_objInvenSlot[i].SetData(null);
			}
		}
		
		SwapSlotHightlight();
	}

	public void SwapSlotHightlight()
	{
		eSwap eTemp =  m_refEquipInfo.eNowSwap;

		m_sprSlotHighlight1.color = (eTemp == eSwap.First) ? Color.black : Color.gray;
		m_sprSlotHighlight2.color = (eTemp == eSwap.First) ? Color.gray : Color.black;
	}

	// 메세지 처리기
	public void OnClickCloseButton()
	{
		SetVisible(false);
	}
}
