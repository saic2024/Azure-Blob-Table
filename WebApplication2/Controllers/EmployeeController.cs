using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmployeeController : Controller
    {
        //Get the storage account from the connection string
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageKey"].ToString());
        

        // GET: Employee
        public ActionResult Index()
        {
            List<EmpDetailsClass> Details = new List<EmpDetailsClass>();
            //Creates table client object
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("EmployeeTable");

            TableQuery<EmpDetailsClass> query = new TableQuery<EmpDetailsClass>();

            foreach (var item in table.ExecuteQuery(query))
            {
                Details.Add(new EmpDetailsClass()
                {
                    EmpId = item.EmpId,
                    EmpName = item.EmpName,
                    Email = item.Email,
                    Designation = item.Designation
                });
            }

            return View(Details);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmpDetailsClass collection)
        {
            try
            {
                EmpDetailsClass emp = new EmpDetailsClass(collection.EmpId,collection.Email);
                emp.EmpId = Convert.ToInt32(collection.EmpId);
                emp.EmpName = collection.EmpName;
                emp.Designation = collection.Designation;
                emp.Email = collection.Email;

                //Creates table client object
                CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();

                CloudTable table = tableClient.GetTableReference("EmployeeTable");

                //Create a Table Operation
                TableOperation insertOperation = TableOperation.InsertOrReplace(emp);
                table.Execute(insertOperation);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
