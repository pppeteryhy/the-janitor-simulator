using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour {

    public Camera uiCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void RaycastToSearch(float dist, out GarbageBase garbage, out GarbageCollectorCar car, out IOutline oulineObj)
    {
        garbage = null;
        car = null;
        oulineObj = null;
        Ray ray = uiCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            IOutline _outline = hit.collider.GetComponentInParent<IOutline>();
            if(_outline != null)
            {
                oulineObj = _outline;
            }
            GarbageBase gar = hit.collider.GetComponentInParent<GarbageBase>();
            GarbageCollectorCar _car = hit.collider.GetComponentInParent<GarbageCollectorCar>();
            if (gar != null)
            {
                garbage = gar;
            }
            else if(_car != null)
            {
                car = _car;
            }
        }
    }
}
