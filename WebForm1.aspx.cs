using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;

namespace Practice_and_study_code_QRCodeScanner
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //The Generate button:
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateCode(txtCode.Text);
        }
        //The read Button:
        protected void btnRead_Click(object sender, EventArgs e)
        {
            ReadQRCode();
        }
        private void GenerateCode(string name) 
        {
            //Initalizing the BarCode Writer and giving it the format of a QR code.
            var Bwriter = new BarcodeWriter();
            Bwriter.Format = BarcodeFormat.QR_CODE;

            //Take the text written in the text box  and converting it into a unquie QRcode
            var result = Bwriter.Write(name);
            string path = Server.MapPath("~/images/QRImage.jpg");
            var barcodeBitmap = new Bitmap(result);

            //storing the file created, in the filestream 
            using (MemoryStream memory = new MemoryStream()) 
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite)) 
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length); 

                }
            }
            //Outputs the actual QR code
            imgQRCode.Visible = true;
            imgQRCode.ImageUrl = "~/images/QRImage.jpg";

        }
        //Read code from the QRCode
        private void ReadQRCode() 
        {
            //Reading of the generated barcode
            var reader = new BarcodeReader();
            string filename = Path.Combine(Request.MapPath("~/images"), "QRImage.jpg");
            //Detech and decode the barcode inside the bitmap
            var result = reader.Decode(new Bitmap(filename));
            if (result != null) 
            {
                lblQRCode.Text = $"QR code:{result.Text}";
            }
        }

    }
}