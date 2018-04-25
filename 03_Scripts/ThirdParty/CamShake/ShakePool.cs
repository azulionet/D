using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakePool
{
	LinkedList<Shake> m_liPool = new LinkedList<Shake>();

	public ShakePool(int nCount)
	{
		m_liPool.AddLast(new Shake());
	}

	public void Return2Pool(Shake a_refShake)
	{
		m_liPool.AddLast(a_refShake);
	}

	public Shake GetFromPool()
	{
		if( m_liPool.Count == 0 )
		{
			m_liPool.AddLast(new Shake());
		}

		Shake sh = m_liPool.First.Value;
		m_liPool.RemoveFirst();
		return sh;
	}
}
