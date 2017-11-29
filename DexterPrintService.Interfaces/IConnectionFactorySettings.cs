using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexterPrintService.Interfaces
{
    interface IConnectionFactorySettings
    {
        ConnectionFactory GetConnectionFactory();
    }
}
