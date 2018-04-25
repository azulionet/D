using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class FunctionWall : MonoBehaviour, IFixedUpdate
{
	#region INSPECTOR

	public GameObject		m_objWall;
	public Rigidbody2D		m_rbWall;
	public BoxCollider2D	m_colWall;

	#endregion

	public void DoFixedUpdate(float a_fDeltaTime)
	{
		var pY = GameMng.Ins.m_refPlayer.transform.localPosition.y;

		m_colWall.enabled = (pY > transform.localPosition.y);
	}
}
