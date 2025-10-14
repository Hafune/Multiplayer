using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;



public class DynamicCodeExecutor
{

    
    public static object Execute()
    {
        Debug.Log("Test");
        return "Execution finished successfully.";
    }
}