using UnityEngine;

namespace _Project.Dev.Runtime.Utilities.ConfigsManagment
{
 [CreateAssetMenu(fileName = "New Config", menuName = "Test")]
 public class TestConfig : ScriptableObject
 {
  [field: SerializeField] public int Damage { get; private set; }
 }
}
