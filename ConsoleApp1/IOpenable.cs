using System;
using System.Collections.Generic;
using System.Text;

namespace TeslaPanelBus
{
    interface IOpenable
    {
        Boolean IsOpen();
        void Open();
        void Close();

    }
}
