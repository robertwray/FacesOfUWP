using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.FaceAnalysis;
using Windows.Storage.Streams;

namespace FacesOfUWP.UI
{
    public sealed class FacialRecognition
    {
        public async Task<RecogniseResult> Recognise(Stream fileStream)
        {
            var randomAccessStream = fileStream.AsRandomAccessStream();

            var bitmapDecoder = await BitmapDecoder.CreateAsync(randomAccessStream);
            var rawBitmap = await bitmapDecoder.GetSoftwareBitmapAsync();

            var supportedBitmapFormats = FaceDetector.GetSupportedBitmapPixelFormats();
            var supportedFormatBitmap = SoftwareBitmap.Convert(rawBitmap, supportedBitmapFormats.First());

            var faceDetector = await FaceDetector.CreateAsync();
            var faces = await faceDetector.DetectFacesAsync(supportedFormatBitmap);
            
            var result = new RecogniseResult();

            if (faces.Any())
            {
                result.Faces = faces.Count();

                var memoryStream = new InMemoryRandomAccessStream();

                var bitmapEncoder = await BitmapEncoder.CreateAsync(BitmapEncoder.BmpEncoderId, memoryStream);
                bitmapEncoder.SetSoftwareBitmap(rawBitmap);
                bitmapEncoder.BitmapTransform.Bounds = faces.First().FaceBox;

                await bitmapEncoder.FlushAsync();

                result.FirstFace = memoryStream.AsStream();
            }

            return result;
        }
    }
}
