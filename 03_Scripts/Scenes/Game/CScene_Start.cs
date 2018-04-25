using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using Global_Define;

public class CScene_Start : CScene
{
	#region INSPECTOR

	#endregion

	// 메세지 처리기
	public void OnClickStart()
	{
		CSceneMng.Ins.ChangeScene(Global_Define.eScene.Town);
	}
}
