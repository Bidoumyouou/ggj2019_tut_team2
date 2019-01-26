using UnityEngine;
using System;
using System.Collections.Generic;

public static class GameContext
{
	public static Camera MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	public static SoundManager SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
}
