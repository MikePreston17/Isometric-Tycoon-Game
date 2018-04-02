using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Guest")]
public class Guest : ScriptableObject {
    [NonSerialized]
    int age = 50;
    GameObject gameObject;

    public void Awake () {
        Debug.Log("I'm awake");
        // TODO: Instanatiate the body of the guest
    }
}
