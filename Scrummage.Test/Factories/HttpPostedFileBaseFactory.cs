using System.IO;
using System.Web;

namespace Scrummage.Test.Factories
{
    public static class HttpPostedFileBaseFactory
    {
        /// <summary>
        ///     Returns a Custom HttpPostedFileBase with ContentLength and InputStream
        /// </summary>
        /// <returns></returns>
        public static CustomHttpPostedFileBase CreateCustomHttpPostedFileBase()
        {
            var stream = new MemoryStream(new byte[1]);
            return new CustomHttpPostedFileBase(stream);
        }
    }

    public class CustomHttpPostedFileBase : HttpPostedFileBase
    {
        private readonly Stream _stream;

        public CustomHttpPostedFileBase(Stream stream)
        {
            _stream = stream;
        }

        public override int ContentLength
        {
            get { return (int) _stream.Length; }
        }

        public override Stream InputStream
        {
            get { return _stream; }
        }
    }
}