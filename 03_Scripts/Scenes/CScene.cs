using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class CScene : MonoBehaviour
{
	#region INSPECTOR

	public eScene		m_eScene;

	public GameObject	m_objUIRoot;
	public GameObject	m_objRoomRoot;
	public GameObject	m_objCharacterRoot;
	public GameObject	m_objBulletRoot;
	
	#endregion

	protected void Awake()
	{
		CSceneMng.Ins.SetScene(this);

		if (m_objRoomRoot == null)		{ m_objRoomRoot = gameObject; }
		if (m_objCharacterRoot == null)	{ m_objCharacterRoot = gameObject; }
		if (m_objBulletRoot == null)	{ m_objBulletRoot = gameObject; }
	}

	virtual public void CloseAllPopup() { }
	virtual public void CleanUp() { }
}
