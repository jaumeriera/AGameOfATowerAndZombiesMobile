using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Idealy this should be a singleton
public class Environment : MonoBehaviour
{
    // Loaders for objects 
    [SerializeField]
    LoadAppEnvironment loadApp;

    // public environments loaded
    public AppEnvironment appEnvironment;

    private void Start() {
        appEnvironment = loadApp.LoadAppEnvironmentFromJson();
    }
}
