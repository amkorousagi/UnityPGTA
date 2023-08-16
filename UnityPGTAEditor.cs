using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Actuators;
using Unity.MLAgents;
using UnityEngine.InputSystem;
using System.Reflection;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using static System.Net.Mime.MediaTypeNames;
//using System.Diagnostics;
//using System.Diagnostics;

//[CustomEditor(typeof(UnityPGTA))]
[CanEditMultipleObjects]
public class UnityPGTAEditor : Editor
{
    SerializedProperty unityPGTA;

    [MenuItem("UnityPGTA/Analyze Game and Custimze Agent")]
    static void ExecuteCode()
    {
        // Analze game
        Debug.Log("Analyze Game and Custimze Agent");

        // Player game object has UnityPGTA component
        UnityPGTA unityPGTA = FindObjectOfType<UnityPGTA>();

        // If no BehaviorParameters, add it
        GameObject player = unityPGTA.gameObject;
        if (player.GetComponent<BehaviorParameters>() == null)
        {
            player.AddComponent<BehaviorParameters>();
        }
        // If no DecisionRequester, add it
        if (player.GetComponent<DecisionRequester>() == null) {
            player.AddComponent<DecisionRequester>();
        }

        // Get all scripts in Player object
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();

        // Get actions
        List<WindowsInput.Native.VirtualKeyCode> actions = new List<WindowsInput.Native.VirtualKeyCode>();
        int actionCnt = 0;

        string assetsFolderPath = "Assets/Scripts"; //UnityEngine.Application.dataPath;
        List<string> csFilePaths = GetAllCSFilePaths(assetsFolderPath);

        foreach (string scriptFilePath in csFilePaths)
        {
            
            string scriptContent = File.ReadAllText(scriptFilePath);

            MatchCollection matches = Regex.Matches(scriptContent, @"KeyCode\.(\w+)");
            foreach (Match match in matches)
            {
                Debug.Log("keycode "+ match.Groups[1].Value);
                //중복없이 세렸다는 가정하에
                
                KeyCode parsedKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), match.Groups[1].Value);
                if (parsedKeyCode == KeyCode.Escape) continue;
                if (!actions.Contains(ConvertKeyCode2VK.ConvertK2VK(parsedKeyCode)))
                {
                    actionCnt++;
                    actions.Add(ConvertKeyCode2VK.ConvertK2VK(parsedKeyCode));
                }

            }

            matches = Regex.Matches(scriptContent, @"GetAxis(Raw)?\s*\(\s*""Horizontal""\s*\)");
            foreach (Match match in matches)
            {
                //Debug.Log(" Horizontal" + GetButtonKeyCode("Horizontal", "right"));
                //중복없이 세렸다는 가정하에

                if (!actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_D))
                {
                    actionCnt++;
                    actions.Add(WindowsInput.Native.VirtualKeyCode.VK_D);
                }
                if (!actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_A))
                 {
                    actionCnt++;
                    actions.Add(WindowsInput.Native.VirtualKeyCode.VK_A);
                }

            }

            matches = Regex.Matches(scriptContent, @"GetAxis(Raw)?\s*\(\s*""Vertical""\s*\)");
            foreach (Match match in matches)
            {
                if (!actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_W))
                {
                    actionCnt++;
                    actions.Add(WindowsInput.Native.VirtualKeyCode.VK_W);
                }
                if (!actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_S))
                 {
                    actionCnt++;
                    actions.Add(WindowsInput.Native.VirtualKeyCode.VK_S);
                }

            }



        }
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        if(playerInput != null)
        {
            InputActionAsset inputActionAsset = playerInput.actions;
            foreach (InputActionMap actionMap in inputActionAsset.actionMaps)
            {
                foreach (InputAction action2 in actionMap.actions)
                {
                    //foreach (InputBinding binding in action2.bindings)
                    //{
                    //    Debug.Log(binding.ToDisplayString());
                        
                    //}
                    foreach(InputControl inputControl in action2.controls)
                    {
                        if (inputControl is KeyControl keyControl)
                        {
                            Debug.Log("keycode " + keyControl.keyCode);
                            if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) != WindowsInput.Native.VirtualKeyCode.ESCAPE)
                            {
                                if (!actions.Contains(ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode)))
                                {
                                    if(ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode)==WindowsInput.Native.VirtualKeyCode.VK_W && actions.Contains(WindowsInput.Native.VirtualKeyCode.UP))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.VK_D && actions.Contains(WindowsInput.Native.VirtualKeyCode.RIGHT))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.VK_S && actions.Contains(WindowsInput.Native.VirtualKeyCode.DOWN))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.VK_A && actions.Contains(WindowsInput.Native.VirtualKeyCode.LEFT))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.UP && actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_W))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.RIGHT && actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_D))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.DOWN && actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_S))
                                    {
                                        continue;
                                    }
                                    if (ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode) == WindowsInput.Native.VirtualKeyCode.LEFT && actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_A))
                                    {
                                        continue;
                                    }
                                    actionCnt++;
                                    actions.Add(ConvertInputSystemKey2VK.ConvertK2VK(keyControl.keyCode));
                                }
                            }
                        }
                    }
                }
            }
        }
        //var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
        //SerializedObject o = new SerializedObject(inputManager);
        //SerializedProperty axisArray = o.FindProperty("m_Axes");
        //if (axisArray.arraySize == 0)
        //    Debug.Log("No Axes");

        //for (int i = 0; i < axisArray.arraySize; ++i)
        //{
        //    var axis = axisArray.GetArrayElementAtIndex(i);
        //    var name = axis.displayName;  //axis.displayName  "Horizontal"	string
        //    axis.Next(true);        //axis.displayName	    "Name"	string
        //    axis.Next(false);   //axis.displayName	"Descriptive Name"	string
        //    axis.Next(false);   //axis.displayName	"Descriptive Negative Name"	string
        //    axis.Next(false);   //axis.displayName	"Negative Button"	string
        //    axis.Next(false);   //axis.displayName	"Positive Button"	string
        //    var value = axis.stringValue;  //"right"

        //    Debug.Log(name + " | " + value);
        //}

        // Set actions
        BehaviorParameters bp = player.GetComponent<BehaviorParameters>();





        int ContinuousCnt = 0;
        if (actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_A)&& actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_D))
        {
            if (actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_W) && actions.Contains(WindowsInput.Native.VirtualKeyCode.VK_S))
            {
                actionCnt -= 4;
                ContinuousCnt = 2;

                int[] action = new int[actionCnt];
                for (int i = 0; i < actionCnt; i++)
                {
                    action[i] = 2;
                }
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_W);
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_S);
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_A);
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_D);
                ActionSpec hybridActionSpec = new ActionSpec(ContinuousCnt, action);
                bp.BrainParameters.ActionSpec = hybridActionSpec;
                unityPGTA.horizontal = true;
                unityPGTA.vertical = true;
            }
            else
            {
                actionCnt -= 2;
                ContinuousCnt = 1;
                int[] action = new int[actionCnt];
                for (int i = 0; i < actionCnt; i++)
                {
                    action[i] = 2;
                }
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_A);
                actions.Remove(WindowsInput.Native.VirtualKeyCode.VK_D);
                ActionSpec hybridActionSpec = new ActionSpec(ContinuousCnt, action);
                bp.BrainParameters.ActionSpec = hybridActionSpec;
                unityPGTA.horizontal = true;
            }
        }
        else
        {
            int[] action = new int[actionCnt];
            for (int i = 0; i < actionCnt; i++)
            {
                action[i] = 2;
            }
            ActionSpec hybridActionSpec = new ActionSpec(ContinuousCnt, action);
            bp.BrainParameters.ActionSpec = hybridActionSpec;
        }

        // scene에 observation을 추출
        // 나의 위치, 속력, 다른 오브젝트와 거리

        // Get observations
        Collider[] colliders = GameObject.FindObjectsOfType<Collider>();
        Rigidbody[] rigidbodies = GameObject.FindObjectsOfType<Rigidbody>();

        HashSet<GameObject> uniqueObjects = new HashSet<GameObject>();

        // Add colliders' GameObjects to the HashSet
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != player)
            {
                uniqueObjects.Add(collider.gameObject);
            }
        }

        // Add rigidbodies' GameObjects to the HashSet
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject != player)
            {
                uniqueObjects.Add(rigidbody.gameObject);
            }

        }
        GameObject[] observations;
        observations = new List<GameObject>(uniqueObjects).ToArray(); // Already included Player
        foreach (GameObject obj in observations)
        {
            Debug.Log(obj);
        }

        // Set observations
        bp.BrainParameters.VectorObservationSize = observations.Length + 3;



        // Customzie agent
        unityPGTA.actions = actions;
    }
    public static GameObject[] UpdateObservation()
    {
        UnityPGTA unityPGTA = FindObjectOfType<UnityPGTA>();

        GameObject player = unityPGTA.gameObject;
        // scene에 observation을 추출
        // 나의 위치, 속력, 다른 오브젝트와 거리

        // Get observations
        Collider[] colliders = GameObject.FindObjectsOfType<Collider>();
        Rigidbody[] rigidbodies = GameObject.FindObjectsOfType<Rigidbody>();

        HashSet<GameObject> uniqueObjects = new HashSet<GameObject>();

        // Add colliders' GameObjects to the HashSet
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != player)
            {
                uniqueObjects.Add(collider.gameObject);
            }
        }

        // Add rigidbodies' GameObjects to the HashSet
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject != player)
            {
                uniqueObjects.Add(rigidbody.gameObject);
            }

        }
        GameObject[] observations;
        observations = new List<GameObject>(uniqueObjects).ToArray(); // Already included Player
        foreach (GameObject obj in observations)
        {
            Debug.Log(obj);
        }
        return observations;
    }
    private static List<string> GetAllCSFilePaths(string directory)
    {
        List<string> csFilePaths = new List<string>();
        string[] files = Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            csFilePaths.Add(file);
        }

        return csFilePaths;
    }
    private static KeyCode GetButtonKeyCode(string axisName, string buttonName)
    {
        string buttonPositive = axisName + "_" + buttonName;

        KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), buttonPositive, true);

        return keyCode;
    }
}
