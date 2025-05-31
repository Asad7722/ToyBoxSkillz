using UnityEngine;
/** Rectangle transform rotator */
public class toyAnimation : MonoBehaviour {

    public void log(string s){
        Debug.Log(s);
    }
    RectTransform tr;

	public float range = 10f;
	public float Speed = 0.5f;

    Quaternion target;

	void Start () {
		tr = GetComponent<RectTransform>();
        target = Quaternion.Euler(0,0,range);
	}    	
}
