using Prism.Events;

namespace SecureStringTest
{
    public class Messenger : EventAggregator
    {
        public static Messenger Instance { get; } = new Messenger();
    }
}
