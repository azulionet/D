using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class RoomInfo : MonoBehaviour
{
	// 네 방향
	const int nDIR_COUNT = 4;

	#region INSPECTOR
	
	public GameObject m_RootEtc;
	public GameObject m_objRoot_Tile;
	public GameObject m_objRoot_MonsterSpawn;
	public GameObject m_objRoot_FunctionWall;

	public GameObject[] m_arrSpawnRoot = new GameObject[nDIR_COUNT]; // 네 방향 스폰위치
	public GameObject[] m_arrEventWallRoot = new GameObject[nDIR_COUNT]; // 네 방향 이벤트 벽 루트
	public GameObject[] m_arrBlockWallRoot = new GameObject[nDIR_COUNT]; // 네 방향 막혀있는 방일 때 막는 벽 루트
	public GameObject[] m_arrTriggerRoot = new GameObject[nDIR_COUNT];  // 네 방향 트리거 루트

	public BoxCollider2D[] m_arrTrigger = new BoxCollider2D[nDIR_COUNT]; // 네 방향 트리거의 콜라이더

	public List<FunctionWall> m_liWall;
	public List<MonsterSpawn> m_objMosnterPos;

	#endregion
	
	ST_Room m_stData = null;

	private void Awake()
	{
		if( m_objRoot_Tile == null )			{ m_objRoot_Tile = m_RootEtc; }
		if( m_objRoot_MonsterSpawn == null )	{ m_objRoot_MonsterSpawn = m_RootEtc; }
		if( m_objRoot_FunctionWall == null )	{ m_objRoot_FunctionWall = m_RootEtc; }

		for( int i=0; i<nDIR_COUNT; ++i )
		{
			if( m_arrSpawnRoot[i] == null )		{ m_arrSpawnRoot[i] = m_RootEtc; }
			if( m_arrEventWallRoot[i] == null )	{ m_arrEventWallRoot[i] = m_RootEtc; }
			if( m_arrBlockWallRoot[i] == null )	{ m_arrBlockWallRoot[i] = m_RootEtc; }
			if( m_arrTriggerRoot[i] == null )	{ m_arrTriggerRoot[i] = m_RootEtc; }
		}

		gameObject.transform.SetTag_AllChildren("Wall");
	}

	public void SetVisible(bool a_bVisible)
	{
		if(gameObject.activeSelf != a_bVisible)
		{
			gameObject.SetActive(a_bVisible);
		}

		if( a_bVisible == true )
		{
			Refresh();
		}
	}

	public void SetData(ST_Room a_stData)
	{
		m_stData = a_stData;
		Refresh();
	}

	public void OnDrawGizmos() // 플레이어 스폰위치 씬뷰에서 보이기
	{
		Gizmos.color = Color.blue;

		for( int i=0; i< m_arrSpawnRoot.Length; ++i )
		{
			Gizmos.DrawCube(m_arrSpawnRoot[i].transform.position, new Vector3(0.1f,0.1f,0.1f));
		}

		Gizmos.color = Color.red;

		for (int i = 0; i < m_objMosnterPos.Count; ++i)
		{
			Gizmos.DrawCube(m_objMosnterPos[i].transform.position, new Vector3(0.05f, 0.05f, 0.05f));
		}
	}

	public void Refresh()
	{
		int nVal = (int)eDir.Left;
		eDir eCheck = eDir.None;

		for (int i = 0; i < nDIR_COUNT; ++i)
		{
			eCheck = (eDir)nVal;

			int nIndex = eCheck.ToIndex();

			// 이벤트 벽 : 특정 트리거 발동시 막아두는 벽
			m_arrEventWallRoot[nIndex].SetActive(false);
			
			// 막는 벽

			bool bShow = !m_stData.eOpenDir.AndOperation(eCheck);
			m_arrBlockWallRoot[nIndex].SetActive(bShow);
			m_arrTriggerRoot[nIndex].SetActive(!bShow);

			nVal <<= 1;
		}
	}
}
