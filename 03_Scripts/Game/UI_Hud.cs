using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class UI_Hud : MonoBehaviour, IFixedUpdate
{
	#region SINGLETON

	public static bool destroyThis = false;

	static UI_Hud _instance = null;

	public static UI_Hud Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(UI_Hud)) as UI_Hud;
				if (_instance == null)
				{
					_instance = new GameObject("UI_Hud", typeof(UI_Hud)).GetComponent<UI_Hud>();
				}
			}

			return _instance;
		}
	}

	#endregion

	#region INSPECTOR

	public Canvas m_objParent;
	public List<UI_HpBar> m_liHPBar;

	#endregion

	LinkedList<UI_HpBar> m_liActive = new LinkedList<UI_HpBar>();
	LinkedList<UI_HpBar> m_liDeactive = new LinkedList<UI_HpBar>();

	void Awake()
	{
		for( int i=0; i<m_liHPBar.Count; ++i )
		{
			m_liDeactive.AddLast(m_liHPBar[i]);
		}
	}

	public UI_HpBar ActiveHPBar(Character a_refTarget)
	{
		var node = m_liDeactive.First;

		node.Value.SetTarget(a_refTarget);
		m_liActive.AddLast(node.Value);

		m_liDeactive.RemoveFirst();

		return node.Value;
	}

	public void DeactiveHPBar(UI_HpBar a_refBar)
	{
		m_liActive.Remove(a_refBar);
		m_liDeactive.AddLast(a_refBar);
	}

	public void DoFixedUpdate(float a_fDelta)
	{
		foreach(var node in m_liActive)
		{
			node.DoFixedUpdate(a_fDelta);
		}
	}
}
