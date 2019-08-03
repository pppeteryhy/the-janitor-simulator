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

    public void RaycastToSearch(float dist, out GarbageBase garbage)
    {
        garbage = null;
        Ray ray = uiCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dist))
        {
            GarbageBase gar = hit.collider.GetComponentInParent<GarbageBase>();
            if(gar != null)
            {
                garbage = gar;
            }
        }
    }
}
