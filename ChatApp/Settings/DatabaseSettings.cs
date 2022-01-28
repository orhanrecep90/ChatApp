using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Settings
{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string DatabaseName { get; set; }
        public string GroupCollectionName { get; set; }
        public string MessageCollectionName { get; set; }
        public string ConnectionString { get; set; }
    }
}
