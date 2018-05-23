using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{

    public Transform fingerPos;

    public Material lMat;

    private MeshLineRenderer currLine;

    private int numClicks = 0;
    private bool init = false;
    private Vector3 vec;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && init == false)
        {
            Debug.Log("first click");
            init = true;
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            currLine = go.AddComponent<MeshLineRenderer>();

            currLine.lmat = lMat;
            currLine.setWidth(.05f);
        }
        else if (init == true)
        {
            Debug.Log(string.Format("Vector is {0} {1}, {2}", vec.x, vec.y, vec.z));
            vec = fingerPos.position;
            currLine.AddPoint(vec);
            numClicks++;
        }
    }
}
