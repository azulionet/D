using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public partial class BasicMonster : Character, IFixedUpdate
{
	#region INSPECTOR

	#endregion

	protected MonsterData m_refData = null;

	protected Player m_refPlayer = null;
	protected Transform m_refPlayerTF = null;

	protected System.Action<float> m_fpUpdateAction = null;
	protected System.Action<float> m_fpMoveAction = null;
	protected System.Action<float> m_fpAttackAction = null;

	public Room m_refParent = null;

	public eID m_testID;

	[HideInInspector]
	public int m_nIndex = -1;

	public void SetData(eID a_eID, int a_nIndex)
	{
		m_refPlayer = GameMng.Ins.m_refPlayer;
		m_refPlayerTF = m_refPlayer.gameObject.transform;

		m_nIndex = a_nIndex;
		m_refData = a_eID.GetMonsterData();
		m_stStat.CopyStat(a_eID.GetStatData());

		m_testID = a_eID;

		switch (m_refData.eLand)
		{
			case eLandType.Land:
				{

				}
				break;
			case eLandType.Flying:
			case eLandType.Ghost:
				{
					m_Rb.isKinematic = true;
				}
				break;
		}

		MakeOwnHitInfo();
		SetAction();
	}

	virtual public void SetAction()
	{
		m_fpUpdateAction =
		(a_fDelta) =>
		{
			bool bDetect = Define.IsInRange(m_refPlayerTF.localPosition, transform.localPosition, m_refData.fDetectRange);

			if (bDetect == true) // 감지 범위 안
			{
				bool bInAtkRange = Define.IsInRange(m_refPlayerTF.localPosition, transform.localPosition, m_refData.fAttackRange);

				if (bInAtkRange == false) // 사정권 밖
				{
					MoveToHero(a_fDelta);
				}
				else // 사정권 안
				{
					if (m_stStat.fAtkTerm <= 0)
					{
						Attack(a_fDelta);
					}
				}

				m_stStat.fAtkTerm -= a_fDelta;
			}
		};
		
		// 이동
		switch (m_refData.eLand)
		{
			case eLandType.Flying:
			case eLandType.Ghost:
			{
				m_fpMoveAction = (a_fDelta) =>
				{
					float fMove = m_stStat.fMove / a_fDelta;
					var vcHeroPos = m_refPlayerTF.localPosition;
					var vcTo = vcHeroPos - transform.localPosition;

					vcTo.Normalize();
					vcTo *= fMove;
					vcTo += transform.localPosition;

					transform.localPosition = vcTo;
				};
			}
			break;

			case eLandType.Land:
			{
				m_fpMoveAction = (a_fDelta) => {
					float fMove = m_stStat.fMove;
					var vcHeroPos = m_refPlayerTF.localPosition;

					if( vcHeroPos.x < gameObject.transform.localPosition.x )
					{
						fMove *= -1;
					}
					
					m_Rb.velocity = new Vector2(fMove, m_Rb.velocity.y);
					// m_rb.AddForce(new Vector2(fMove, 0));

					// var vcPos = transform.localPosition;
					// vcPos.x += fMove;
					// transform.localPosition = vcPos;
				};
			}
			break;
		}

		// 공격 세팅
// 		var atkData = m_refData.eAtkID.GetData();
// 
// 		if( atkData.bIsBullet == true )
// 		{
// 			m_fpAttackAction = (fDeltaTime) => {
// 
// 			};
// 		}
// 		else
// 		{
// 			m_fpAttackAction = (fDeltaTime) => {
// 
// 			};
// 		}
	}

	public override void MakeOwnHitInfo()
	{
		m_stHit.m_nDamage = 10;

		m_objAtk.SetOwner(this);
	}

	public void DoFixedUpdate(float a_fDelta)
	{
		// 플레이어를 바라보도록 세팅
		m_bRight = gameObject.transform.localPosition.x < m_refPlayerTF.localPosition.x;
		gameObject.transform.localScale = (m_bRight == true) ? Vector3.one : new Vector3(-1, 1, 1);

		if (m_fpUpdateAction != null)
		{
			m_fpUpdateAction(a_fDelta);
		}

		Invincible();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if( collision.gameObject.CompareTag("Waepon") )
		{
			AttackObject obj = collision.gameObject.GetComponent<AttackObject>();

			if( obj  == null )
			{
				Debug.LogError("logic error");
				return;
			}
			
			var atkInfo = obj.AttackInfo();

			if( atkInfo.m_bRight == m_bRight ) { return; }

			switch( atkInfo.eSource )
			{
				case eDamageSource.Player:
				case eDamageSource.PlayerSkill:
				{
					GetHit(atkInfo);
					
					Debug.LogError("어택드");

					if( bIsDie == true )
					{
						Debug.LogError("으앙 다이");
					}
				}
				break;
			}
		}
		else if( collision.gameObject.CompareTag("Bullet") )
		{
			AttackObject obj = collision.gameObject.GetComponent<AttackObject>();

			if (obj == null)
			{
				Debug.LogError("logic error");
				return;
			}

			var atkInfo = obj.AttackInfo();

			if (atkInfo.m_bRight == m_bRight) { return; }

			switch (atkInfo.eSource)
			{
				case eDamageSource.Player:
				case eDamageSource.PlayerSkill:
				{
					GetHit(atkInfo);

					Debug.LogError("어택드");

					if (bIsDie == true)
					{
						Die();
						Debug.LogError("으앙 다이");
					}
				}
				break;
			}
		}
	}

	UI_HpBar m_refHPBar = null;

	public override void GetHit(ST_AttackInfo a_refAttack)
	{
		Define.Attack_ToMonster(ref m_stStat, a_refAttack);

		m_fIndesTime = 0.1f;

		CSceneMng.Ins.CameraShake(0.4f);

		if(m_refHPBar == null )
		{
			m_refHPBar = UI_Hud.Ins.ActiveHPBar(this);
		}
	}

	void Die()
	{
		// 룸에 보고, 스폰 몬스터 데이터에 die true로 체크

		if( m_refHPBar != null )
		{
			UI_Hud.Ins.DeactiveHPBar(m_refHPBar);
		}
	}
}
