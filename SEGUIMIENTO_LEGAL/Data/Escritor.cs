using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SEGUIMIENTO_LEGAL.Data
{
    public class Escritor
    {
        public void logError(Exception e)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\log.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + e.Source.ToString().Trim() + "; " + e.Message, ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}  