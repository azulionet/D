using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public partial class BasicMonster : Character, IFixedUpdate
{
	// 판단
	bool IsInDetectRange()
	{
		return Define.IsInRange(m_refPlayerTF.localPosition, transform.localPosition, m_refData.fDetectRange);
	}

	bool IsInAttackRange()
	{
		return Define.IsInRange(m_refPlayerTF.localPosition, transform.localPosition, m_refData.fAttackRange);
	}

	// 행동
	void MoveToHero(float a_fDelta)
	{
		if( m_fpMoveAction != null )
		{
			m_fpMoveAction(a_fDelta);
		}
	}

	void Attack(float a_fDelta)
	{
		if( m_fpAttackAction != null )
		{
			m_fpAttackAction(a_fDelta);
		}
	}
}
