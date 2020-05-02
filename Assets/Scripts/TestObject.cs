using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.Animations;
using UnityEngine.Networking;
using System.Text;
using System.Net.Http;
using System;
using System.Threading.Tasks;

public class TestObject : MonoBehaviour, iGazeReceiver
{
    public bool isGazingUpon;
    public GameObject face;
    public Renderer mesh_renderer;
    public Material smile_face;
    public Material normal_face;
    public Material[] mat;
    public string physical_user;

    private FirebaseDatabase firebaseDatabase;

    //public Animator myAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
        GameObject mesh = gameObject.transform.Find("Mesh").gameObject;
        face = mesh.transform.Find("Face01").gameObject;
        mesh_renderer = face.GetComponent<Renderer>();
        mat = mesh_renderer.materials;
        smile_face = Resources.Load<Material>("Face03");
        normal_face = Resources.Load<Material>("Face01");

        firebaseDatabase = FirebaseDatabase.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGazingUpon)
        {
            mat[0] = smile_face;
            mesh_renderer.materials = mat;
            //myAnimationController.SetBool("headrotate", true);
            //Debug.Log("headrotate");

            //firebaseDatabase.RootReference.Child("VR_user").SetValueAsync(gameObject.name);
            //gameObject.GetComponent<Animator>().Play("headrotate");
            //PutAsync().Wait();
            StartCoroutine(Upload());

        }
        else
        {
            mat[0] = normal_face;
            mesh_renderer.materials = mat;
            //myAnimationController.SetBool("headrotate", false);

            //firebaseDatabase.GetReference("Physical_user").ValueChanged += HandleValueChanged;
            StartCoroutine(GetText());
        }
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://vr-all-presence.firebaseio.com/Physical_user.json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            print(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    //async Task PutAsync()
    //{
    //    var url = "https://vr-all-presence.firebaseio.com/VR_user.json";
    //    var client = new HttpClient();
    //    var response = await client.PutAsync(url, new StringContent("Bunny"));

    //    string result = response.Content.ReadAsStringAsync().Result;
    //    Console.WriteLine(result);
    //}

    IEnumerator Upload()
    {
        byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
        UnityWebRequest www = UnityWebRequest.Put("https://vr-all-presence.firebaseio.com/VR_user.json", myData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("jessie: " + www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
        }
    }


    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            print("db error");
            return;
        }
        // Do something with the data in args.Snapshot
        physical_user = args.Snapshot.Value.ToString();

        if (physical_user == gameObject.name)

        {

            print("Get" + gameObject.name);
            mat[0] = smile_face;
            mesh_renderer.materials = mat;
            //myAnimationController.SetBool("headrotate", true);
        }
        else
        {
            mat[0] = normal_face;
            mesh_renderer.materials = mat;
            //myAnimationController.SetBool("headrotate", false);
        }
    }

    public void GazingUpon()
    {
        isGazingUpon = true;
    }

    public void NotGazingUpon()
    {
        isGazingUpon = false;
    }
}