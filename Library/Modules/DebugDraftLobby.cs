using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Modules
{
    class DebugDraftLobby : IBaseModule
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            CreateLobby();
        }

        private void CreateLobby()
        {

        }
    }
}
