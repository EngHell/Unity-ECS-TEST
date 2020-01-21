using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Components
{
    public class InputSingleton : IComponentData
    {
        public float horizontal;
        public float vertical;
        public bool fire1;
        public bool fire2;
        public bool fire3;
    }

}
