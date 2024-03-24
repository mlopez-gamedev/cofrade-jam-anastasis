#if !ODIN_INSPECTOR_EDITOR_ONLY
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiguelGameDev
{
    public class SerializedWorldActor : SerializedMonoBehaviour
    {
        public Transform Transform => transform; // Unity caches transform
    }
}
#endif