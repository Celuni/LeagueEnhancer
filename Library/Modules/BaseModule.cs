using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Modules
{
    public class BaseModule
    {
        public Action Enabled, Disabled;

        public BaseModule()
        {
            Enabled += OnEnable;
            Disabled += OnDisable;

            Enabled?.Invoke();
        }

        protected virtual void OnEnable()
        {
            Console.WriteLine($"Module \"{this.GetType().Name}\" enabled!");
        }
        protected virtual void OnDisable()
        {
            Console.WriteLine($"Module \"{this.GetType().Name}\" disabled!");
        }


        public void Enable()
        {
            Enabled?.Invoke();
        }

        public void Disable()
        {
            Disabled?.Invoke();
        }
    }
}
