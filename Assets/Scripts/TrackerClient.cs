using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class TrackerClient : MonoBehaviour {

	private Dictionary<string, Human> _humans;

	void Start () {
		_humans = new Dictionary<string, Human>();
	}

	void Update () {
	
		foreach (Human h in _humans.Values)
		{
			// get human properties:
			string id = h.id;
			string handLeftState = h.body.Properties[BodyPropertiesType.HandLeftState];

			// get human joints positions:
			Vector3 headPosition = h.body.Joints[BodyJointType.head];
		}

		// finally
		_cleanDeadHumans();
	}

	public void setNewFrame (Body[] bodies)
	{
		foreach (Body b in bodies)
		{
			try
			{
			string bodyID = b.Properties[BodyPropertiesType.UID];
			if (!_humans.Keys.Contains(bodyID))
			{
				_humans.Add(bodyID, new Human());
			}
			_humans[bodyID].Update(b);
			}
			catch (Exception e) { }
		}
	}

	void _cleanDeadHumans ()
	{
		List<Human> deadhumans = new List<Human>();

		foreach (Human h in _humans.Values)
		{
			if (DateTime.Now > h.lastUpdated.AddMilliseconds(1000))
				deadhumans.Add(h);
		}

		foreach (Human h in deadhumans)
		{
			_humans.Remove(h.id);
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 200, 35), "Number of users: " + _humans.Count);
	}
}
