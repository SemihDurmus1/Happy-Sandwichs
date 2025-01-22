using UnityEngine;

namespace Grabbing
{
    public interface IGrabbable
    {
        void Grab(Transform grabPointTransform);
        void Drop();
    }



}