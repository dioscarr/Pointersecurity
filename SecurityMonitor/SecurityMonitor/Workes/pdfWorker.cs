using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Drawing;
using SecurityMonitor.Models;

namespace SecurityMonitor.Workes
{
    public class pdfWorker
    {
        public static iTextSharp.text.Font Overpass(string fontpath)
        {
            var fontName = "Tahoma";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\Overpass_Regular.ttf";
                FontFactory.Register(fontpath + "Overpass_Regular.ttf");
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        }


        public string CreateTable0(string FilePath, string ImagePath, string fontpath )
        {


           
           
            string FilePathName = FilePath + "Test.pdf";

            Document doc = new Document();
            PdfPTable table = new PdfPTable(12);
            table.WidthPercentage =100 ;

            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            PdfWriter.GetInstance(doc, new FileStream(FilePathName, FileMode.Create));
             //Heading     
            PdfPCell cell = new PdfPCell(new Phrase("Work Order # 062120150901",
            new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.TIMES_ROMAN,
                20f,
                iTextSharp.text.Font.BOLD,
                iTextSharp.text.BaseColor.LIGHT_GRAY
                )));

            cell.Border = 0;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 2f;
            cell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            cell.BorderWidthBottom = 1;
            cell.BorderColorBottom = iTextSharp.text.BaseColor.LIGHT_GRAY;
            cell.Colspan = 8;            
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            table.AddCell(cell);
            //Tenant
            PdfPCell cellFrom = new PdfPCell(new Phrase("Tenant Request",
           new iTextSharp.text.Font(
            iTextSharp.text.Font.NORMAL, 
            20f, 
            iTextSharp.text.Font.BOLD, 
            iTextSharp.text.BaseColor.LIGHT_GRAY)));
            cellFrom.Border = 0;
            cellFrom.PaddingTop = 10f;
            cellFrom.PaddingBottom = 2f;            
            cellFrom.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            cellFrom.Colspan = 4;
            cellFrom.BorderWidthBottom = 1;
            cellFrom.BorderColorBottom = iTextSharp.text.BaseColor.LIGHT_GRAY;
           
            table.AddCell(cellFrom);
            //iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(ImagePath + "B.png");
            //gif.ScalePercent(20f);
            //table.AddCell(gif);

            PdfPCell marginspace = new PdfPCell();
            marginspace.PaddingTop = 20f;
            marginspace.Colspan = 12;
            marginspace.FixedHeight=22f;
            marginspace.Border = 0;
            table.AddCell(marginspace);
//tenantinfo right box
            PdfPCell TenantInfoBox = new PdfPCell(new Phrase("Joe Smith\n 4530 broadway",
            new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.TIMES_ROMAN,
                12f,
                iTextSharp.text.Font.BOLD,
                iTextSharp.text.BaseColor.LIGHT_GRAY
                )));
            TenantInfoBox.Border = 1;
            TenantInfoBox.PaddingTop = 5f;
            TenantInfoBox.PaddingBottom = 5f;
            TenantInfoBox.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            TenantInfoBox.Colspan = 6;
            TenantInfoBox.BorderWidth = 1;
            TenantInfoBox.BorderWidthBottom = 1;
            TenantInfoBox.BorderWidthLeft = 1;
            TenantInfoBox.BorderWidthRight = 1;
            TenantInfoBox.BorderWidthTop = 1;
            TenantInfoBox.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            TenantInfoBox.PaddingTop = 20f;
            table.AddCell(TenantInfoBox);
//assign left box
            PdfPCell AssigntoCompanyortenant = new PdfPCell(new Phrase("Jose Smith",
           new iTextSharp.text.Font(
               iTextSharp.text.Font.FontFamily.TIMES_ROMAN,
               12f,
               iTextSharp.text.Font.BOLD,
               iTextSharp.text.BaseColor.LIGHT_GRAY
               )));
            AssigntoCompanyortenant.Border = 1;
            AssigntoCompanyortenant.PaddingTop = 5f;
            AssigntoCompanyortenant.PaddingBottom = 5f;
            AssigntoCompanyortenant.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            AssigntoCompanyortenant.Colspan = 6;
            AssigntoCompanyortenant.BorderWidthBottom = 1;
            AssigntoCompanyortenant.BorderWidthLeft = 1;
            AssigntoCompanyortenant.BorderWidthRight = 1;
            AssigntoCompanyortenant.BorderWidthTop = 1;
            AssigntoCompanyortenant.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            AssigntoCompanyortenant.PaddingTop = 20f;
            table.AddCell(AssigntoCompanyortenant);
          

            table.AddCell("Col 1");
            table.AddCell("Col 2");
            table.AddCell("Col 3");
            table.AddCell("Col 4");
            table.AddCell("Col 5");
            table.AddCell("Col 6");
            table.AddCell("Col 7");
            table.AddCell("Col 8");
            table.AddCell("Col 9");
            table.AddCell("Col 10");
            table.AddCell("Col 11");
            table.AddCell("Col 12");
           
            doc.Open();
            doc.Add(table);
            doc.Close();
           // doc.Dispose();
 
            return "success";
        }



        public string CreateTable1(string FilePath, string ImagePath, string fontpath, PdfContractContent pdfModel)
        {

            string oldFile =FilePath + "ContractSample.pdf";
            string newFile = FilePath + "newContractFile.pdf";

            // open the reader
            PdfReader reader = new PdfReader(oldFile);
            iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            Document document = new Document(size);

            // open the writer
            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            // the pdf content
            PdfContentByte cb = writer.DirectContent;

            // select the font properties
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 14);

            string RequestNumber = pdfModel.RequestNumber;
            string Address = pdfModel.Address;
            string Category = pdfModel.Category;
            string Priority = pdfModel.Priority;
            string Status = pdfModel.Status;
            string IssuedOn = pdfModel.Issued;

            string MainContact = pdfModel.primaryContact;
            string MainEmailAddress = pdfModel.PrimaryEmail;
            string MainPhone = pdfModel.PrimaryPhone;

            string Problem = pdfModel.problem;
            string Instructions = pdfModel.TenantInstruction;
            string OfficeNotes = pdfModel.OfficeNotes;

  

           // // write the text in the pdf content
           // cb.BeginText();          
           // // put the alignment and coordinates here
           // cb.ShowTextAligned(0, RequestNumber, 700, 740, 0);

            cb.BeginText();
            
            cb.ShowTextAligned(2, RequestNumber, 550, 648, 0);
            cb.EndText();

            cb.BeginText();
            cb.ShowTextAligned(0, Address, 48, 648, 0);
            cb.EndText(); 

            cb.BeginText();
            cb.ShowTextAligned(0, Category, 118, 578, 0);
            cb.EndText();


            cb.BeginText();
            cb.ShowTextAligned(0, Priority, 108, 561, 0);
            cb.EndText();

            cb.BeginText();
            cb.ShowTextAligned(0, Status, 100, 544, 0);
            cb.EndText();
            

            cb.BeginText();
            cb.ShowTextAligned(0, IssuedOn, 120, 525, 0);                    
            cb.EndText();

            cb.BeginText();
            cb.ShowTextAligned(0, MainContact, 320, 560, 0);
            cb.EndText();

            cb.BeginText();
            cb.ShowTextAligned(0, MainEmailAddress, 363, 542, 0);
            cb.EndText();

            cb.BeginText();
            cb.ShowTextAligned(0, MainPhone, 348, 526, 0);
            cb.EndText();
            
            ColumnText ct = new ColumnText(cb);
             ct.SetSimpleColumn(new Phrase(new Chunk(Problem, FontFactory.GetFont(FontFactory.HELVETICA, 14,iTextSharp.text.Font.NORMAL))),
                     46, 465, 550, 30, 20, Element.ALIGN_LEFT | Element.ALIGN_TOP);
             ct.Go();

             ColumnText oinstruction = new ColumnText(cb);
             oinstruction.SetSimpleColumn(new Phrase(new Chunk(OfficeNotes, FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.NORMAL))),
                     46, 264, 550, 30, 20, Element.ALIGN_LEFT | Element.ALIGN_TOP);
             oinstruction.Go();


           
            
            

            //cb.BeginText();
          
            //cb.ShowTextAligned(0, Problem, 100, 450, 0);
            //cb.ShowTextAligned(0, Instructions, 100, 350, 0);
            //cb.ShowTextAligned(0, OfficeNotes, 100, 250, 0);
           
            //cb.EndText();

            //cb.BeginText();
            //// put the alignment and coordinates here
            //cb.ShowTextAligned(0, MainContact, 100, 540, 0);
            //cb.ShowTextAligned(0, MainEmailAddress, 100, 500, 0);
            //cb.ShowTextAligned(0, MainPhone, 100, 460, 0);
            //cb.EndText();

           


            // create the new page and add it to the pdf
            PdfImportedPage page = writer.GetImportedPage(reader, 1);
            cb.AddTemplate(page, 0, 0);

            // close the streams and voilá the file should be changed :)
            document.Close();
            fs.Close();
            fs.Dispose();
            writer.Close();
            reader.Close();

           

            return "success";
        }






        /// <summary>
        /// This is a method to generate pdf from templates. Templates needs to be existant html pages inn ContractPDF folder
        /// </summary>
        /// <param name="TemplateName"></param>
        public void CreateFromTemplate(string TemplateName)
        {

            ////Creating PDF
            //Document document = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);







            ////load template
            //// Read in the contents of the Receipt.htm file...
            //string contents =System.IO.File.ReadAllText(Server.MapPath("~/ContractPDF/Receitp.htm"));

            //// Replace the placeholders with the user-specified text
            //contents = contents.Replace("[ORDERID]", "123456");
            //contents = contents.Replace("[TOTALPRICE]", Convert.ToDecimal("99").ToString("c"));
            //contents = contents.Replace("[ORDERDATE]", DateTime.Now.ToShortDateString());


            // // Step 4: Parse the HTML string into a collection of elements...
            // var parsedHtmlElements = HTMLWorker.ParseToList(new StringReader(contents,));


            // string FilePath = Server.MapPath("~/ContractPDF/");
            // string FilePathName = FilePath + "Test.pdf";
            // PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePathName, FileMode.Create));
            // //open document to write content
            // document.Open();

            // // Enumerate the elements, adding each one to the Document...
            // foreach (var htmlElement in parsedHtmlElements)
            // document.Add(htmlElement as IElement);


            // var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/B.png"));
            // logo.SetAbsolutePosition(440, 800);
            // document.Add(logo);


            ////content
            ////Paragraph paragraph = new Paragraph("<h1>this is the first line.</h1> " +
            ////                                     " \n This is the second line");
            ////document.Add(paragraph);


            //document.Close();

             

        }


    }
}