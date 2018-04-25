using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ricimi;
using Global_Define;


public class CSceneMng : MonoBehaviour
{
	#region SINGLETON

	public static bool destroyThis = false;

	static CSceneMng _instance = null;

	public static CSceneMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(CSceneMng)) as CSceneMng;
				if (_instance == null)
				{
					_instance = new GameObject("CSceneMng", typeof(CSceneMng)).GetComponent<CSceneMng>();

					Rand.CreatePool();
				}
			}

			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	#endregion

	CScene m_refScene = null;
	
	NPCText			m_objText = null;
	UI_Inventory	m_objInven = null;
	UI_ItemInfo		m_objItemInfo = null;

	public NPCText textBox
	{
		get
		{
			if (m_objText == null)
			{
				var newObj = m_refScene.m_objUIRoot.Instantiate_asChild("Popup/NPCText");
				m_objText = newObj.GetComponent<NPCText>();
			}

			return m_objText;
		}
	}

	public UI_Inventory inventory
	{
		get
		{
			if (m_objInven == null)
			{
				var newObj = m_refScene.m_objUIRoot.Instantiate_asChild("Popup/Inventory");
				m_objInven = newObj.GetComponent<UI_Inventory>();
				m_objInven.SetData(GameMng.Ins.m_stEquip, GameMng.Ins.m_liInvenItems);
			}

			return m_objInven;
		}
	}
	
	public eScene eNowScene
	{
		get { return m_refScene.m_eScene; }
	}

	public void ToggleInvenVisible()
	{
		if( m_objInven == null )
		{
			inventory.SetVisible(true);
		}
		else
		{
			bool bAct = m_objInven.gameObject.activeSelf;
			m_objInven.SetVisible(!bAct);
		}
	}

	public void ChangeScene(eScene a_eScene)
	{
		if( m_refScene == null ) { return; }

		m_refScene.CleanUp();
		m_refScene = null;

		DungeonMng.Ins.ChangeSceneCleanUp();
		GameMng.Ins.ChangeSceneCleanUp();
		BulletMng.Ins.ChangeSceneCleanUp();

		ResetPopup();

        System.GC.Collect();

		Transition.LoadLevel(a_eScene.ToDesc(), 0.5f, Color.black);
	}

	public void FadeIn(System.Action a_fpCallback)
	{
		m_refScene.CloseAllPopup();

		Transition.FadeAction(0.5f, Color.black, a_fpCallback);
	}

	public void SetScene(CScene a_refScene)
	{
		m_refScene = a_refScene;
	}

	UnityStandardAssets.Utility.SmoothFollow cam = null;
	public void CameraShake(float a_fDuration)
	{
		if( cam == null )
		{
			cam = Camera.main.gameObject.GetComponent<UnityStandardAssets.Utility.SmoothFollow>();
		}

		cam.Shake(a_fDuration);
	}

	public void ResetPopup()
	{
		// 삭제는 매우 비용이 쎕니다.// 씬 전환이 보통 삭제관련 넣기 가장 좋은 때 입니다. 그 때만 지웁니다.
		if ( m_objText != null ) { Destroy(m_objText); m_objText = null; }
		if ( m_objInven != null ) { Destroy(m_objInven); m_objInven = null; }
	}
}
