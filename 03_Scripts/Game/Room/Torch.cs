using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;
using Global_Define;

public class Torch : UpdateableObject
{
	#region INSPECTOR

	public LightSprite m_sprLight;

	[Range(0, 255)]
	public int m_nStartAlpha = 50;

	[Range(0, 255)]
	public int m_nLastAlpha = 150;

	[Range(0.1f, 3f)]
	public float m_fChangeTime = 0.1f;
	
	#endregion

	float m_fCheck = 0.0f;

	void Awake()
	{
		SetLightValue();
	}

	void SetLightValue()
	{
		m_fCheck = m_fChangeTime * Rand.Percent();

		var color = m_sprLight.Color;
		color.a = Rand.Range(m_nStartAlpha, m_nLastAlpha) / (float)255;
		m_sprLight.Color = color;
	}

	public override void DoFixedUpdate(float a_fDelta)
	{
		m_fCheck -= a_fDelta;

		if( m_fCheck < 0 )
		{
			SetLightValue();
		}
	}
}
