  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour, iGazeReceiver
{
    private bool isGazingUpon;
    private bool isRotated;
    public GameObject face;
    public Renderer mesh_renderer;
    public Material smile_face;
    public Material[] mat;

    // Start is called before the first frame update
    void Start() {
    	print(gameObject.name);
    	GameObject mesh = gameObject.transform.Find("Mesh").gameObject;
    	face = mesh.transform.Find("Face01").gameObject;
    	mesh_renderer = face.GetComponent<Renderer>();
    	mat = mesh_renderer.materials;
    	smile_face = Resources.Load<Material>("Face03");
    	print(face.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGazingUpon)
        {
        	if (!isRotated) {
        		//transform.Rotate(0, 30, 0);
        		isRotated = true;
        		mat[0] = smile_face;
        		mesh_renderer.materials = mat;
        		print("changed!");
        	} else {
        		print("not rotate!");
        	}
        }
    }

    public void GazingUpon()
    {
        isGazingUpon = true;
    }

    public void NotGazingUpon()
    {
        isGazingUpon = false;
        isRotated = false;
    }
}