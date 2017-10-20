using System;
using Microsoft.VisualBasic.FileIO;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class BatchRegisterModels
    {
        public void FileParser()
        {
            var path = @"/Users/terrycao/DownloadsQ/user.csv"; 
            
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Name = fields[0];
                    string Address = fields[1];
                    Console.Write(Name + Address);
                }
            }
        }
        
    }
}