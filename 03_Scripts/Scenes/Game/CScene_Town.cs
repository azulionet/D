using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Town : CScene
{
	#region INSPECTOR

	public RoomInfo		m_RoomInfo;

	#endregion

	protected new void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
		DungeonMng.Ins.EnterTown(m_RoomInfo, m_objRoomRoot, m_objCharacterRoot);
		GameMng.Ins.EnterTown();
	}

	override public void CleanUp()
	{
		
	}
	
	// 플레이어, 씬만 Update()를 돌리도록 합시다
	private void FixedUpdate()
	{
		var fDeltaTime = Time.deltaTime;

		DungeonMng.Ins.DoFixedUpdate(fDeltaTime);
	}
}
