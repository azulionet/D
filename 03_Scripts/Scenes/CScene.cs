using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

using Global_Define;

public class CScene : MonoBehaviour, IAtlas
{
	#region INSPECTOR

	public eScene		m_eScene;

	public GameObject	m_objUIRoot;
	public GameObject	m_objRoomRoot;
	public GameObject	m_objCharacterRoot;
	public GameObject	m_objBulletRoot;

	public List<SpriteAtlas> m_liAtlas;
	
	#endregion

	protected void Awake()
	{
		CSceneMng.Ins.SetScene(this);
		
		for( int i=0; i<m_liAtlas.Count; ++i )
		{
			string name = m_liAtlas[i].name;
			eAtlas eAt = (eAtlas)System.Enum.Parse(typeof(eAtlas), name);
			m_mapAtlas.Add(eAt, m_liAtlas[i]);
		}
		
		if (m_objRoomRoot == null)		{ m_objRoomRoot = gameObject; }
		if (m_objCharacterRoot == null)	{ m_objCharacterRoot = gameObject; }
		if (m_objBulletRoot == null)	{ m_objBulletRoot = gameObject; }
	}

	virtual public void CloseAllPopup() { }
	virtual public void CleanUp() { }
	
	// IAtals
	Dictionary<eAtlas, SpriteAtlas> m_mapAtlas = new Dictionary<eAtlas, SpriteAtlas>();

	public SpriteAtlas GetAtlas(eAtlas a_eAtlas)
	{
		if (m_mapAtlas.ContainsKey(a_eAtlas) == false)
		{
			string FileName_withPath = string.Format(Path.ATLAS_PATH_ADD, a_eAtlas.ToString());

			SpriteAtlas objAtlas = Resources.Load(FileName_withPath) as SpriteAtlas;

			m_mapAtlas.Add(a_eAtlas, objAtlas);
		}

		return m_mapAtlas[a_eAtlas];
	}

	public Sprite GetSprite(eAtlas a_eAtlas, string a_strSpriteName)
	{
		return GetAtlas(a_eAtlas).GetSprite(a_strSpriteName);
	}
}
