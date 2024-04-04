using System.Collections.Generic;
using UnityEngine;
using Utilities;

//qqq2 in further implementation, aim is to populate the scene using a SceneSetUp config SO
// and along with the DI framework.

namespace GamePlay.Environment
{
    public class EnvironmentSetUp : MonoSingleton<EnvironmentSetUp>
    {
        public List<Transform> teamTransforms;
    }
    
}
