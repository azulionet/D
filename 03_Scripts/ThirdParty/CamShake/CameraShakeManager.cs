using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class CameraShakeManager : MonoBehaviour
{
	private LinkedList<Shake> m_activeShakes = new LinkedList<Shake>();
	private ShakePool m_ShakePool = new ShakePool(10);
	private Dictionary<string, ShakeValue> m_mapValue = new Dictionary<string, ShakeValue>();

	#region SINGLETON

	private static CameraShakeManager m_instance;
	public static CameraShakeManager Instance {
		get { return m_instance; }
	}

	void Awake()
	{
		m_instance = this;
		m_instance.m_mapValue.Add("Ambient",
			new ShakeValue(Shake.ShakeType.EaseIn, Shake.NoiseType.Perlin,
							new Vector3(0.1f, 0.1f, 0.1f), Vector3.one, 0.25f, -1));
		m_instance.m_mapValue.Add("Default",
			new ShakeValue(Shake.ShakeType.EaseOut, Shake.NoiseType.Sin,
							new Vector3(0.1f, 0.1f, 0.1f), new Vector3(0,5,5), 100, 1));
		m_instance.m_mapValue.Add("Impact",
			new ShakeValue(Shake.ShakeType.EaseOut, Shake.NoiseType.Sin,
							new Vector3(0.0f, 0.1f, 0.0f), new Vector3(5,0,0), 100, 0.4f));
	}

	#endregion SINGLETON

	private Camera Camera
	{
		get { return GetComponent<Camera>(); }
	}

	public Shake Play(string name)
	{
		var sh = m_ShakePool.GetFromPool();

		ShakeValue val;
		
		if( m_mapValue.TryGetValue(name, out val) )
		{
			sh.SetShakeValue(ref val);
		}

		m_activeShakes.AddLast(sh);

		return sh;
	}

	void LateUpdate() {
		Matrix4x4 shakeMatrix = Matrix4x4.identity;

		// For each active shake
		foreach (var shake in m_activeShakes.Reverse<Shake>()) {

			// Concatenate its shake matrix
			shakeMatrix *= shake.ComputeMatrix();

			// If done, remove
			if (shake.IsDone()) {

				m_ShakePool.Return2Pool(shake);
				m_activeShakes.Remove(shake);
				
				Camera.ResetWorldToCameraMatrix();
			}
		}

		// Camera always looks down the negative z-axis
		shakeMatrix *= Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));

		// Update camera matrix
		if (m_activeShakes.Count > 0) {
			Camera.worldToCameraMatrix = shakeMatrix * transform.worldToLocalMatrix;
		}
	}
	
	public void Stop(Shake shake, bool immediate = false) {
		if (shake == null) return;

		shake.Finish(immediate);
	}

	public void StopAll(bool immediate = false) {
		foreach (var shake in m_activeShakes) {
			Stop(shake, immediate);
		}
	}

	public void OnClick1()
	{
		Instance.Play("Ambient");	
	}

	public void OnClick2()
	{
		Instance.Play("Default");
	}

	public void OnClick3()
	{
		Instance.Play("Impact");
	}	
}
