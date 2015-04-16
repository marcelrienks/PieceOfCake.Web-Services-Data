
namespace PieceOfCake.Web.Libraries
{
    public static class RestResourceUri
    {
        /// <summary>
        /// Returns a Formatted URI section of the resource
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static string FormatResourceAsUri(string resource)
        {
            return string.Format("api/{0}", resource);
        }

        public static string FormatResourceAsUri(string resource, int id)
        {
            return string.Format("api/{0}/{1}", resource, id);
        }
    }
}