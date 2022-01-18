using Joystick;
using ObjectSystem.ObjectBase.Interfaces;
using Spawners.Scripts;
using UnityEngine;

namespace Base
{
    public static class PoolDelegates
    {
        public delegate  PositionData GetPositionData(int index);
        public delegate  int GetLengthData();
        public delegate void ReleasePoolObjectObstacles(IPoolObject poolObject);
    }
    public static class PlayerEvents
    {
        public delegate int GetScore(int score);
        public delegate void Move(Vector3 dir);
        public delegate void OnPlayerHealthReceived(int health);
    }
    public static class GeneralEvents
    {
        public delegate Transform GetTarget(Transform target);
        public delegate Color GetRandomColor();
        public delegate void OnPauseStateChanged(bool pause);
        public delegate void MarkedForDie(bool state);
        public delegate Joystick.Scripts.Joystick GetJoystick(Joystick.Scripts.Joystick joystick);
    }
    public static class ScreenStateDelegates
    {
        public delegate void Play();
        public delegate void Die();
        public delegate void Pause();
        public delegate void Paused();
    }
    public static class LoadScenes
    {
        public delegate void Restart();
        public delegate void GoToMainMenu();
    }
}
