using UnityEngine;

namespace Lance
{
    [CreateAssetMenu(fileName = "QuestInfo", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
    public class QuestInfoSO : ScriptableObject
    {
        [SerializeField] private string id;

        public string Id => id;

        [Header("General")]
        public string displayName;

        [Header("Requirements")]
        public int levelRequirement;
        public QuestInfoSO[] questPrerequisites;

        [Header("Steps")]
        public GameObject[] questStepPrefabs;

        [Header("Rewards")]
        public int elementsReward;
        public int stonesReward;

        // Ensure the Id is always in the Scriptable Object
        private void OnValidate()
        {
            id = name;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
