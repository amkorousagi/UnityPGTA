using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using WindowsInput;
using System.Numerics;
using Unity.MLAgents.Sensors;
using UnityEngine.SceneManagement;
using System.IO;
using JetBrains.Annotations;
using UnityEditor;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using System;

[System.Serializable]
public class Report
{
    public string real_time;
    public float elapsed_time;
    public int unique_error_count=0;
    public List<string> unique_errors;
    public List<string> unique_error_types;
    public List<int> unique_error_steps;
    public int error_count=0;
    public int warning_count = 0;
    public List<string> errors;
    public List<string> error_types;
    public List<int> error_steps;
    public List<string> warnings;
    public List<string> warning_types;
    public List<int> warning_steps;
}

public class UnityPGTA : Agent
{
    
    public List<WindowsInput.Native.VirtualKeyCode> actions;
    public GameObject[] observations;
    public Transform rb;
    public UnityEngine.Vector3 prior;
    public string scriptPath = "Assets/Scripts";
    public float delayInSeconds = 10.0f;
    public bool horizontal = false;
    public bool vertical = false;
    public float threshold = 0.1f;
    public int endStep = 10000;
    private InputSimulator inputSim;
    private float startTime;
    private float elapsedTime;
    private bool ea = false;
    private DateTime startDateTime;


    [SerializeField] private Report report;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        startTime = Time.time;
        startDateTime = DateTime.Now;
    }

    // Start is called before the first frame update
    void Awake()
    {
        ea = false;
        Invoke("EnableAgentBehavior", 10.0f);
        Application.logMessageReceived += HandleLogMessage;
        inputSim = new InputSimulator();
        rb = GetComponent<Transform>();
        

    }
    private void EnableAgentBehavior()
    {
        // Enable agent's behavior here
        // For example, set a flag to indicate the agent can start taking actions
        ea = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (UnityEngine.Vector3.Equals(rb.position, prior))
        {
            AddReward(-1f);
        }
        prior = rb.position;
        elapsedTime = Time.time - startTime;
        // AddReward(-0.001f);
        /*
        if (StepCount > 500)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            //SceneManager.LoadScene(currentSceneName);
            EndEpisode();
        }
        */
        
        if (StepCount > endStep)
        {
            EndEpisode();
            EditorApplication.isPlaying = false;
        }
        
    }
    
    public override void OnActionReceived(ActionBuffers recivedActions)
    {
        if (ea)
        {
            if(horizontal)
            {
                float horizontalInput = recivedActions.ContinuousActions[0];
                if(horizontalInput > threshold)
                {
                    inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_D);
                }else if(horizontalInput < -threshold)
                {
                    inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_A);
                }
                else
                {
                    inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_D);
                    inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_A);
                }
            }
            if(vertical)
            {
                float verticalInput = recivedActions.ContinuousActions[1];
                if (verticalInput > threshold)
                {
                    inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_W);
                }
                else if (verticalInput < -threshold)
                {
                    inputSim.Keyboard.KeyDown(WindowsInput.Native.VirtualKeyCode.VK_S);
                }
                else
                {
                    inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_W);
                    inputSim.Keyboard.KeyUp(WindowsInput.Native.VirtualKeyCode.VK_S);
                }
            }
            
            for (int i = 0; i < actions.Count; i++)
            {
                switch (recivedActions.DiscreteActions[i])
                {
                    case 0:
                        break;
                    case 1:
                        Debug.Log(actions[i]);
                        inputSim.Keyboard.KeyPress(actions[i]);
                        break;
                }
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("collect");
        // player position, velocity
        sensor.AddObservation(rb.position);

        // observations
        foreach(GameObject observation in observations)
        {
            // Debug.Log(observation);
            if (observation != null)
            {
                sensor.AddObservation(UnityEngine.Vector3.Distance(rb.position, observation.transform.position));
            }
        }
       
        //sensor.AddObservation(StepCount/MaxStep);
        /*
        if (StepCount > 500)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            EndEpisode();
            

        }
        */
        //base.CollectObservations(sensor);
    }
    public override void OnEpisodeBegin()
    {
        // init pos, vel, rot of observation = without load scene?
        // init bug
        Debug.Log("ep begin");
        observations = UnityPGTAEditor.UpdateObservation();
        //Debug.Log("Reset");
        //SceneManager.LoadScene("Basic");
        // base.OnEpisodeBegin();
    }
    void HandleLogMessage(string logString, string stackTrace, LogType logType)
    {
        // Check if this is an error message
        if (logType == LogType.Error || logType == LogType.Exception)
        {
            if (report.errors.Contains(stackTrace))
            {
                report.error_count++;
                report.errors.Add(stackTrace);
                report.error_types.Add(logString);
                report.error_steps.Add(StepCount);
                return;
            }else{
                report.error_count++;
                report.errors.Add(stackTrace);
                report.error_types.Add(logString);
                report.error_steps.Add(StepCount);
                AddReward(10.0f);
                Debug.Log("unique error");
                report.unique_error_count++;
                report.unique_errors.Add(stackTrace);
                report.unique_error_types.Add(logString);
                report.unique_error_steps.Add(StepCount);
            }
        }else if(logType == LogType.Warning)
        {
            report.warning_count++;

            report.warnings.Add(stackTrace);
            report.warning_types.Add(logString);
            report.warning_steps.Add(StepCount);
           
        }
        // 처음 찾은 오류면 보상 부여
        // 이미 찾은 오류면 보상 안줌
        // 보상 안줘도 기록은 하기
    }

    private void OnDestroy()
    {
        TimeSpan elapsedDateTime = DateTime.Now - startDateTime;
        report.real_time =  elapsedDateTime.TotalSeconds.ToString("F2");
        report.elapsed_time = elapsedTime;
        Debug.Log("DateTime elapsed: " + elapsedDateTime.TotalSeconds.ToString("F2") + " seconds");
        Debug.Log("Time elapsed: " + elapsedTime.ToString("F2") + " seconds");
        string filePath = Application.dataPath + "/bug_report"+ System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".json";
        // Write your quitting log to the file
        File.WriteAllText(filePath, JsonUtility.ToJson(report));
        Debug.Log("Quitting log written to file: " + filePath);

    }
}
