using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIDrive.Models
{
    public sealed class InMemoryDB
    {
        private static readonly InMemoryDB instance = new InMemoryDB();
        private List<Chunk> drive = new List<Chunk>();

        private InMemoryDB() { }

        public static InMemoryDB Instance { get { return instance; } }

        public Guid AddChunk(Chunk chunk)
        {
            chunk.Id = Guid.NewGuid();
            drive.Add(chunk);

            return chunk.Id;
        }

        public Chunk GetChunk(Guid Id)
        {
            return drive.Where(x => x.Id == Id).Single();
        }
    }
}