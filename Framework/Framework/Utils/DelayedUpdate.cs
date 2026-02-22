using System;

namespace Framework.Utils
{
    public class DelayedUpdate
    {
        private readonly Action action;

        private bool isUpdating;

        private bool isChanged;

        public DelayedUpdate(Action action)
        {
            this.action = action;
        }

        public void BeginUpdate()
        {
            if (isUpdating)
            {
                throw new Exception("Twice called BeginUpdate()");
            }

            isUpdating = true;
        }

        public void EndUpdate()
        {
            if (!isUpdating)
            {
                throw new Exception("BeginUpdate() was not called");
            }

            isUpdating = false;

            if (isChanged)
            {
                action();
                isChanged = false;
            }
        }

        public void Exec()
        {
            if (isUpdating)
            {
                isChanged = true;
            }
            else
            {
                action();
            }
        }
    }
}