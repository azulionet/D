using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Global_Define;

public class UI_HpBar : MonoBehaviour, IFixedUpdate
{
	#region INSEPCTOR

	public GameObject	m_objBar;
	public Image		m_imgHP;

	#endregion

	Character m_refTarget = null;

	public void SetTarget(Character a_refTarget)
	{
		m_refTarget = a_refTarget;
		
		if( m_refTarget == null )
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
			Refresh();
		}
	}

	void Refresh()
	{
		if( m_refTarget != null )
		{
			m_imgHP.fillAmount = m_refTarget.fHPRate;
		}
	}

	public void DoFixedUpdate(float a_fDelta)
	{
		var vcPos = Camera.main.WorldToScreenPoint(m_refTarget.gameObject.transform.position);

		gameObject.transform.position = vcPos;

		Refresh();
	}
}
