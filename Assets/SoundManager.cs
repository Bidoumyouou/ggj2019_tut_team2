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

	public enum MusicTrackType
	{
		None,
	}

	public AudioClip[] FallClips;
	public AudioClip[] MusicClips;
	public AudioSource BaseMusic;

	List<AudioSource> musicTracks_ = new List<AudioSource>();
	List<float> targetVolumes_ = new List<float>();

	// Start is called before the first frame update
	void Start()
    {
		foreach( AudioClip musicTrack in MusicClips )
		{
			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.clip = musicTrack;
			source.volume = 0;
			source.loop = true;
			source.Play();
			musicTracks_.Add(source);
			targetVolumes_.Add(0);
		}
		BaseMusic.Play();
	}

    // Update is called once per frame
    void Update()
    {
		for( int i = 0; i < targetVolumes_.Count; ++i )
		{
			float diff = (targetVolumes_[i] - musicTracks_[i].volume);
			if( Mathf.Abs(diff) > 0.01f )
			{
				musicTracks_[i].volume += diff * 0.05f;
			}
			else
			{
				musicTracks_[i].volume = targetVolumes_[i];
			}
		}
    }


	public void PlayFall(Item item)
	{
		AudioSource source = item.gameObject.AddComponent<AudioSource>();
		source.clip = FallClips[(int)item.fallSE];
		source.Play();
	}

	public void PlayMusic(MusicTrackType type)
	{
		targetVolumes_[(int)type] = 1.0f;
	}
}
