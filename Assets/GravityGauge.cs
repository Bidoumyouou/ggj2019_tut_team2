using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGauge : MonoBehaviour
{
	public float BlinkRate = 25;

	public float Gravity { get; private set; }
	float MaxGravity;
	float OldGravity;
	int TextCurrentGravity;

	public UIGaugeRenderer Current;
	public UIGaugeRenderer Prev;
	public UIGaugeRenderer Next;
	public Text Text;

	Color currentColor_;
	Color nextColor_;

	// Start is called before the first frame update
	void Awake()
    {
		Gravity = GameObject.Find("GameMgr").GetComponent<GameMgr>().startGPoint;
		MaxGravity = Gravity;
		OldGravity = Gravity;
		TextCurrentGravity = (int)Gravity;

		Current.SetRate(1.0f);
		Prev.SetRate(1.0f);
		Next.SetRate(1.0f);

		currentColor_ = Current.color;
		nextColor_ = Next.color;
	}

    // Update is called once per frame
    void Update()
    {
		Prev.SetColor(ColorManager.MakeAlpha(Prev.color, (Mathf.Sin(Time.time * BlinkRate) + 1) / 2));
		Current.SetColor(Color.Lerp(currentColor_, nextColor_, (Mathf.Sin(Time.time * BlinkRate/2) + 1) / 2 - 0.3f));

		if( TextCurrentGravity > (int)Gravity )
		{
			TextCurrentGravity -= 7;
			if( TextCurrentGravity <= (int)Gravity )
			{
				TextCurrentGravity = (int)Gravity;
			}

			Text.text = TextCurrentGravity.ToString();
		}
	}

	public void SetPreviewPoint(int previewPoint)
	{
		Next.SetRate(previewPoint / MaxGravity);
	}

	public void SetGravityPoint(float power)
	{
		Gravity = power;

		AnimManager.AddAnim(Current, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f);
		AnimManager.AddAnim(Next, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f);
		AnimManager.AddAnim(Prev, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f, 0.5f);

		OldGravity = Gravity;
	}
}
