using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public ItemGenerator ItemGen;
	public GravityGunEffect GunEffect;

	Item lastPreviewedItem_ = null;

	// Start is called before the first frame update
	void Start()
	{
	}

    // Update is called once per frame
    void Update()
	{
		Item touchedItem = null;
		foreach( Item item in ItemGen.FloatingItemList )
		{
			if( item.IsOnMouse() )
			{
				touchedItem = item;
				break;
			}
		}

		if( touchedItem != null )
		{
			GunEffect.gameObject.SetActive(true);
			if( Input.GetMouseButtonDown(0) )
			{
				// クリックで発射
				GunEffect.Fire();
				touchedItem.Fall();
			}
			else
			{
				// マウスオーバーでプレビュー表示
				GunEffect.Preview(touchedItem);
				lastPreviewedItem_ = touchedItem;
			}
		}
		else
		{
			if( lastPreviewedItem_ != null )
			{
				GunEffect.EndPreview();
				lastPreviewedItem_ = null;
			}
			GunEffect.gameObject.SetActive(false);
		}
	}
}
