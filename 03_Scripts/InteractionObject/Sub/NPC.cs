using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Global_Define;

public class NPC : TriggerObject
{
	#region INSPECTOR
	
	public eID			m_eNPC = eID.___NPC___;
	
	#endregion

	Brick				m_objNPCBrick;
	NPCData				m_refTable;

	public void Awake()
	{
		if( m_eNPC == eID.None )
		{
			Debug.LogError("inspector error - set value in inspector");
		}

		m_refTable = m_eNPC.GetNPCData();

		m_objNPCBrick = gameObject.Instantiate_asChild("Game/Brick").GetComponent<Brick>();

		m_objNPCBrick.transform.parent = this.gameObject.transform.parent;
		m_objNPCBrick.transform.position = gameObject.transform.position + new Vector3(3,2);
		m_objNPCBrick.name = string.Format("NPCBrick_{0}", gameObject.name);

		m_objNPCBrick.SetInteraction(
			Brick.eState.Repeat,
			(a_obj) => {
				CSceneMng.Ins.textBox.SetData(m_refTable.strName, 1);
				CSceneMng.Ins.textBox.SetVisible(true);
			}
		);
	}

	public override void Trigger(Character a_obj)
	{
		if(m_objNPCBrick.gameObject.activeSelf == false )
		{
			m_objNPCBrick.gameObject.SetActive(true);
		}
	}

	public override void TriggerExit(Character a_obj)
	{
		if (m_objNPCBrick.gameObject.activeSelf == false)
		{
			m_objNPCBrick.gameObject.SetActive(false);
		}
	}
}
