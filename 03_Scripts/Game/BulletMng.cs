using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

public class BulletMng : MonoBehaviour, ICleanUp
{
	#region SINGLETON

	public static bool destroyThis = false;

	static BulletMng _instance = null;

	public static BulletMng Ins
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(BulletMng)) as BulletMng;
				if (_instance == null)
				{
					_instance = new GameObject("BulletMng", typeof(BulletMng)).GetComponent<BulletMng>();
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

	const int nFIRST_COUNT = 50;
	const int nMAKING_COUNT = 10;

	GameObject			m_objRoot = null;

	LinkedList<Bullet>	m_lliBullet = new LinkedList<Bullet>();
	List<Bullet>		m_liRemove = new List<Bullet>();
	LinkedList<Bullet>	m_lliBulletPool = new LinkedList<Bullet>();

	public void EnterDungeon(GameObject a_objBulletRoot)
	{
		m_objRoot = a_objBulletRoot;
		AddPool(nFIRST_COUNT);
	}

	public void ChangeSceneCleanUp()
	{
		m_lliBullet.Clear();
		m_liRemove.Clear();
		m_lliBulletPool.Clear();

		m_objRoot = null;
	}

	void AddPool(int a_nCount)
	{
		for( int i=0; i<a_nCount; ++i )
		{
			m_objRoot.Instantiate_asChild("Game/Bullet");
		}
		
		// m_lliBulletPool.
	}

	public void AddShot(eBullet a_eBulletID, ST_AttackInfo a_refAtk, ref Vector3 a_VcFirstPos, Character a_refTarget = null)
	{
		if( m_lliBulletPool.Count == 0 )
		{
			AddPool(nMAKING_COUNT);
		}

		var val = m_lliBulletPool.First.Value;
		val.SetData(a_eBulletID.GetBulletData(), a_refAtk);

		m_lliBullet.AddLast(val);
		m_lliBulletPool.RemoveFirst();
	}

	public void DoUpdate(float a_fDelta)
	{
		var node = m_lliBullet.GetEnumerator();

		while (node.MoveNext())
		{
			node.Current.DoUpdate(a_fDelta);

			if (node.Current.bIsDie == true)
			{
				m_liRemove.Add(node.Current);
			}
		}

		int nCount = m_liRemove.Count;

		if (nCount > 0)
		{
			for (int i = 0; i < nCount; ++i)
			{
				m_liRemove[i].Reset();
				m_lliBulletPool.Remove(m_liRemove[i]);
				m_lliBulletPool.AddLast(m_liRemove[i]);
			}
			
			m_liRemove.Clear();
		}
	}
}
