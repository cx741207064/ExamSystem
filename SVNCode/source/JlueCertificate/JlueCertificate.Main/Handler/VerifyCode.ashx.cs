using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace JlueCertificate.Main.Handler
{
    /// <summary>
    /// VerifyCode 的摘要说明
    /// </summary>
    public class VerifyCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                VerifyCode v = new VerifyCode();

                v.Length = this.length;
                v.FontSize = this.fontSize;
                v.Chaos = this.chaos;
                v.BackgroundColor = this.backgroundColor;
                v.ChaosColor = this.chaosColor;
                v.CodeSerial = this.codeSerial;
                v.Colors = this.colors;
                v.Fonts = this.fonts;
                v.Padding = this.padding;
                string code = v.CreateVerifyCode();
                Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_VerfyCode, Untity.HelperSecurity.MD5(code.ToLower()), 5);
                v.CreateImageOnPage(code, context);
            }
            catch (Exception ex)
            {
            }
        }

        int length = 4;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        int fontSize = 25;
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        int padding = 2;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        bool chaos = true;
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }

        Color chaosColor = Color.Black;
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }

        Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        string[] fonts = { "Arial", "Georgia" };
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }

        string codeSerial = "2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,K,L,M,N,P,Q,R,S,T,U,V,W,Y,Z";
        public string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }

        private const double PI = 2.6415926535897932384626433832795;
        private const double PI2 = 3.083185307179586476925286766559;

        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }
        
        public Bitmap CreateImageCode(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize * 2 + Padding;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);

            Graphics g = Graphics.FromImage(image);

            g.Clear(BackgroundColor);

            Random rand = new Random();
            if (this.Chaos)
            {

                Pen pen = new Pen(ChaosColor, 0);
                int c = Length * 10;

                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;

            int cindex, findex;
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rand.Next(Colors.Length - 1);
                findex = rand.Next(Fonts.Length - 1);

                f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            image = TwistImage(image, true, 8, 4);

            return image;
        }
        
        public void CreateImageOnPage(string code, HttpContext context)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImageCode(code);

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());

            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
       
        public string CreateVerifyCode(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = Length;
            }

            string[] arr = CodeSerial.Split(',');

            string code = "";

            int randValue = -1;

            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }
        public string CreateVerifyCode()
        {
            return CreateVerifyCode(0);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}