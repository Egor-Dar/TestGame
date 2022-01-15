using System;
using Base;
using CorePlugin.Cross.Events.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorController.Script
{
    public class Colors : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private Color[] colors;
        private Color GetRandomColor()
        {
            return colors[Random.Range(0,colors.Length)];
        }
        public Delegate[] GetSubscribers()
        {
            return new Delegate[]
            {
                (GeneralEvents.GetRandomColor) GetRandomColor
            };
        }
    }
}
