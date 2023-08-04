using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string name;
    public List<string> steps = new();
    public List<bool> completedSteps = new();

    void Start()
    {
        // Initialize the completedSteps list with the same number of elements as the steps list
        completedSteps = new List<bool>(new bool[steps.Count]);
    }

    public void CompleteStep(int stepIndex)
    {
        // Mark the specified step as completed
        completedSteps[stepIndex] = true;
    }

    public bool IsRecipeCompleted()
    {
        // Check if all steps have been completed
        foreach (bool stepCompleted in completedSteps)
        {
            if (!stepCompleted)
            {
                return false;
            }
        }
        return true;
    }
}
