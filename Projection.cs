using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Projection : MonoBehaviour
{
    Scene _simulationScene;
    PhysicsScene _physicsScene;
    [SerializeField]
    private Transform _obstacleParent;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private int maxPhysicsFrameIterations;
    void Start()
    {
        createPhysicsScene();
    }

   
    public void createPhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation",new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();
        foreach (Transform item in _obstacleParent)
        {
            var ghostObj = Instantiate(item.gameObject,item.position,item.rotation);
            item.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);


            
        }
    }

    public void simulateTrajectory(Ball ballPrefab,Vector3 pos,Vector3 velocity)
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity );
       
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simulationScene);

        ghostObj.Init(velocity, true);

        _lineRenderer.positionCount = maxPhysicsFrameIterations;

        for (int i = 0; i < maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }
}
