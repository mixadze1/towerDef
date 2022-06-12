using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameObjectFactory : ScriptableObject
    {
    //private Scene _scene;
    protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
    {
        T instance = Instantiate(prefab);     
        return instance;
    }
    }
/*  if(!_scene.isLoaded)
       {

               if(!_scene.isLoaded)
               {
                   _scene = SceneManager.CreateScene(name);
               }

           else
           {
               _scene = SceneManager.CreateScene(name);
           }
       }*/
//SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);