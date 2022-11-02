using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
	[RequireComponent(typeof(Image))]
	public class SoundOnOff : MonoBehaviour
	{
		#region Unity Methods

		//private void OnEnable()
		//{
		//	Debug.Log("SoundOnOff : " + SoundManager.Instance.IsSoundEffectsOn);
		//	if (SoundManager.Instance.IsSoundEffectsOn)
		//		gameObject.GetComponent<Image>().sprite = SoundManager.Instance.soundOn;
		//	else
		//		gameObject.GetComponent<Image>().sprite = SoundManager.Instance.soundOff;
		//}

		#endregion
	}
}