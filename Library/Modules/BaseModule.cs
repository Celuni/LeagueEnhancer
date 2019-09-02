using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Modules
{
    public class BaseModule
    {
        public BaseModule()
        {
            OnEnable();
        }

        protected virtual void OnEnable()
        {

        }
    }
}
