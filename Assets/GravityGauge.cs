using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityGauge : MonoBehaviour
{
	public float BlinkRate = 25;

	public float Gravity = 2500;
	float MaxGravity;
	float OldGravity;
	int TextCurrentGravity;

	public UIGaugeRenderer Current;
	public UIGaugeRenderer Prev;
	public UIGaugeRenderer Next;
	public Text Text;

	// Start is called before the first frame update
	void Start()
    {
		MaxGravity = Gravity;
		OldGravity = Gravity;
		TextCurrentGravity = (int)Gravity;
	}

    // Update is called once per frame
    void Update()
    {
		Prev.SetColor(ColorManager.MakeAlpha(Prev.color, (Mathf.Sin(Time.time * BlinkRate) + 1) / 2));

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

	public void SetGravityPoint(float power)
	{
		Gravity = power;

		AnimManager.AddAnim(Current, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f);
		AnimManager.AddAnim(Next, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f);
		AnimManager.AddAnim(Prev, Gravity / MaxGravity, ParamType.GaugeRate, AnimType.Time, 0.1f, 0.5f);

		OldGravity = Gravity;
	}


	// debug
	public bool UpdateUI = false;
	void OnValidate()
	{
		if( UpdateUI )
		{
			SetGravityPoint(Gravity);
			UpdateUI = false;
		}
	}
}
