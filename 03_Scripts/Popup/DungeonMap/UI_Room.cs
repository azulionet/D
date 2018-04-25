using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class UI_Room : MonoBehaviour
{
	#region INSPECTOR

	public SpriteRenderer m_sprBg;

	public SpriteRenderer m_sprRight;
	public SpriteRenderer m_sprUp;

	public GameObject m_objPortal;
	public GameObject m_objMe;
	public GameObject m_objStart;
	public GameObject m_objLast;
	public GameObject m_objWaepon;
	public GameObject m_objDining;

	#endregion

	ST_Room m_refData = null;
	int m_nOffsetX = 0;
	int m_nOffsetY = 0;

	public void SetData(ST_Room a_refData, int a_nOffsetX, int a_nOffsetY)
	{
		m_refData = a_refData;

		m_nOffsetX = a_nOffsetX;
		m_nOffsetY = a_nOffsetY;

		Refresh();
	}

	void Refresh()
	{
		m_objPortal.SetActive(m_refData.bHasPortal);
		
		m_objStart.SetActive(m_refData.eState == eRoomState.First);
		m_objLast.SetActive(m_refData.eState == eRoomState.Last);
		m_objWaepon.SetActive(m_refData.eExistNPC == eID.N_DungeonWaepon);
		m_objDining.SetActive(m_refData.eExistNPC == eID.N_DungeonDining);

		SetMyMark();
		
		m_sprRight.gameObject.SetActive(((m_refData.eOpenDir & eDir.Right) == eDir.Right) );
		m_sprUp.gameObject.SetActive((m_refData.eOpenDir & eDir.Top) == eDir.Top);

		transform.localPosition = new Vector3(m_refData.nX * 100 + m_nOffsetX, m_refData.nY * 100 + m_nOffsetY);
	}

	public void SetMyMark()
	{
		if( m_refData == null ) { return; }
		int nX = DungeonMng.Ins.m_nMapCoordX;
		int nY = DungeonMng.Ins.m_nMapCoordY;

		bool bShow = ( (m_refData.nX == nX) && (m_refData.nY == nY) );

		m_objMe.SetActive(bShow);
	}

	// 메세지 처리기
	public void OnClickPortal()
	{
		DungeonMng.Ins.ChangeRoom_withPortal(m_refData.nX.GetCoordKey(m_refData.nY));
	}
}
