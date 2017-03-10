using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;

public class followhand : BodySourceView {


	public GameObject bodypart;
	public Kinect.JointType TrackedJoint;
	private BodySourceManager bodyManager;
	private Kinect.Body[] bodies;

	bool lostSkeleton = false;

	[SerializeField]public float speed;

	[SerializeField]public float distance;

	public Vector3 temp;

	// Use this for initialization
	void Start () {
		bodyManager = BodySourceManager.GetComponent<BodySourceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (bodyManager == null)
		{
			// Debug.Log ("Body Manager Null");
			return;
		}
		bodies = bodyManager.GetData ();
		if (bodies == null && !lostSkeleton)
		{
			// NOTIFY PLAYER SKELETON IS BEING BUILT
			 Debug.Log ("bodies null");
			lostSkeleton = true;
			return;
		}
		foreach (var body in bodies) 
		{
			if (body == null)
			{
				Debug.Log ("no skeleton!!!!");
				continue;
			}
			if (body.IsTracked) 
			{
				// Debug.Log ("Body Tracked");
				if (body.Joints [TrackedJoint].Position.Z < distance) {
					var pos = body.Joints [TrackedJoint].Position;

					// Debug.Log (body.Joints [TrackedJoint].Position.Z);
					temp = new Vector3 (pos.X, pos.Y, 0);

					//var rot = body.Joints [TrackedJoint].Position;
					//gameObject.transform.position = new Vector3 (pos.X, pos.Y,0)*Time.time*speed;
					//gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(pos.X,pos.Y,0), Time.deltaTime * speed * 1000);
					//gameObject.transform.rotation = new Quaternion (rot.X, rot.Y,0,0);

					SmoothMove ();

				}

			}
		}


	}

	void SmoothMove()
	{
		this.transform.position = Vector3.Lerp (this.transform.position, temp * speed, Time.smoothDeltaTime);
		// Debug.Log ("Moving");
	}

	IEnumerator BuildSkeleton(){
		yield return new WaitForSeconds (0.1f);
		if (bodies == null) {
			// Keep notifying the player


		} else {
			// Skeleton is built - start game
		}
	}
}
