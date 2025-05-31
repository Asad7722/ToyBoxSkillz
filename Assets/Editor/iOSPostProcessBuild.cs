//using System.Collections;

//using System.Collections.Generic;

//using UnityEngine;

//using UnityEditor;

//using UnityEditor.Callbacks;

//using UnityEditor.iOS.Xcode;


//public class iOSPostProcessBuild : MonoBehaviour

//{

//    [PostProcessBuild]

//    public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)

//    {

//        const string UIApplicationExitsOnSuspend = "UIApplicationExitsOnSuspend";
//        const string TrackingDescription =
//        "This identifier will be used to deliver personalized ads to you. ";

//        Debug.Log("Build Target: " + buildTarget);

//        Debug.Log("Path: " + pathToBuiltProject);

//        if (buildTarget == BuildTarget.iOS)

//        {

//            string plistPath = pathToBuiltProject + "/Info.plist";

//            PlistDocument plist = new PlistDocument();

//            plist.ReadFromFile(plistPath);


//            PlistElementDict root = plist.root;

//            var rootDic = root.values;

//            rootDic.Remove(UIApplicationExitsOnSuspend);

//            // Set value in plist
//            root.SetString("NSUserTrackingUsageDescription", TrackingDescription);


//            plist.WriteToFile(plistPath);

//        }

//    }

//}