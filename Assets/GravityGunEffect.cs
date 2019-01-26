using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGunEffect : MonoBehaviour, IColoredObject
{
	public LineRenderer ThunderPrefab;
	public GameObject TargetPoint;
	public float VertexPerLength;
	public float RandomRange;
	public float UpdateTime;
	public Color DefaultColor;

	Color color_;
	float time_;
	AudioSource audioSource_;
	LineRenderer[] thunders_;

    // Start is called before the first frame update
    void Awake()
    {
		for( int i = 0; i < 2; ++i )
		{
			Instantiate(ThunderPrefab, this.transform);
		}
		thunders_ = GetComponentsInChildren<LineRenderer>(includeInactive: true);
		audioSource_ = GetComponent<AudioSource>();
		color_ = ThunderPrefab.startColor;
	}

	// Update is called once per frame
	void Update()
	{
		Animate();
	}

	public void Fire()
	{
		GravityGunEffect remainGunEffect = Instantiate(this, this.transform.position, this.transform.rotation);
		remainGunEffect.FireAndDestroy();
	}

	public void FireAndDestroy()
	{
		audioSource_.Play();
		SetColor(DefaultColor);
		AnimManager.AddAnim(this, 0.0f, ParamType.AlphaColor, AnimType.Time, 0.5f, 0.5f, endOption: AnimEndOption.Destroy);
	}

	public void Preview(Item item)
	{
		SetColor(ColorManager.MakeAlpha(DefaultColor, 0.3f));
		TargetPoint.transform.position = item.transform.position;
	}

	void Animate()
	{
		time_ += Time.deltaTime;
		if( time_ > UpdateTime )
		{
			Vector3 direction = TargetPoint.transform.localPosition;
			float length = direction.magnitude;
			int numVertex = (int)(VertexPerLength * length);
			for( int i = 0; i < thunders_.Length; ++i )
			{
				thunders_[i].positionCount = numVertex;
				for( int pi = 0; pi < numVertex; ++pi )
				{
					Vector3 linePosition = direction * ((float)pi / (numVertex - 1));
					if( pi != 0 & pi != numVertex - 1 )
					{
						linePosition += Random.insideUnitSphere * RandomRange;
					}
					thunders_[i].SetPosition(pi, linePosition);
				}
			}
		}
	}


	public void SetColor(Color color)
	{
		color_ = color;
		for( int i = 0; i < thunders_.Length; ++i )
		{
			thunders_[i].startColor = color_;
			thunders_[i].endColor = color_;
		}
		TargetPoint.GetComponent<SpriteRenderer>().color = color_;
	}

	public Color GetColor()
	{
		return color_;
	}
}
