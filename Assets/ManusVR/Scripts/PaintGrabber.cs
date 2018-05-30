using Assets.ManusVR.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGrabber : MonoBehaviour {

    public HandData HandData;

    private GameObject _grabbedItem = null;

    public device_type_t DeviceType;

    private bool _isGrabbing = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var paint in GameObject.FindGameObjectsWithTag("PaintObject"))
        {
            if (!_isGrabbing && Vector3.Distance(paint.GetComponent<MeshRenderer>().bounds.center, DeviceType == device_type_t.GLOVE_RIGHT ? GameObject.Find("hand_s_r").transform.position : GameObject.Find("hand_s_l").transform.position) < 0.1f)
            {
                if(HandData.GetCloseValue(DeviceType) == CloseValue.Fist)
                {
                    if(DeviceType == device_type_t.GLOVE_LEFT)
                    {
                        GameObject leftHand = GameObject.Find("hand_s_l");
                        paint.transform.parent = leftHand.transform;
                    }
                    else
                    {
                        GameObject rightHand = GameObject.Find("hand_s_r");
                        paint.transform.parent = rightHand.transform;
                    }
                    _grabbedItem = paint;
                    _isGrabbing = true;

                }
            }
            else if (_isGrabbing)
            {
                if (HandData.GetCloseValue(DeviceType) != CloseValue.Fist)
                {
                    _grabbedItem.transform.parent = null;
                    _isGrabbing = false;
                }
            }
        }
	}
}
