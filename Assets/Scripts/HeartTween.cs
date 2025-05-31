using UnityEngine;
using System.Collections;

public class HeartTween : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
		iTween.ScaleTo(this.gameObject, iTween.Hash("x", 1.2f,"y", 1.2f, "easeType", "easeInOutQuart", "loopType", "pingPong"));
	}
	

}
