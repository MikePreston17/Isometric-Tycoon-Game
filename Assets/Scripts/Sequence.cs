using System;

[Serializable]
public class Sequence : Node  //todo: Sequence of what?  Name accordingly.
{
    public override Status Tick(float deltaTime)
    {
        //foreach (var child in children) {
        //Status childStatus = child.Tick(deltaTime);
        //if (childStatus != Status.Success) {
        //  return childStatus;
        //}
        //}

        return Status.Success;
    }
}
