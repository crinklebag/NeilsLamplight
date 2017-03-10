using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSceneLoad : MonoBehaviour {
	
	[SerializeField] GameObject loader;
	public void Enable()
	{
		loader.SetActive(true);

	}
	

}
