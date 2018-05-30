using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ManusVR.Scripts;
using Parabox.STL;
using Assets.ManusVR.Scripts.PhysicalInteraction;

public class DrawLineManager : MonoBehaviour
{

    public Transform fingerPos;

    public Material lMat;

    private MeshLineRenderer currLine;

    public HandData HandData;

    public device_type_t DeviceType;

    private int numClicks = 0;
    private bool init = false;
    private Vector3 vec;

    private int i = 0;
    

    void Update()
    {

        if (HandData.HandStartedPointing(DeviceType)) { 
            Debug.Log("first click");
            init = true;
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            go.tag = "PaintObject";
            
            currLine = go.AddComponent<MeshLineRenderer>();
            go.transform.position = go.GetComponent<MeshRenderer>().bounds.center;
            Debug.Log(go.GetComponent<MeshRenderer>().bounds.center);
            
            /*
            //tried to use the InteractableItem component in order to move the items - failed.. 
            go.AddComponent<MeshCollider>();
            go.AddComponent<Rigidbody>();
            go.GetComponent<Rigidbody>().useGravity = false;
            go.AddComponent<MeshCollider>();
            go.AddComponent<CollisionDetector>();
            go.AddComponent<CapsuleCollider>();
            go.AddComponent<BoxCollider>();
            go.GetComponent<MeshCollider>().convex = true;
            go.AddComponent<objectDragger>();
            go.AddComponent<InteractablePaintedItem>();
            go.GetComponent<InteractablePaintedItem>().IsGrabbable = true;
            go.GetComponent<InteractablePaintedItem>().GravityWhenGrabbed = false;
            go.GetComponent<InteractablePaintedItem>().GravityWhenReleased = false;
            go.GetComponent<InteractablePaintedItem>().KinematicWhenReleased = false;
            go.GetComponent<InteractablePaintedItem>().AttachHandToItem = true;
            go.GetComponent<InteractablePaintedItem>().DropDistance = (float)0.25;
            */

            currLine.lmat = lMat;
            currLine.setWidth(.05f);
        }
        else if (HandData.HandPoints(DeviceType))
        {
            Debug.Log(string.Format("Vector is {0} {1}, {2}", vec.x, vec.y, vec.z));
            vec = fingerPos.position;
            currLine.AddPoint(vec);
            numClicks++;
        }

        //save obj as .stl
        if (Input.GetMouseButtonDown(1))
        {
            if (currLine != null)
            {
                GameObject[] gos = { currLine.gameObject };
                pb_Stl_Exporter.Export(string.Format("test{0}.stl", i++), gos, FileType.Ascii);
            }
        }

    }

    
}
