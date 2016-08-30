using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDrive.Contracts
{
    public class DriveResponseContract
    {
        public Guid? Id { get; set; }
        public byte[] Data { get; set; }
    }
}