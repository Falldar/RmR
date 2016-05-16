using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RmR.Helpers
{
    class FileUploads
    {
        // folder for the upload, you can put this in the web.config
        private readonly string UploadPath = "~/ResumeFile/";

        public FileResults RenameUploadFile(HttpPostedFileBase file, string name)
        {
            var fileName = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            //string append = ".jpg";
            //string finalFileName = name+ append;


            //file doesn't exist, upload item but validate first
            //return UploadFile(file, finalFileName);
            return UploadFile(file, name + extension);

        }
        private FileResults UploadFile(HttpPostedFileBase file, string fileName)
        {
            FileResults fileResult = new FileResults { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!ValidateExtension(extension))
            {
                fileResult.Success = false;
                fileResult.ErrorMessage = "Invalid Extension";
                return fileResult;
            }

            try
            {
                file.SaveAs(path);

                //Image imgOriginal = Image.FromFile(path);


                fileResult.FileName = fileName;

                return fileResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generaly logging or testing

                fileResult.Success = false;
                fileResult.ErrorMessage = ex.Message;
                return fileResult;
            }
        }
        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".doc":
                    return true;
                case ".docx":
                    return true;
                case ".pdf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
