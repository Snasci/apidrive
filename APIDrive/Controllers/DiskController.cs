using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIDrive.Contracts;
using APIDrive.Models;


namespace APIDrive.Controllers
{
    public class DiskController : ApiController
    {
        [HttpPost]
        [Route("api/drive/store")]
        public HttpResponseMessage Store([FromBody] DriveStoreContract driveContract)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new DriveResponseContract() { Id = InMemoryDB.Instance.AddChunk(new Chunk() { Data = driveContract.Data }) });
        }

        [HttpPost]
        [Route("api/drive/store")]
        public HttpResponseMessage Store([FromBody] DriveRetrieveContract driveContract)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new DriveResponseContract() { Data = InMemoryDB.Instance.GetChunk(driveContract.Id).Data });
        }
    }
}
