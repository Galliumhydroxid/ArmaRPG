using UnityEngine;

namespace Flocking
{
    public class HunterParameters : MonoBehaviour

    {
        public  Transform lastKnownPosition;
        public string HerdTag;
        public VisibilityManager visManager;
    }
}