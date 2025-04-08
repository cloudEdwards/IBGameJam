using UnityEngine;

namespace _Project.Script.Persistent
{
    [CreateAssetMenu(fileName = "Persisten Manager Holder", menuName = "Manager/Persistent Holder", order = 0)]
    public class PersistentManagerHolderSO : ScriptableObject
    {
        private static PersistentManagerHolderSO instance;

        public static PersistentManagerHolderSO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<PersistentManagerHolderSO>("Persisten Manager Holder");
                }

                return instance;
            }
        }
        
        
        public DamageTextManagerSO damageTextManager;
        public SoundFXManager soundFXManager;
    }
}