using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //CreateAndDownloadBlobFile();

            CreateTableAndReadData();

            return View();
        }

        public void CreateTableAndReadData()
        {
            //Get the storage account from the connection string
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageKey"].ToString());
            //Creates table client object
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            //Create Table if does not exist
            CloudTable table = tableClient.GetTableReference("EmployeeTable");
            table.CreateIfNotExists();
           
        }


        public void CreateAndDownloadBlobFile()
        {
            //Get the storage account from the connection string
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageKey"].ToString());
            //  create a blob client.

            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            //  create a container
            CloudBlobContainer blobcontainer = blobClient.GetContainerReference("fileblob");//blobContainer
            //IEnumerable<IListBlobItem> bloblist = blobcontainer.ListBlobs();

            //ToUpload File
            CloudBlockBlob blob = blobcontainer.GetBlockBlobReference("TestFileToUpload.txt");
            blob.UploadFromFile(ConfigurationManager.AppSettings["FilePathTest"].ToString(), null, null, null);

            //To Download the file from Blob

            //  create a block blob
            CloudBlockBlob blockBlob = blobcontainer.GetBlockBlobReference("TestFileToUpload.txt");

            //  download from Azure Storage
            blockBlob.DownloadToFileAsync(ConfigurationManager.AppSettings["FileToSave"].ToString(), FileMode.Create);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}