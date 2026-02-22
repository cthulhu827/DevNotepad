using System;

namespace Framework.MVC
{
    public class EventSuppressor
    {
        public bool Suppress { get; private set; }

        public void Exec(Action action)
        {
            Suppress = true;
            try
            {
                action();
            }
            finally
            {
                Suppress = false;
            }
        }
    }
}