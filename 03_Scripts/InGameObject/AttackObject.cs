using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class AttackObject : MonoBehaviour, IAttackInfo
{
	private IAttackInfo m_refOwner;

	public void SetOwner(IAttackInfo a_refOwner)
	{
		if( a_refOwner == null )
		{
			Define.LogError("arg error");
		}

		m_refOwner = a_refOwner;
	}

	public bool GetDir()
	{
		return m_refOwner.GetDir();
	}

	public ST_AttackInfo AttackInfo()
	{
		m_refOwner.AttackInfo().m_bRight = GetDir();
		return m_refOwner.AttackInfo();
	}
}
