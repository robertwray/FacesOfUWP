using System.IO;

namespace FacesOfUWP.UI
{
    public class RecogniseResult
    {
        public int Faces { get; set; }
        public Stream FirstFace { get; set; }
    }
}
