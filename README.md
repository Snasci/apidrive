# API Drive

Reference implemention for a massively scalable API-based distributed harddrive a bit similar to a cross between bittorrent and HDFS.

API Drive exposes two public endpoints, store and retrieve. Designed to store/retrieve a 64KB chunk of arbitrary data as a BSON byte array, this arrangement allows millions of machines to be clustered together to produce a massively scalable distributed harddrive.

The general principle is that rather than storing an individual file on a server, only a few chunks are stored, the rest are store on any number of similar servers. The addresses, chunk GUIDs and order of reconstruction are stored in a file, much similar to a .bittorrent file.

By processing the file, hundreds (or more) of connections can be to download chunks in parrallel greatly improving download speeds. In addition, as no one knows what a chunk is, there is deniability in the off-chance that illegal content is uploaded. Further, there is a very high probability that the chunk itself will not contain anything that is illegal in itself.

This project is a very basic bare bones implementation, without any whistles or bells (or even error handling), but its enough to get the point. You will need to write the calling code for the API, just look at the contracts and use JSON/BSON.

The source is available under numerous licenses:

Apache - http://www.apache.org/licenses/LICENSE-2.0
BSD 3-Clause - https://opensource.org/licenses/BSD-3-Clause
BSD 2-Clause - https://opensource.org/licenses/BSD-2-Clause
WTFPL - http://www.wtfpl.net/about/
MIT - https://opensource.org/licenses/MIT
LGPL v3.0 - http://www.gnu.org/licenses/lgpl-3.0.en.html
LGPL v2.1 - http://www.gnu.org/licenses/old-licenses/lgpl-2.1.en.html
GPL v3.0 - http://www.gnu.org/licenses/gpl-3.0.en.html
GPL v2.0 - http://www.gnu.org/licenses/old-licenses/gpl-2.0.en.html 

Frequently Asked Questions

Is there any authentication?

No. API Drive is intended to be an open platform. The only restriction should be the number of chunks from a given IP address within a defined period.

How scalable is this?

The defining limit is how many URLs your personal machine can hold in memory. If we had 1 million servers, each exposing 1TB of storage (physical/DB) that would provide an Exabyte scale distributed harddrive.

How do we manage space?

Typically a chunk has a lifetime of 30 days. If it is not downloaded within that period, it is deleted. If it is downloaded, it is given an additional 30 days. An additional API should be exposed revealing the current space (or slots) available.

How do we backup chunks?

All chunks are submitted to at least 3 servers to ensure no lost data. The network itself may communicate information about lost chunks and self-heal.

How do we get a list of servers?

This is a management protocol that is not defined in the reference code. There are a number of ways to resolve this. The first is that each server carries a full list of public servers and this is maintained by communication between servers. The second is a partial list within a defined zone (say 100 servers), with a number of links outside that zone to enable client-side spidering of the network. To reduce network load, the former option is recommended.

What do apidrive files look like?

These files are simple JSON array that lists each chunk. Each chunk contains the Id, the order it appears in the reconstructed file and several links to locate the chunk. Here is a quick example:

{
  "MyFile": [
    {
      "Id": "4D3158B3-6FD2-473E-8648-1D8B3D0E3987",
      "Order": 0,
      "URL1": "https://myserver.com/api/drive/retrieve",
      "URL2": "https://myserver1.com/api/drive/retrieve",
      "URL3": "https://myserver2.com/api/drive/retrieve"
    },
    {
      "Id": "2CCB2696-A029-41D7-9C5A-1B60135C328B",
      "Order": 1,
      "URL1": "https://myserver.com/api/drive/retrieve",
      "URL2": "https://myserver1.com/api/drive/retrieve",
      "URL3": "https://myserver2.com/api/drive/retrieve"
    }
  ]
}



How does the client software choose which URL to download from?

Its random and obviously based on availability. There may be also an extra API to inform software of the current load on the server, allowing it to select an alternative location.
