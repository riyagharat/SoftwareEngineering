using MessagingToolkit.QRCode.Codec;
using System.Drawing;

namespace FindTheBooty
{
    /// <summary>
    /// This is a wrapper class for generating QR code images.
    /// </summary>
    public class QRGenerator
    {
        public static Bitmap generateQRCode(string qrCode)
        {
            Bitmap image;
            QRCodeEncoder encoder = new QRCodeEncoder();

            // set QR Code Format info
            encoder.QRCodeVersion = 2;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;

            image = encoder.Encode(qrCode);
            return image;
        } 
    }
}