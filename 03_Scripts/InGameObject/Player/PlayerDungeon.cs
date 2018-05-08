using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

using Global_Define;


public class PlayerDungeon : Player
{
	ST_AttackInfo m_stDash = new ST_AttackInfo();
	ST_AttackInfo m_refNowHitInfo = null;

	override public ST_AttackInfo stHitInfo
	{
		get { return m_refNowHitInfo; }
	}

	override public System.Action fpUpdate
	{
		get { return FixedUpdate_InDungeon; }
	}

	public override void MakeOwnHitInfo()
	{
		m_stHit.m_nDamage = 10;
		m_stDash.m_nDamage = 8;

		m_refNowHitInfo = m_stHit;
		m_objAtk.SetOwner(this);
	}

	void Start()
	{
		// if (CSceneMng.Ins.eNowScene == eScene.Dungeon)
		{
			m_fpFixedUpdate = fpUpdate;
			m_fpOnTriggerEnter2D = OnTriggerEnter2D_InDungeon;
			m_fpOnTriggerExit2D = OnTriggerExit2D_InDungeon;
			m_fpOnCollisionEnter2D = OnCollisionEnter2D_InDungeon;
		}
	}

	public void FixedUpdate_InDungeon()
	{
		// 무적시간 관련
		Invincible();

		if (this.m_Rb.isKinematic == true) { return; }

		if (Input.GetAxis("Vertical") < 0 && Input.GetButton("Jump"))
		{
			var pos = transform.localPosition;
			pos.y -= 1f;
			transform.localPosition = pos;
			// 
			// 			m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
			// 			m_rb.AddForce(new Vector2(0, -10.0f));
		}
		else if (Input.GetButton("Jump") == true)
		{
			

			// if (m_stStat.nNowJumpCount < m_stStat.nMaxJump)
			{
				m_Rb.velocity = new Vector2(m_Rb.velocity.x, 0);
				m_Rb.AddForce(new Vector2(0, m_stStat.fJumpForce));

				// 				var pos = transform.localPosition;
				// 				pos.y += 15.0f;
				// 				transform.localPosition = pos;
			}

			// m_rb.isKinematic = true;
		}
		else
		{
			// m_rb.isKinematic = false;
		}

		m_bGrounded = IsGroundCheck();

		float f = Input.GetAxis("Horizontal");
		f /= 2;

		m_Rb.velocity = new Vector2(f * m_stStat.fMove, m_Rb.velocity.y);

		if( Input.GetButtonDown("Fire2") == true )
		{
			StartTrailImage();

			var centerPos = transform.position;
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			m_Rb.isKinematic = true;

			m_vcDest = mousePos - centerPos;
			m_vcDest.Normalize();
			m_vcDir = m_vcDest;

			m_vcDest *= fDashLength;
			m_vcDest += new Vector2(centerPos.x, centerPos.y);

			m_colliderAtk.enabled = true;
			m_refNowHitInfo = m_stDash;
			m_fpFixedUpdate = FixedUpdate_Dash;
			m_fpOnTriggerEnter2D = OnTriggerEnter2D_InDungeon_Dash;
			m_fpOnCollisionEnter2D = OnCollisionEnter2D_InDungeon_Dash;
		}
	}
	
	override public void ResetDash()
	{
		m_refNowHitInfo = m_stHit;
		m_colliderAtk.enabled = false;
		m_Rb.isKinematic = false;

		m_fpFixedUpdate = fpUpdate;
		m_fpOnTriggerEnter2D = OnTriggerEnter2D_InDungeon;
		m_fpOnCollisionEnter2D = OnCollisionEnter2D_InDungeon;
	}

	public void OnTriggerEnter2D_InDungeon_Dash(Collider2D collision)
	{
		if( collision.CompareTag("Ground") == true ||
			collision.CompareTag("Trigger") == true )
		{
			ResetDash();
		}

		TriggerObject obj = collision.gameObject.GetComponent<TriggerObject>();

		if( obj == null )
		{
			return;
		}

		obj.Trigger(this);
	}

	public void OnCollisionEnter2D_InDungeon_Dash(Collision2D collision)
	{
		if( collision.gameObject.CompareTag("Ground") == true ||
			collision.gameObject.CompareTag("Trigger") == true )
		{
			ResetDash();
		}
	}

	private void OnTriggerEnter2D_InDungeon(Collider2D collision)
	{
		Trigger(collision);
	}

	private void OnTriggerExit2D_InDungeon(Collider2D collision)
	{
		TriggerExit(collision);
	}

	public void OnCollisionEnter2D_InDungeon(Collision2D collision)
	{
		if (this.m_Rb.isKinematic == true) { return; }
	}


	// 잔상
	List<GameObject> trailParts = new List<GameObject>();

	void StartTrailImage()
	{
		StartCoroutine(TrailImage());
	}

	IEnumerator TrailImage()
	{
		for( int i=0; i<13; ++i )
		{
			SpawnTrailPart();
			yield return new WaitForSeconds(0.01f);
		}
	}

	void SpawnTrailPart()
	{
		GameObject trailPart = new GameObject();
		SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
		trailPartRenderer.sprite = m_Img.sprite;
		trailPart.transform.position = m_Img.transform.position;
		trailParts.Add(trailPart);

		StartCoroutine(FadeTrailPart(trailPartRenderer));
		StartCoroutine(DestroyTrailPart(trailPart, 0.4f));
	}

	IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
	{
		Color color = trailPartRenderer.color;
		color.a -= 0.5f; // replace 0.5f with needed alpha decrement
		trailPartRenderer.color = color;

		yield return new WaitForEndOfFrame();
	}

	IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
	{
		yield return new WaitForSeconds(delay);

		trailParts.Remove(trailPart);
		Destroy(trailPart);
	}
}
