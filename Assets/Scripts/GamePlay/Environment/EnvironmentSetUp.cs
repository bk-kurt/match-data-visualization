using System.Collections.Generic;
using UnityEngine;
using Utilities;

// in further implementation, aim is to set up scene using a SceneSetUp config SO
// and along with a custom dependency wrapper / or use a DI framework like zenject.

namespace GamePlay.Environment
{
    public class EnvironmentSetUp : MonoSingleton<EnvironmentSetUp>
    {
        public List<Transform> teamTransforms;
    }
    
}
