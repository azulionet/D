using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public partial class Player : Character
{
    #region INSPECTOR

    public CircleCollider2D m_colliderGround;

	public GameObject		m_objCenter;
	public GameObject		m_objHand;
	public BoxCollider2D	m_colliderAtk;
	
	public GameObject		m_objWeapon;
	public SpriteRenderer	m_ImgWeapon;

	public Animator			m_objAnim;

	#endregion

	protected bool		m_bJumping;
	protected bool		m_bGrounded;

	public Transform	groundCheck;
	public LayerMask	groundLayers;

	public float		fHandRoundRadius = 0.001f;
	public float groundCheckRadius = 0.1f;
	public float		fDashLength = 10.0f;
	
	protected System.Action m_fpFixedUpdate = null;
	protected System.Action<Collider2D> m_fpOnTriggerEnter2D = null;
	protected System.Action<Collider2D> m_fpOnTriggerExit2D = null;
	protected System.Action<Collision2D> m_fpOnCollisionEnter2D = null;

	// 1. 게임매니져가 씬을 받으면 기본 스탯 세팅
	// 2. 마을의 플레이어는 그걸로 세팅
	// 3. 인겜은 플레이어 상속해서 인겜으로 해서 ㄱㄱ
	
	// 대쉬 관련
	
	protected Vector2		m_vcDest;
	public Vector2			m_vcDir;
	public float			m_fDashSpeed;

	// 어택 애니매이션
	public int				m_nAtkStep = 0;

	// 현재 장비
	ST_EquipInfo			m_refEquip;

	virtual public System.Action fpUpdate
	{
		get { return FixedUpdate_InTown; }
	}
	
	public void SetStat(StatData a_refBaseStat, StatData a_refWaeponStat, ST_EquipInfo a_refEquipInfo)
	{
		m_stStat.Clear();

		m_stStat.AddStat(a_refBaseStat);
		m_stStat.AddStat(a_refWaeponStat);

		m_refEquip = a_refEquipInfo;

		SetAttackRange();
		MakeOwnHitInfo();
	}

	public void SetAttackRange()
	{
		var data = m_refEquip.eMainHand.GetItemData();
		var atals = CSceneMng.Ins.iAtlas;

		m_ImgWeapon.sprite = atals.GetSprite(eAtlas.Weapon01, data.strSpriteName);

		AttackData tb = m_refEquip.eMainHand.GetAttackData();

		var rt = tb.stRt;
		m_colliderAtk.size = rt.size;

		// m_colliderAtk.offset = rt.position;
	}

	public override void MakeOwnHitInfo()
	{
		m_stHit.m_nDamage = 10;
		m_objAtk.SetOwner(this);
	}

	void Awake()
	{
		GameMng.Ins.SetPlayer(this);

		// Camera.main.gameObject.transform.parent = this.gameObject.transform;
	}

	void Start()
	{
		if (CSceneMng.Ins.eNowScene == eScene.Town)
		{
			m_fpFixedUpdate = fpUpdate;
			m_fpOnTriggerEnter2D = OnTriggerEnter2D_InTown;
			m_fpOnTriggerExit2D = OnTriggerExit2D_InTown;
			m_fpOnCollisionEnter2D = OnCollisionEnter2D_InTown;
		}	
	}

	public void AttackAniEnd()
	{
		m_objAnim.enabled = false;
	}

	public void FixedUpdate()
	{
		// 손위치, 무기위치
		var centerPos = m_objCenter.transform.position;
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var vcR = mousePos - centerPos;

		// 플립여부
		m_bRight = vcR.x > 0;

		if( m_objAnim.enabled == false )
		{
			vcR.Normalize();

			float fVal = Mathf.Atan(vcR.y / vcR.x);
			fVal *= Mathf.Rad2Deg;

			if( vcR.x < 0 )
			{
				fVal = -fVal;
			}

			m_objHand.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, fVal));

			vcR *= fHandRoundRadius;

			m_objHand.transform.position = centerPos;
			m_objHand.transform.position += vcR;
			
			gameObject.transform.localScale = (m_bRight == true) ? Vector3.one : new Vector3(-1, 1, 1);
		}

		if( Input.GetButtonDown("Fire1") == true )
		{
			if( m_objAnim.enabled == false )
			{
				m_objAnim.enabled = true;
				m_objAnim.CrossFade("charAtk1", 0.1f);

				// 공속에 따라 변경 클수록 빨라짐
				// m_objAnim.speed = 2;
			}
		}

		m_fpFixedUpdate();
	}

	public void FixedUpdate_Dash()
	{
		transform.position += new Vector3(m_vcDir.x * m_fDashSpeed, m_vcDir.y * m_fDashSpeed);

		float fX = transform.position.x;
		bool bDest = false;

		if(m_vcDir.x < 0 )
		{
			if(fX < m_vcDest.x )
			{
				bDest = true;
			}
		}
		else
		{
			if (fX > m_vcDest.x)
			{
				bDest = true;
			}
		}

		if( bDest == true )
		{
			ResetDash();	
		}
	}

	virtual public void ResetDash()
	{
		m_colliderAtk.enabled = false;
		m_fpFixedUpdate = fpUpdate;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		m_fpOnTriggerEnter2D(collision);
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		m_fpOnCollisionEnter2D(collision);
	}

	public override void GetHit(ST_AttackInfo a_refDamage)
	{

// 		float fDamage = 0.0f;
// 
// 		switch (a_cDamage.eSource)
// 		{
// 			case eDamageSource.Hero:
// 			case eDamageSource.HeroSkill:
// 			{
// 				fDamage = a_cDamage.nDamage; // 자해 스킬류
// 
// 				if (a_cDamage.eSource == eDamageSource.HeroSkill)
// 				{
// 					if (a_cDamage.m_refSkill.bIsDamageSkill == false)
// 					{
// 						fDamage = -1.0f;
// 					}
// 				}
// 			}
// 			break;
// 			case eDamageSource.Enemy:
// 			{
// 				GameManager.Instance.AddHeroHitCount();
// 				fDamage = InGameUtil.CalculateEnemyAttackDamageToHero(this, a_cDamage);
// 				UseGetHitTriggerSkill();
// 			}
// 			break;
// 			case eDamageSource.EnemySkill:
// 			{
// 				GameManager.Instance.AddHeroSkillHitCount();
// 
// 				if (a_cDamage.m_refSkill.bIsDamageSkill)
// 				{
// 					fDamage = InGameUtil.CalculateEnemyAttackDamageToHero(this, a_cDamage);
// 				}
// 				else
// 				{
// 					fDamage = -1.0f;
// 				}
// 			}
// 			break;
// 			case eDamageSource.BgObject_Skill:
// 			{
// 				GameManager.Instance.AddHeroObjectHitCount();
// 
// 				if (a_cDamage.m_refSkill.bIsDamageSkill)
// 				{
// 					fDamage = InGameUtil.CalculateEnemyAttackDamageToHero(this, a_cDamage);
// 				}
// 				else
// 				{
// 					fDamage = -1.0f;
// 				}
// 			}
// 			break;
// 		}
// 
// 		// TimeScaleManager.Instance.TimeSlowKeepTimeReset();
// 		// TimeScaleManager.Instance.SetCameraTilt(0.2f);
// 
// 		// 16.09.30 // 회피 사라짐
// 		// if( Util.CheckProbability( PlayerInfo.Instance.m_Stat.dodge)) { return false; }
// 
// 		if (a_refDamage.m_bHasStatusEffect == true)
// 		{
// 			if (a_cDamage.m_refSkill != null)
// 			{
// 				this.AddStatusEffect(a_cDamage.m_refSkill);
// 			}
// 		}
// 
// #if UNITY_EDITOR
// 
// 		if (GameManager.Instance.m_cGameMode.bIndestructable == true)
// 		{
// 			fDamage = 0.0f;
// 		}
// 
// #endif
// 
// 		if (this.m_nLife > 0 && fDamage >= 0.0f) // 데미지가 없는 스킬도 존재할 수 있음
// 		{
// 			SoundManager.Instance.SoundPlay("VoiceHit");
// 
// 			UIManager.Instance.CreateUIDamage(this, fDamage, a_cDamage.eSource);
// 			SetHeroColor(false);
// 			GameManager.Instance.m_nComboCount = 0;
// 
// 			Vector3 hitPos = m_HitPosition.position;
// 			hitPos.x += Random.Range(-0.5f, 0.5f);
// 			hitPos.y += Random.Range(-1.5f, 0.5f);
// 
// 			EffectManager.Instance.CreateEffect("FX_Pc_hit", hitPos);
// 			PlayHitSound();
// 
// 			if (fDamage > 0)
// 			{
// 				if (m_nLife > 0)
// 				{
// 					SetHitAction();
// 				}
// 
// 				this.m_nLife -= fDamage;
// 			}
// 		}
// 
// 		if (this.m_nLife > 0)
// 		{
// 			return false;
// 		}
// 		else
// 		{
// 			SetState(eCharacterState.Death);
// 			return true;
// 		}
	}
}
