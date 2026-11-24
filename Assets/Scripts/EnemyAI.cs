using UnityEngine;
using System.IO;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private NeuralNetwork brain;
    private EnemyController controller;
    private EnemyHealth health;

    [Header("AI Settings")]
    public float decisionInterval = 1.5f; 
    private float nextDecisionTime;
    private int lastAction = -1;

    void Start()
    {
        controller = GetComponent<EnemyController>();
        health = GetComponent<EnemyHealth>();
        
        brain = new NeuralNetwork();
        brain.Initialize(); // FIX: Initialize the brain properly here

        // Check if we chose Pre-Trained in the Main Menu
        if (PlayerPrefs.GetInt("UsePreTrainedAI", 0) == 1)
        {
            LoadBrain(); 
        }
        else
        {
            Debug.Log("<color=yellow>AI Starting Untrained (Random)</color>");
        }

        nextDecisionTime = Time.time + decisionInterval;
    }

    void Update()
    {
        if (player == null) return;

        // Press K to save the brain while testing
        if (Input.GetKeyDown(KeyCode.K)) SaveBrain();

        if (Time.time >= nextDecisionTime)
        {
            MakeDecision();
            nextDecisionTime = Time.time + decisionInterval;
        }
    }

    void MakeDecision()
    {
        float distance = Vector3.Distance(transform.position, player.position) / 50f; 
        float myHealth = health.currentHealth / 100f;
        
        float[] inputs = new float[] { distance, myHealth, 1f }; 

        float[] outputs = brain.ForwardPass(inputs);

        int bestAction = 0;
        float highestScore = outputs[0];
        for (int i = 1; i < outputs.Length; i++)
        {
            if (outputs[i] > highestScore)
            {
                highestScore = outputs[i];
                bestAction = i;
            }
        }

        lastAction = bestAction;

        switch (bestAction)
        {
            case 0: controller.Shoot(); break;
            case 1: controller.MoveToPlayer(); break;
            case 2: controller.Dodge(); break;
        }

        brain.Learn(inputs, lastAction, -0.1f); // Small penalty for existing
    }

    public void ReceiveReward(float reward)
    {
        if (lastAction != -1)
        {
            float distance = Vector3.Distance(transform.position, player.position) / 50f;
            float myHealth = health.currentHealth / 100f;
            float[] inputs = new float[] { distance, myHealth, 1f };
            brain.Learn(inputs, lastAction, reward);
        }
    }

    void SaveBrain()
    {
        float[] flatWeights = brain.FlattenWeights();
        string json = JsonUtility.ToJson(new FloatArrayWrapper(flatWeights));
        File.WriteAllText(Application.dataPath + "/AIBrain.json", json);
        Debug.Log("<color=green>AI Brain Saved to /AIBrain.json</color>");
    }
    
    void LoadBrain()
    {
        string path = Application.dataPath + "/AIBrain.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            FloatArrayWrapper wrapper = JsonUtility.FromJson<FloatArrayWrapper>(json);
            brain.LoadWeights(wrapper.values);
            Debug.Log("<color=green>AI Starting Pre-Trained from JSON</color>");
        }
        else
        {
            Debug.Log("<color=yellow>No JSON file found. Starting Untrained.</color>");
        }
    }

    [System.Serializable]
    private class FloatArrayWrapper { public float[] values; public FloatArrayWrapper(float[] v) { values = v; } }
}