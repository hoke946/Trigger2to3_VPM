
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Trigger2to3
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class T23_AudioBank : UdonSharpBehaviour
    {
        public AudioSource source;

        public int playbackOrder;

        public int playbackStyle;

        public bool repeat;

        [Range(-3, 3)]
        public float minPitchRange = 1;

        [Range(-3, 3)]
        public float maxPitchRange = 1;

        public GameObject onPlay;
        public string onPlayCustomName;

        public GameObject onStop;
        public string onStopCustomName;

        public GameObject onChange;
        public string onChangeCustomName;

        public AudioClip[] clips;

        [HideInInspector]
        public int[] order;

        [HideInInspector]
        public int currentIndex = 0;
        private bool isPlaying = false;
        private int seed = 0;
        private int actionCount = 0;

        void Start()
        {
            seed = Random.Range(0, 1000000000);
            currentIndex = -1;
            if (playbackOrder == 1)
            {
                order = new int[clips.Length];
                for (int i = 0; i < order.Length; i++)
                {
                    order[i] = order.Length - 1 - i;
                }
            }
            else if (playbackOrder == 2)
            {
                Shuffle();
            }
            else
            {
                order = new int[clips.Length];
                for (int i = 0; i < order.Length; i++)
                {
                    order[i] = i;
                }
            }
        }

        public void SetInitialSeed(int _seed)
        {
            seed = _seed;
        }

        public void Play(int index)
        {
            if (clips.Length <= index) { return; }
            currentIndex = index;
            Play_inner();

            SendOnPlayTrigger();
        }

        private void Play_inner()
        {
            if (!source) { return; }

            actionCount++;
            if (source.isPlaying) { source.Stop(); }
            source.clip = clips[order[currentIndex]];
            if (minPitchRange >= maxPitchRange)
            {
                source.pitch = minPitchRange;
            }
            else
            {
                Random.InitState(seed + actionCount);
                source.pitch = Random.Range(minPitchRange, maxPitchRange);
            }
            source.Play();
            isPlaying = true;
        }

        public void PlayNext()
        {
            PlayNext_inner();
            SendOnPlayTrigger();
        }

        private void PlayNext_inner()
        {
            if (playbackOrder == 3)
            {
                Random.InitState(seed + actionCount);
                currentIndex = Random.Range(0, clips.Length);
            }
            else
            {
                if (currentIndex == clips.Length - 1)
                {
                    if (repeat)
                    {
                        currentIndex = 0;
                    }
                    else
                    {
                        Stop();
                        return;
                    }
                }
                else
                {
                    currentIndex++;
                }
            }
            Play_inner();
        }

        public void Stop()
        {
            if (!source) { return; }

            if (source.isPlaying)
            {
                source.Stop();
                isPlaying = false;
                SendOnStopTrigger();
            }
        }

        void Update()
        {
            if (!source) { return; }
            if (isPlaying && !source.isPlaying)
            {
                if (playbackStyle == 0)
                {
                    isPlaying = false;
                    SendOnStopTrigger();
                }
                else
                {
                    PlayNext_inner();
                    SendOnChangeTrigger();
                }
            }
        }

        public void Shuffle()
        {
            int[] lottery = new int[clips.Length];
            for (int i = 0; i < lottery.Length; i++)
            {
                lottery[i] = i;
            }
            order = new int[clips.Length];
            actionCount++;
            Random.InitState(seed + actionCount);
            Debug.Log(seed + actionCount);
            for (int i = 0; i < lottery.Length; i++)
            {
                int result = -1;
                while (result == -1)
                {
                    int idx = Random.Range(0, lottery.Length);
                    result = lottery[idx];
                    lottery[idx] = -1;
                }
                order[i] = result;
            }
        }

        private void SendOnPlayTrigger()
        {
            if (onPlay && onPlayCustomName != "")
            {
                T23_CustomTrigger[] customTriggers = onPlay.GetComponents<T23_CustomTrigger>();
                for (int i = 0; i < customTriggers.Length; i++)
                {
                    if (customTriggers[i].Name == onPlayCustomName)
                    {
                        customTriggers[i].Trigger();
                    }
                }
            }
        }

        private void SendOnStopTrigger()
        {
            if (onStop && onStopCustomName != "")
            {
                T23_CustomTrigger[] customTriggers = onStop.GetComponents<T23_CustomTrigger>();
                for (int i = 0; i < customTriggers.Length; i++)
                {
                    if (customTriggers[i].Name == onStopCustomName)
                    {
                        customTriggers[i].Trigger();
                    }
                }
            }
        }

        private void SendOnChangeTrigger()
        {
            if (onChange && onChangeCustomName != "")
            {
                T23_CustomTrigger[] customTriggers = onChange.GetComponents<T23_CustomTrigger>();
                for (int i = 0; i < customTriggers.Length; i++)
                {
                    if (customTriggers[i].Name == onChangeCustomName)
                    {
                        customTriggers[i].Trigger();
                    }
                }
            }
        }
    }
}
