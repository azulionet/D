using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class BulletMonster : BasicMonster, IFixedUpdate
{
	#region INSPECTOR

	public eDir m_eDir;
	public eBullet m_eBulletID;
	public Vector3 vcDir = Vector3.right;

	#endregion

	public void Start()
	{
		switch (m_eDir)
		{
			case eDir.Left:
			{
				vcDir = Vector3.left;
			} break;
			case eDir.Top:
			{
				vcDir = Vector3.up;
			}
			break;
			case eDir.Right:
			{
				vcDir = Vector3.right;
			}
			break;
			case eDir.Bottom:
			{
				vcDir = Vector3.down;
			}
			break;
		}

		m_stStat.CopyStat(eID.M_BulletMonster.GetStatData());

		m_fpAttackAction = ( a_fDelta )=>
		{
			m_stStat.fAtkTerm -= a_fDelta;

			if ( m_stStat.fAtkTerm <= 0.0f )
			{
				m_stStat.fAtkTerm = m_stStat.fAtkSpd;

				BulletMng.Ins.AddShot(gameObject.layer, m_eBulletID, stHitInfo, transform.localPosition, vcDir);
			}
		};
	}

	public void DoFixedUpdate(float a_fDelta)
	{
		Attack(a_fDelta);
	}

	public override void GetHit(ST_AttackInfo a_refAttack) { }
}
