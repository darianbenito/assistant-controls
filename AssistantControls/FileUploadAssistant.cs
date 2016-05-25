using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace AssistantControls
{
    public class FileUploadAssistant
    {
        private enum UploadState{                        
                         UPLOADEDWITHOTHERNAME,
                         UPLOADED,
                         NOTUPLOADED
                     };
        #region Métodos públicos 
        public static int UploadFilesToServer(FileUpload fileUpload, string serverLocation, bool strict)
        {       
            int resultState = 0;

            UploadState uploadState = SaveFile(fileUpload, serverLocation, strict);           
            switch (uploadState)
            {
                case UploadState.NOTUPLOADED:
                    resultState = 0;
                    break;
                case UploadState.UPLOADED:
                    resultState = 1;
                    break;
                case UploadState.UPLOADEDWITHOTHERNAME:
                    resultState = 2;
                    break;
            }

            return resultState;
        }

        public static int UploadFilesToServer(Dictionary<FileUpload, string> filesToUpload, bool strict)
        {
            int resultState = 0;
            foreach (KeyValuePair<FileUpload, string> entry in filesToUpload)
            {
                resultState = UploadFilesToServer(entry.Key, entry.Value, strict);
            }

            return resultState;
        }
        #endregion

        #region Métodos privados
        private static UploadState SaveFile(FileUpload fileUpload, string serverLocation, bool strict)
        {
            try
            {
                if (fileUpload.HasFile)
                {
                    UploadState uploadState = UploadState.NOTUPLOADED;

                    // Get the name of the file to upload.
                    string fileName = fileUpload.FileName;

                    // Create the path and file name to check for duplicates.
                    string pathToCheck = serverLocation + fileName;

                    if (!strict)
                    {
                        fileName = GetNewFileName(serverLocation, fileName, pathToCheck);

                        // Notify the user that the file name was changed.
                        uploadState = UploadState.UPLOADEDWITHOTHERNAME;
                    }
                    else
                    {
                        // Notify the user that the file was saved successfully.
                        uploadState = UploadState.UPLOADED;
                    }

                    // Append the name of the file to upload to the path.
                    serverLocation += fileName;

                    // Call the SaveAs method to save the uploaded
                    // file to the specified directory.
                    fileUpload.SaveAs(serverLocation);
                    return uploadState;
                }
                else
                {
                    return UploadState.NOTUPLOADED;
                }
            }
            catch
            {
                return UploadState.NOTUPLOADED;
            }
        }

        private static string GetNewFileName(string serverLocation, string fileName, string pathToCheck)
        {
            // Create a temporary file name to use for checking duplicates.
            string tempfileName = string.Empty;

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = serverLocation + tempfileName;
                    counter++;
                }                      
            }

            return tempfileName;
        }
        #endregion
    }
}
