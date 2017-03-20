using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;

namespace FindTheBooty
{
    public class QRReader
    {
        /// <summary>
        /// Takes the input stream of a image file and returns the string contained in the QR image
        /// </summary>
        /// <param name="input">A PNG file</param>
        /// <returns>QR Code String</returns>
        public static string getQRCode(System.IO.Stream input)
        {
            string result = null;
            try
            {
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(input);
                QRCodeDecoder reader = new QRCodeDecoder();
                QRCodeBitmapImage qrCodeImage = new QRCodeBitmapImage(image);
                result = reader.Decode(qrCodeImage);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }
}