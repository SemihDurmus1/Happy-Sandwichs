using UnityEngine;

public interface IGrabbable
{
    void Grab(Transform grabPointTransform);
    void Drop();
}
