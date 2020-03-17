﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SiMay.Core.ScreenSpy
{
    public class JpgCompression : IDisposable
    {
        private readonly ImageCodecInfo _encoderInfo;
        private readonly EncoderParameters _encoderParams;

        public JpgCompression(long quality)
        {
            EncoderParameter parameter = new EncoderParameter(Encoder.Quality, quality);
            this._encoderInfo = GetEncoderInfo("image/jpeg");
            this._encoderParams = new EncoderParameters(2);
            this._encoderParams.Param[0] = parameter;
            this._encoderParams.Param[1] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionRle);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_encoderParams != null)
                {
                    _encoderParams.Dispose();
                }
            }
        }

        public void SetQuanlity(long quality)
        {
            EncoderParameter parameter = new EncoderParameter(Encoder.Quality, quality);
            var end = this._encoderParams.Param[0];
            this._encoderParams.Param[0] = parameter;
            end.Dispose();
        }

        public byte[] Compress(Bitmap bmp)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bmp.Save(stream, _encoderInfo, _encoderParams);
                return stream.ToArray();
            }
        }

        public void Compress(Bitmap bmp, ref Stream targetStream)
        {
            bmp.Save(targetStream, _encoderInfo, _encoderParams);
        }

        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            int num2 = imageEncoders.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (imageEncoders[i].MimeType == mimeType)
                {
                    return imageEncoders[i];
                }
            }
            return null;
        }
    }
}