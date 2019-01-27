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
	public Color HighCostColor;
	public Text ConsumeGText;
	public Text LastShotText;
	public GravityGauge GGauge;
	public float RemainTime = 0.5f;
	public float ScaleCoeff = 0.2f;

	Color color_;
	float time_;
	AudioSource audioSource_;
	LineRenderer[] thunders_;
	bool isLastShot_ = false;

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

	public void Fire(Item item)
	{
		// これ自体は残して、プレイヤーの腕に追随しないエフェクトだけ出して消したいので、別オブジェクトにしてFire＆Destroy!
		GravityGunEffect remainGunEffect = Instantiate(this, this.transform.position, this.transform.rotation);
		remainGunEffect.transform.localScale = this.transform.lossyScale;
		remainGunEffect.SetColor(GetDesiredColor(item));
		audioSource_.Play();
		AnimManager.AddAnim(remainGunEffect, 0.0f, ParamType.AlphaColor, AnimType.Time, 0.2f, (isLastShot_ ? RemainTime * 3 : RemainTime), endOption: AnimEndOption.Destroy);
	}

	public void Preview(Item item)
	{
		SetColor(ColorManager.MakeAlpha(GetDesiredColor(item), 0.15f));
		TargetPoint.transform.position = item.transform.position;
		TargetPoint.transform.localScale = Vector3.one * ScaleCoeff * (float)item.consumeGPoint / 1000.0f;

		isLastShot_ = item.consumeGPoint >= GGauge.Gravity;

		ConsumeGText.gameObject.SetActive(true);
		LastShotText.gameObject.SetActive(isLastShot_);
		LastShotText.color = Color.Lerp(Color.red, Color.white, (Mathf.Sin(Time.time * 20.0f) + 1) / 2 + 0.5f);
		ConsumeGText.rectTransform.anchoredPosition = Input.mousePosition;
		ConsumeGText.text = item.consumeGPoint.ToString();
		ConsumeGText.color = GetDesiredColor(item);
		ConsumeGText.transform.localScale = Vector3.one * Mathf.Sqrt((float)item.consumeGPoint / 1000.0f);

		GGauge.SetPreviewPoint(Mathf.Max(0, (int)GGauge.Gravity - item.consumeGPoint));
	}

	Color GetDesiredColor(Item item)
	{
		if( isLastShot_ )
			return HighCostColor;

		return Color.Lerp(DefaultColor, HighCostColor, Mathf.Clamp01((float)(item.consumeGPoint - 100) / 500.0f));
	}

	public void EndPreview()
	{
		ConsumeGText.gameObject.SetActive(false);
		GGauge.SetPreviewPoint((int)GGauge.Gravity);
		isLastShot_ = false;
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

			time_ = 0;
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
		TargetPoint.GetComponentInChildren<SpriteRenderer>().color = color_;
	}

	public Color GetColor()
	{
		return color_;
	}
}
