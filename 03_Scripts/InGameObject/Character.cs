using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

abstract public class Character : InGameObject, IAttackInfo
{
	#region INSPECTOR

	public AttackObject m_objAtk = null;

	#endregion

	protected ST_GameStat   m_stStat = new ST_GameStat();
	protected bool          m_bRight;
	protected ST_AttackInfo	m_stHit = new ST_AttackInfo();
	
	abstract public void MakeOwnHitInfo(); // AttackObject, ST_AttackInfo 세팅
	abstract public void GetHit(ST_AttackInfo a_refInfo);

	virtual public ST_AttackInfo stHitInfo
	{
		get { return m_stHit; }
	}

	public bool bIsRight
	{
		get { return m_bRight; }
	}

	// IAttackInfo
	public bool GetDir()
	{
		return bIsRight;
	}

	public ST_AttackInfo AttackInfo()
	{
		return stHitInfo;
	}

	// 잠시간 데미지 안받게 하기 위함
	[HideInInspector]
	public float m_fIndesTime = -1.0f;
	const float fINDES_TIME = 0.2f;

	const float fTWINKLE_TIME = 0.01f;
	float m_fTwinkle = 0.0f;

	public bool bIsIndestructable	{ get { return (m_fIndesTime >= 0.0f); } }
	public bool bIsDie				{ get { return (m_stStat.nNowHP < 0); } }
	public float fHPRate			{ get { return (m_stStat.nNowHP / (float)m_stStat.nMaxHP); } }

// 	protected void OnHit()
// 	{
// 		if (bIsIndestructable == false)
// 		{
// 			m_fIndesTime = fINDES_TIME;
// 		}
// 	}

	protected void Invincible()
	{
		if (m_fIndesTime > 0.0f)
		{
			m_fIndesTime -= Time.deltaTime; // 현재 fixedupdate라서 사실 이렇게 하면 안되지만

			if (m_fTwinkle < fTWINKLE_TIME) // 껏다 켰다
			{
				m_fTwinkle += Time.deltaTime;
			}
			else
			{
				m_Img.enabled = !m_Img.enabled;
				m_fTwinkle = 0.0f;
			}

			if (m_fIndesTime <= 0.0f)
			{
				m_fIndesTime = -1.0f;
				m_Img.enabled = true;
				// 무적 끝
			}
		}
	}

	
}
