using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NPCText : MonoBehaviour
{
	#region INSPECTOR

	public Text m_lbName;
	public Text m_lbText;
		
	#endregion
	
	List<NPCTextData> m_liText = null;

	int m_nCut = 0;

	public void SetData(string a_strName, int a_nNPCTextID)
	{
		m_lbName.text = a_strName;
		m_liText = a_nNPCTextID.GetNPCTextData();

		m_nCut = 0;

		Refresh();
	}

	void Refresh()
	{
		m_lbText.text = m_liText[m_nCut].strText;
	}

	public void SetVisible(bool a_bVisible)
	{
		if( gameObject.activeSelf != a_bVisible )
		{
			gameObject.SetActive(a_bVisible);
		}
	}

	// 메세지 처리기
	public void OnClickBg()
	{
		++m_nCut;

		if( m_nCut >= m_liText.Count) { gameObject.SetActive(false); return; }

		Refresh();
	}
}
