using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global_Define;

// 모든 Room클래스가 m_liMonsterPool안의 몬스터를 공유
// 만약 모든 Room이 가지는 몬스터가 3개라면 m_liMonsterPool의 3번째 까지의 리소스만 사용되게 됨
public class MonsterPool
{
	const int nFIRST_MAKE_COUNT = 10;

	GameObject m_objMonsterRoot = null;

	// 아틀라스가 따로 구성되있을 경우 풀을 따로 만드는 편이 유리
	LinkedList<BasicMonster> m_liMonsterPool = new LinkedList<BasicMonster>();

	public void CleanUp()
	{
		m_liMonsterPool.Clear();
		m_objMonsterRoot = null;
	}

	public void SetData(GameObject a_objRoot)
	{
		m_objMonsterRoot = a_objRoot;
		
		if(a_objRoot != null )
		{
			GetMonster(nFIRST_MAKE_COUNT);
		}
	}

	public void GetMonster(int a_nMonsterCount, LinkedList<BasicMonster> m_refList = null)
	{
		if ( a_nMonsterCount > m_liMonsterPool.Count )
		{
			a_nMonsterCount -= m_liMonsterPool.Count;

			for( int i=0; i< a_nMonsterCount; ++i )
			{
				var monster = m_objMonsterRoot.Instantiate_asChild("Game/Monster").GetComponent<BasicMonster>();

				m_liMonsterPool.AddLast(monster);
			}
		}

		if( m_refList == null ) { return; }

		m_refList.Clear();

		var node = m_liMonsterPool.First;

		for ( int i=0; i<a_nMonsterCount; ++i )
		{
			
			m_refList.AddLast(node.Value);

			node = node.Next;
			// m_liMonsterPool.RemoveFirst(); // 보통은 넣고 빼고 하는게 맞지만 꼭 뺄 필요가없어서 완전 공용으로 사용
		}
	}
}
