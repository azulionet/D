using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class ScenePortal : TriggerObject
{
	#region INSPECTOR

	public eScene m_eScene;

	#endregion

	private void Awake()
	{
		if (m_eScene == eScene.None)
		{
			Define.LogError("logic error - inspector value setting");
		}
	}

	public override void Trigger(Character a_obj)
	{
		CSceneMng.Ins.ChangeScene(m_eScene);
	}
}
