using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.Events
{
    public class GrandfatherClock : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        private int hour;

        // Start is called before the first frame update
        void Start()
        {
            hour = GameManagerV2.Instance.hour;
        }

        // Update is called once per frame
        void Update()
        {
            if (hour != GameManagerV2.Instance.hour)
            {
                audioSource.Play();
                hour = GameManagerV2.Instance.hour;
            }
        }
    }
}