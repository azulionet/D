using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : InGameObject
{
	public abstract void Trigger(Character a_obj);
	public virtual void TriggerExit(Character a_obj) { }
}
