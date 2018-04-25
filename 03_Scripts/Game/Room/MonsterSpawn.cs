using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
	#region INSPECTOR

	public Global_Define.eID eID = Global_Define.eID.___MONSTER___;

	#endregion

	public bool		bIsDie { get; set; }

	public Vector3	vcLocalPos { get { return gameObject.transform.localPosition; } }
	public Vector3	vcWorldPos { get { return gameObject.transform.position; } }
}
