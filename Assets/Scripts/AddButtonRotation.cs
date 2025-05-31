using UnityEngine;
using System.Collections;

public class AddButtonRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// adding the plus button to tween rotation value
		iTween.RotateBy(this.gameObject, iTween.Hash("z", -0.25, "easeType", "easeInOutCirc", "loopType", "loop", "delay", .4));

	}

}
