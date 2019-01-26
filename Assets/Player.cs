using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public ItemGenerator ItemGen;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if( Input.GetMouseButtonDown(0) )
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
				touchedItem.Fall();
			}
		}
	}
}
