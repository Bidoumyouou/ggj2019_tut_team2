using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public enum FallSEType
	{
		Light,
		Light2,
		Middle,
		Middle2,
		Heavy
	}

	public AudioClip[] FallClips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	public void PlayFall(Item item)
	{
		AudioSource source = item.gameObject.AddComponent<AudioSource>();
		source.clip = FallClips[(int)item.fallSE];
		source.Play();
	}
}
