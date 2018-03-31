using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node {
    public abstract Status Tick(float deltaTime);
    [SerializeField]
    public enum Status {
        Success,
        Failed,
        Running
    }
    public object[] nextparams;
    public void Initialize(GameObject g, params object[] parameters) {

    }


}
[Serializable]
public class Sequence : Node {

    public override Status Tick(float deltaTime) {
        //foreach (var child in children) {
            //Status childStatus = child.Tick(deltaTime);
            //if (childStatus != Status.Success) {
              //  return childStatus;
            //}
        //}
        return Status.Success;
    }
}
public class LollygagAction : Node {
    GameObject gameObject;
    Block path;
    [SerializeField]
    float timer = 0;


    public override Status Tick(float deltaTime) {
        timer += Time.deltaTime;
        if (timer >= 1f) {
            gameObject.transform.Translate(Vector3.forward);
            timer = 0;
        }
        return Status.Running;
    }
}
public class PathfindingAction : Node {
    Vector3 start, end;

    public new void Initialize(GameObject g, params object[] parameters) {
        start = (Vector3)parameters[0];
        end = (Vector3)parameters[1];








    }



    public override Status Tick(float deltaTime) {
        throw new NotImplementedException();
    }
}
