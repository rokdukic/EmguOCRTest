using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace MordhauOCRtest
{
    class Program
    {

        static Emgu.CV.Image<Bgr, byte> test() {
            Emgu.CV.Image<Bgr, byte> image = new Emgu.CV.Image<Bgr, byte>("./testImages/chatLight.png");
            Emgu.CV.Image<Bgr, byte> outImage;
            Emgu.CV.Image<Gray, byte> imgGray = null;
            string input = "";
            
            while(true) {
                input = Console.ReadLine();
                double threshold;
                if (!double.TryParse(input,out threshold))
                {
                    if(input == "return")
                    {
                        return imgGray.Convert<Bgr, byte>();
                    }
                    break;
                }
                imgGray = image.Convert<Gray, byte>();

                imgGray.Save("grayscale.png");

                imgGray = imgGray.ThresholdBinary(new Gray(threshold), new Gray(255));
                

                /*Emgu.CV.Mat kernel1 = Emgu.CV.CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, new Size(3, 3), new Point(1, 1));
                Emgu.CV.Matrix<byte> kernel2 = new Emgu.CV.Matrix<byte>(new Byte[3, 3] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } });

                imgGray = imgGray.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close, kernel1, new Point(-1, -1), 1, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar());*/

                imgGray.Save("threshold.png");


            }
            return null;
        }

        static string DoOCR(Emgu.CV.Image<Bgr, byte> inputImage)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            //Emgu.CV.Image<Bgr, Byte> image = new Emgu.CV.Image<Bgr, Byte>("./testImages/chat.png");


            Emgu.CV.OCR.Tesseract t = new Emgu.CV.OCR.Tesseract(@"./tessdata", "eng", Emgu.CV.OCR.OcrEngineMode.Default);
            t.SetImage(inputImage);
            t.SetVariable("tessedit_char_whitelist", " ABCDEFGHIJKLMNOPQRSTUVWXYZabccdefghijklmnopqrstuvwxyz");
            t.SetVariable("user_defined_dpi", "70");
            t.Recognize();
            foreach(var c in t.GetCharacters()){
                Rectangle bla = new Rectangle(c.Region.X - 5, c.Region.Y - 5, c.Region.Width + 5, c.Region.Height + 5);
                inputImage.Draw(bla,new Bgr(0,0,255));
            }
            inputImage.Save("result.png");
            var result = t.GetUTF8Text();
            return result.ToString();
        }

        static void Main(string[] args)
        {
            Emgu.CV.Image<Bgr, byte> inputImage = test();
            string test2 = DoOCR(inputImage);
            Console.WriteLine(test2);
            Console.ReadKey();
        }
    }
}
