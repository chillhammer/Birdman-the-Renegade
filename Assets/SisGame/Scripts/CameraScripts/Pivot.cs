using UnityEngine;
using System.Collections;

public class Pivot : FollowTarget {

	protected Transform cam;
	protected Transform pivot;
	protected Vector3 lastTargetPosition;

	protected virtual void Awake()
	{
		cam = GetComponentInChildren<Camera>().transform;
		pivot = cam.parent;
		}
	
	// Use this for initialization
 protected override	void Start () {
	
		base.Start();
	}
	
	// Update is called once per frame
	virtual protected void Update () {
	   
		if (!Application.isPlaying)
		{  
			if(target != null)
			{
				Follow(999);
				lastTargetPosition = target.position;
			}

			if(Mathf.Abs(cam.localPosition.x) > .5f || Mathf.Abs(cam.localPosition.y) > .5f)
			{
				cam.localPosition = Vector3.Scale(cam.localPosition, Vector3.forward);
			}

			cam.localPosition = Vector3.Scale(cam.localPosition, Vector3.forward);
		}
	}
	protected override void Follow(float deltaTime)
	{

	}
}
