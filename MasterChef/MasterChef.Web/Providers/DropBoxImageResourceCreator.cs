using Spring.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MasterChef.Web.Providers
{
    public class DropBoxImageResourceCreator : IResource
    {
        private string urlToWorkWith;
        private byte[] data;
        private MemoryStream stream;

        public DropBoxImageResourceCreator(string url)
        {
            this.urlToWorkWith = url;

            var client = new WebClient(); 

            this.data = client.DownloadData(this.urlToWorkWith);
        }

        public Stream GetStream()
        {
            this.stream = new MemoryStream(this.data);
            return this.stream;
        }

        public bool IsOpen
        {
            get { return !this.stream.CanWrite; }
        }

        public Uri Uri
        {
            get { return new Uri(this.urlToWorkWith); }
        }
    }
}