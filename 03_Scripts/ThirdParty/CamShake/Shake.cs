using UnityEngine;

public struct ShakeValue
{
	public ShakeValue(Shake.ShakeType shake, Shake.NoiseType noise, Vector3 move,
						Vector3 rot, float speed, float duration)
	{
		eShake = shake;
		eNoise = noise;
		MoveExtents = move;
		RotateExtents = rot;
		Speed = speed;
		Duration = duration;
	}

	public Shake.ShakeType eShake; // = Shake.ShakeType.EaseOut;
	public Shake.NoiseType eNoise; // = Shake.NoiseType.Perlin;
	public Vector3 MoveExtents;
	public Vector3 RotateExtents;
	public float Speed;
	public float Duration;
}

public class Shake
{
	public enum ShakeType { Constant, EaseIn, EaseOut, EaseInOut }
	public enum NoiseType { Perlin, Sin }

	ShakeValue v = new ShakeValue();

	private Vector3 m_seed;
	private float m_startTime;
	private bool m_loop;

	private const float kTransitionDuration = 1.0f;
	private const float kSeedRange = 1000.0f;
	
	public void SetShakeValue(ref ShakeValue val)
	{
		v = val;

		m_startTime = Time.time;
		m_seed = new Vector3(Random.Range(0.0f, kSeedRange), Random.Range(0.0f, kSeedRange), Random.Range(0.0f, kSeedRange));
		m_loop = v.Duration == -1.0f ? true : false;

		// If loop, ease in
		if (m_loop)
		{
			v.Duration = kTransitionDuration;
		}
	}

	public void Finish(bool immediate = false)
	{
		// Ramp down the shake effect
		if (m_loop || v.Duration > kTransitionDuration)
		{
			m_loop = false;
			v.eShake = ShakeType.EaseOut;
			v.Duration = kTransitionDuration;

			if (immediate)
			{
				m_startTime = Time.time - v.Duration;
			}
			else
			{
				m_startTime = Time.time;
			}
		}
	}

	public Matrix4x4 ComputeMatrix()
	{
		Vector3 current = v.Speed * (Time.time * Vector3.one + m_seed);
		Vector3 adjustedMove = AdjustExtents(v.MoveExtents, v.eShake);
		Vector3 adjustedRotate = AdjustExtents(v.RotateExtents, v.eShake);

		Vector3 pos = Vector3.zero;
		if (v.MoveExtents != Vector3.zero)
		{
			pos = ApplyNoise(current, adjustedMove);
		}

		Quaternion rot = Quaternion.identity;
		if (v.RotateExtents != Vector3.zero)
		{
			rot = Quaternion.Euler(ApplyNoise(current, adjustedRotate));
		}

		return Matrix4x4.TRS(pos, rot, Vector3.one);
	}

	public bool IsDone()
	{
		return !m_loop && GetT() >= 1.0f;
	}

	private float GetT()
	{
		float t = Mathf.Clamp((Time.time - m_startTime) / v.Duration, 0.0f, 1.0f);
		return ApplyEaseOutSin(0.0f, 1.0f, t);
	}

	private float ApplyEaseOutSin(float start, float end, float value)
	{
		return (end - start) * Mathf.Sin((value / 1.0f) * (Mathf.PI / 2.0f)) + start;
	}

	private Vector3 AdjustExtents(Vector3 extents, ShakeType shakeType)
	{
		switch (shakeType)
		{
			case ShakeType.Constant:
				return extents;
			case ShakeType.EaseIn:
				return Vector3.Slerp(Vector3.zero, extents, GetT());
			case ShakeType.EaseOut:
				return Vector3.Slerp(extents, Vector3.zero, GetT());
			case ShakeType.EaseInOut:
				return GetT() < 0.5f ? AdjustExtents(extents, ShakeType.EaseIn) : AdjustExtents(extents, ShakeType.EaseOut);
		}
		return extents;
	}

	private Vector3 ApplyNoise(Vector3 target, Vector3 amplitude)
	{
		switch (v.eNoise)
		{
			case NoiseType.Sin:
			{
				float x = amplitude.x * Mathf.Sin(target.x);
				float y = amplitude.y * Mathf.Sin(target.y);
				float z = amplitude.z * Mathf.Sin(target.z);
				return new Vector3(x, y, z);
			}
			case NoiseType.Perlin:
			{
				float x = amplitude.x * 2.0f * (Mathf.PerlinNoise(target.x, target.x) - 0.5f);
				float y = amplitude.y * 2.0f * (Mathf.PerlinNoise(target.y, target.y) - 0.5f);
				float z = amplitude.z * 2.0f * (Mathf.PerlinNoise(target.z, target.z) - 0.5f);
				return new Vector3(x, y, z);
			}
		}
		return target;
	}
}
