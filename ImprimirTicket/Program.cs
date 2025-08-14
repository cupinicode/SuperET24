using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace ImprimirTicket
{
    class Program
    {
        static string textoTicket = "";
        static Image logo;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                    textoTicket = args[0].Replace("|", "\n");

                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "et24.png");
                if (File.Exists(logoPath))
                    logo = Image.FromFile(logoPath);

                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = "POS-80C";
                pd.PrintPage += new PrintPageEventHandler(PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al imprimir: " + ex.Message);
            }
        }

        static void PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            Font fuenteTitulo = new Font("Arial", 14, FontStyle.Bold);
            Font fuenteTexto = new Font("Arial", 10);

            if (logo != null)
            {
                e.Graphics.DrawImage(logo, new PointF(0, y));
                y += logo.Height + 10;
            }

            e.Graphics.DrawString("************ SuperET24 ************", fuenteTitulo, Brushes.Black, 0, y);
            y += fuenteTitulo.GetHeight(e.Graphics) + 5;

            e.Graphics.DrawString("************************************", fuenteTexto, Brushes.Black, 0, y);
            y += fuenteTexto.GetHeight(e.Graphics) + 10;

            e.Graphics.DrawString(textoTicket, fuenteTexto, Brushes.Black, 0, y);
            y += fuenteTexto.GetHeight(e.Graphics) * textoTicket.Split('\n').Length + 10;

            e.Graphics.DrawString("Muchas gracias por visitar ExpoTécnica", fuenteTexto, Brushes.Black, 0, y);
            y += fuenteTexto.GetHeight(e.Graphics);

            e.Graphics.DrawString("Escuela Técnica Nro. 24", fuenteTexto, Brushes.Black, 0, y);
        }
    }
}
