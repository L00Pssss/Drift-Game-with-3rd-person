
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _position;


    public Transform[] Position => _position;

}
