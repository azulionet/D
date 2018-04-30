using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
	private void Update_InTown()
	{
		
	}

	private void FixedUpdate_InTown()
	{
		if (Input.GetAxis("Vertical") < 0 && Input.GetButton("Jump"))
		{
			var pos = transform.localPosition;
			pos.y -= 20;
			transform.localPosition = pos;
			// 
			// 			m_rb.velocity = new Vector2(m_rb.velocity.x, 0);
			// 			m_rb.AddForce(new Vector2(0, -10.0f));


		}
		else if (Input.GetButtonDown("Jump") == true)
		{
			if( m_stStat.nNowJumpCount < m_stStat.nMaxJump )
			{
				m_Rb.velocity = new Vector2(m_Rb.velocity.x, 0);
				m_Rb.AddForce(new Vector2(0, m_stStat.fJumpForce));

				// 				var pos = transform.localPosition;
				// 				pos.y += 15.0f;
				// 				transform.localPosition = pos;

				// ++m_refStat.nNowJumpCount;
			}

			// m_rb.isKinematic = true;
		}
		else
		{
			// m_rb.isKinematic = false;
		}
		
		m_bGrounded = IsGroundCheck();

		float f = Input.GetAxis("Horizontal");
		m_Rb.velocity = new Vector2(f * m_stStat.fMove, m_Rb.velocity.y);
	}

	protected bool IsGroundCheck()
	{
		bool bGround = false;

		Collider2D[] colArr1 = null;
		Collider2D[] colArr2 = null;

		int nSize = Physics2D.OverlapPointNonAlloc(groundCheck[0].position, colArr1, groundLayers);

		for (int i = 0; i < nSize; ++i)
		{
			if( colArr1[i].CompareTag("ground") == true )
			{
				bGround = true;
				break;
			}
		}

		if( bGround == false )
		{
			nSize = Physics2D.OverlapPointNonAlloc(groundCheck[1].position, colArr2, groundLayers);

			for (int i = 0; i < nSize; ++i)
			{
				if (colArr1[i].CompareTag("ground") == true)
				{
					bGround = true;
					break;
				}
			}
		}

		return bGround;
	}

	private void OnTriggerEnter2D_InTown(Collider2D collision)
	{
		Trigger(collision);
	}

	private void OnTriggerExit2D_InTown(Collider2D collision)
	{
		TriggerExit(collision);
	}

	protected void Trigger(Collider2D collision)
	{
		TriggerObject obj = collision.gameObject.GetComponent<TriggerObject>();

		if (obj == null)
		{
			return;
		}

		obj.Trigger(this);
	}

	protected void TriggerExit(Collider2D collision)
	{
		TriggerObject obj = collision.gameObject.GetComponent<TriggerObject>();

		if (obj == null)
		{
			return;
		}

		obj.TriggerExit(this);
	}

	private void OnCollisionEnter2D_InTown(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("NPCBox") == true)
		{
			Brick a_refBrick = collision.gameObject.GetComponent<Brick>();

			if (a_refBrick == null)
			{
				Debug.LogError("logic error - check Collider Tag");
				return;
			}

			a_refBrick.Trigger(this);
		}
	}



	
}
