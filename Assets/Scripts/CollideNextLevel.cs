using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideNextLevel : MonoBehaviour
{
	public LoaderScriptMM scriptLoad;
	public string level;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "NextLevel")
		{
			scriptLoad.LoadAnimation(level);
		}
	}
}
