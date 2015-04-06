using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HPB.Common.Utils
{
    public class ResponseCapture : IDisposable
    {
        private readonly HttpResponseBase _response;
        private readonly TextWriter _originalWriter;
        private StringWriter _localWriter;

        public ResponseCapture(HttpResponseBase response)
        {
            this._response = response;
            _originalWriter = response.Output;
            _localWriter = new StringWriter();
            response.Output = _localWriter;
        }
        public override string ToString()
        {
            _localWriter.Flush();
            return _localWriter.ToString();
        }
        public void Dispose()
        {
            if (_localWriter != null)
            {
                _localWriter.Dispose();
                _localWriter = null;
                _response.Output = _originalWriter;
            }
        }
    }
}
