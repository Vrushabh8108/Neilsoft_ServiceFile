﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService17" in both code and config file together.
    [ServiceContract]
    public interface ExceptionLogging
    {
        [OperationContract]
        void SendErrorToText(System.Exception ex);
    }
}