using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task=> {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                print("Firebase initialization fail");
            }
            //if (task.Exception != null)
            //{
            //    Debug.LogError("Failed to initialize Firebase with {task.Exception}");
            //    print("Fail to initialize Firebase");
            //    return;
            //}

            //OnFirebaseInitialized.invoke();
        });
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vr-all-presence.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
