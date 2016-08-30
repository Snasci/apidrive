using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDrive.Models
{
    public class Chunk
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
    }
}