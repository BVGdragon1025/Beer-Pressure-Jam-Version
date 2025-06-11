using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryProjection : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _line;
    [SerializeField]
    private int _maxPhysicsFrameIterations = 100;
    [SerializeField]
    private List<Transform> _obstacles;

    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private readonly Dictionary<Transform, Transform> _spawnedObjects = new Dictionary<Transform, Transform>();

    [Header("Ghost Ball variables")]
    [SerializeField]
    private GameObject _trajectoryBall;
    private BallController _trajectoryBallController;
    private Rigidbody _trajectoryBallRb;

    private void Awake()
    {
        CreatePhysicsScene();
    }

    void Update()
    {
        foreach (var item in _spawnedObjects)
        {
            item.Value.SetPositionAndRotation(item.Key.position, item.Key.rotation);
        }
    }

    private void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("TrajectorySimulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        foreach (Transform obj in _obstacles)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            if(ghostObj.transform.childCount > 0)
            {
                foreach(Transform child in ghostObj.transform)
                {
                    if(child.gameObject.TryGetComponent(out Renderer renderer))
                    {
                        renderer.enabled = false;
                    }
                }
            }
            SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            if (!ghostObj.isStatic)
            {
                _spawnedObjects.Add(obj, ghostObj.transform);
            }
        }

    }

    public void CreateGhostBall(GameObject ballPrefab, Vector3 position)
    {
        Debug.Log("Creating ghost ball");
        _trajectoryBall = Instantiate(ballPrefab, position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(_trajectoryBall, _simulationScene);
        _trajectoryBallController = _trajectoryBall.GetComponent<BallController>();
        _trajectoryBallRb = _trajectoryBall.GetComponent<Rigidbody>();
        _trajectoryBallController.IsGhost = true;
        _trajectoryBall.GetComponent<MeshRenderer>().enabled = false;
        _trajectoryBall.GetComponent<TrailRenderer>().enabled = false;
    }

    public void SimulateTrajectory(GameObject ballPrefab, Vector3 position, float velocity)
    {
        if(_trajectoryBall == null)
            CreateGhostBall(ballPrefab, position);

        _trajectoryBallRb.AddForce(velocity * (-transform.forward + new Vector3(0, 0.7f, 0)), ForceMode.Impulse);
        _trajectoryBallController.IsGhost = true;

        _line.positionCount = _maxPhysicsFrameIterations;

        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(i, _trajectoryBall.transform.position);
        }

        _trajectoryBall.transform.position = position;
        _trajectoryBallRb.velocity = Vector3.zero;

    }

    public void EnableGhostBall()
    {
        if(_trajectoryBall != null)
            _trajectoryBall.SetActive(true);
    }


}
