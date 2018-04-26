using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class Bullet : InGameObject, IUpdate, IAttackInfo
{
	#region INSPECTOR

	public AttackObject m_objAtk;

	#endregion

	BulletData		m_refData = null;
	ST_AttackInfo	m_refAtkInfo = null;

	Vector2			m_vcDir;

	float			m_fNowSpeed;
	float			m_fNowLife;

	// IAttackInfo
	public bool GetDir()
	{
		return m_vcDir.x > 0;
	}

	public ST_AttackInfo AttackInfo()
	{
		return m_refAtkInfo;
	}
	
	// IUpdate
	public void DoUpdate(float a_fDeltaTime)
	{
		var pos = transform.localPosition;
		var move = m_fNowSpeed * m_vcDir * a_fDeltaTime;

		pos.x += move.x;
		pos.y += move.y;

		transform.localPosition = pos;

		m_fNowLife -= a_fDeltaTime;
	}

	public bool bIsDie { get { return (m_fNowLife <= 0); } }

	public void Reset()
	{
		m_refData = null;
		m_refAtkInfo = null;

		m_fNowSpeed = 0;
		m_fNowLife = 0;

		gameObject.SetActive(false);
	}

	public void SetData(BulletData a_refData, ST_AttackInfo a_refAtkInfo)
	{
		m_refData = a_refData;
		m_refAtkInfo = a_refAtkInfo;

		m_fNowSpeed = a_refData.fSpeed;
		m_fNowLife = a_refData.fLifeTime;

		m_objAtk.SetOwner(this);

		Refresh();
	}

	void Refresh()
	{
		if (m_refData != null)
		{
			// m_Img.spriteName = m_refData.strSpriteName;
			m_Col.size = new Vector2(m_refData.fWidth, m_refData.fHeight);

			gameObject.SetActive(true);
		}
		else
		{
			Reset();
		}
	}

	public void SetValue(Vector3 a_vcPos, Vector3 a_vcDir)
	{
		transform.localPosition = a_vcPos;

		m_vcDir.x = a_vcDir.x;
		m_vcDir.y = a_vcDir.y;
	}
}
