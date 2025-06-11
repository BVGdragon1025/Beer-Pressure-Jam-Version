using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolerType
{
    SFX,
    BALL,
    AI_BALL,
}

public class ObjectPuller : MonoBehaviour
{
    [Header("Object Prefabs Section")]
    [SerializeField]
    private GameObject _specialEffectsObject;
    [SerializeField]
    private GameObject _ballObject;

    [Header("Object Pools Section")]
    [SerializeField]
    private List<GameObject> _specialEffectsPool;
    [SerializeField]
    private List<GameObject> _ballPool;
    [SerializeField]
    private List<GameObject> _aiBallPool;

    [Header("Main Section")]
    [SerializeField]
    private int _maxSFXPooledObjects;
    [SerializeField]
    private int _maxBallObjects;
    public int MaxSFXPooledObjects { get { return _maxSFXPooledObjects; } }

    private GameObject GetObjectToPool(ObjectPoolerType poolType)
    {
        switch (poolType)
        {
            case ObjectPoolerType.SFX:
                return _specialEffectsObject;
            case ObjectPoolerType.BALL:
            case ObjectPoolerType.AI_BALL:
                return _ballObject;
            default:
                Debug.LogError("This pool type is not defined in switch statement!");
                return null;

        }
    }

    private int GetAmountToPool(ObjectPoolerType poolType)
    {
        return poolType switch
        {
            ObjectPoolerType.SFX => _maxSFXPooledObjects,
            ObjectPoolerType.BALL => _maxBallObjects,
            ObjectPoolerType.AI_BALL => _maxBallObjects,
            _ => 0,
        };
    }

    private List<GameObject> GetListToPool(ObjectPoolerType poolType)
    {
        return poolType switch
        {
            ObjectPoolerType.SFX => _specialEffectsPool,
            ObjectPoolerType.BALL => _ballPool,
            ObjectPoolerType.AI_BALL => _aiBallPool,
            _ => null,
        };
    }

    private void SetObjectPool(ObjectPoolerType poolType)
    {
        GameObject temp;

        for(int i = 0; i < GetAmountToPool(poolType); i++)
        {
            temp = Instantiate(GetObjectToPool(poolType));
            GetListToPool(poolType).Add(temp);
            GetListToPool(poolType)[i].SetActive(false);
        }
    }

    public GameObject GetObjectPool(ObjectPoolerType poolType)
    {
        for(int i = 0; i < GetAmountToPool(poolType); i++)
        {
            if (!GetListToPool(poolType)[i].activeInHierarchy)
            {
                return GetListToPool(poolType)[i];
            }

        }
        return null;
    }

    public void ChangeBallMaterial(Material material)
    {
        foreach(var ball in _ballPool)
        {
            ball.GetComponent<Renderer>().material = material;
        }
    }

    public void ChangeExplosionEffect(GameObject explosionEffect)
    {
        _specialEffectsObject = explosionEffect;
        foreach(var explosion in  _specialEffectsPool)
        {
            Destroy(explosion);
        }
        _specialEffectsPool.Clear();
        SetObjectPool(ObjectPoolerType.SFX);
        _specialEffectsPool.TrimExcess();
    }

    public void PreparePools()
    {
        SetObjectPool(ObjectPoolerType.SFX);
        SetObjectPool(ObjectPoolerType.BALL);
        SetObjectPool(ObjectPoolerType.AI_BALL);
    }

}
