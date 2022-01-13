using CorePlugin.Cross.Events.Interface;

namespace Input.Scripts
{
    public interface ICrossInput : IEventHandler, IEventSubscriber
    {
        public void Execute();
    }
}