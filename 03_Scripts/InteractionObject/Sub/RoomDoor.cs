using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class RoomDoor : TriggerObject
{
	#region INSPECTOR

	public eDir m_eDir;

	#endregion

	public override void Trigger(Character a_obj)
	{
		DungeonMng.Ins.ChangeRoom(m_eDir);
	}
}
