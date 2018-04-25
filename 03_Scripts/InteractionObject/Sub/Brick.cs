using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : TriggerObject
{
	public enum eState
	{
		None = -1,

		Repeat,
		Once,
	}

	#region INSPECTOR

	public Sprite m_sprEnable;
	public Sprite m_sprDisable;
	public eState m_eState;

	#endregion INSPECTOR

	public System.Action<Character> m_fpTrigger = null;
	public System.Action<Character> m_fpTriggerExit = null;

	public void SetInteraction(eState a_eState,
								System.Action<Character> a_fpCallback,
								System.Action<Character> a_fpExit = null)
	{
		m_fpTrigger = a_fpCallback;
		m_fpTriggerExit = a_fpExit;

		m_eState = a_eState;

		Refresh();
	}

	public void Refresh()
	{
		switch (m_eState)
		{
			case eState.None:
			{
				m_Img.sprite = m_sprDisable;
			} break;
			case eState.Once:
			case eState.Repeat:
			{
				m_Img.sprite = m_sprEnable;
			}
			break;
		}
	}

	public override void Trigger(Character a_obj)
	{
		if (m_fpTrigger != null && a_obj != null)
		{
			if (a_obj.transform.localPosition.y < transform.localPosition.y)
			{
				m_fpTrigger(a_obj);

				if( m_eState == eState.Once )
				{
					m_fpTrigger = null;
					m_eState = eState.None;
					Refresh();
				}
			}
		}
	}

	public override void TriggerExit(Character a_obj)
	{
		if( m_fpTriggerExit != null && a_obj != null )
		{
			m_fpTriggerExit(a_obj);

			if (m_eState == eState.Once)
			{
				m_fpTriggerExit = null;
				m_eState = eState.None;
				Refresh();
			}
		}
	}
}
