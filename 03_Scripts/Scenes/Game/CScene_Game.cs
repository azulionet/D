using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScene_Game : CScene
{
	#region INSPECTOR

	public DungeonMap m_objMap; // 많이 보는 UI라 씬이 물고있음

	#endregion

	protected new void Awake()
	{
		base.Awake();
	}

	private void Start()
	{
		DungeonMng.Ins.EnterDungeon(m_objRoomRoot, m_objCharacterRoot);
		GameMng.Ins.EnterDungeon();
		BulletMng.Ins.EnterDungeon(m_objBulletRoot);

		m_objMap.transform.parent = Camera.main.transform;
		m_objMap.transform.localPosition = new Vector3(0, 0);
		m_objMap.SetData(DungeonMng.Ins.m_stDungeon);
	}

	override public void CleanUp()
	{

	}

	public override void CloseAllPopup()
	{
		m_objMap.SetVisible(false);
	}

	private void FixedUpdate()
	{
		if( Input.GetButtonDown("Fire3") == true ) // 왼쪽 시프트
		{
			bool bVisible = m_objMap.bIsVisible;
			bVisible = !bVisible;

			m_objMap.SetVisible(bVisible);
		}
		else if( Input.GetKeyDown(KeyCode.V) == true )
		{
			CSceneMng.Ins.ToggleInvenVisible();
		}
		else if (Input.GetKeyDown(KeyCode.B) == true)
		{
			CSceneMng.Ins.ChangeScene(Global_Define.eScene.Town);
		}


		float fDeltaTime = Time.deltaTime;

		DungeonMng.Ins.DoFixedUpdate(fDeltaTime);
		BulletMng.Ins.DoUpdate(fDeltaTime);
	}
}
