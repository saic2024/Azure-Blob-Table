using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EmpDetailsClass : TableEntity
    {
        public EmpDetailsClass()
        {

        }

        public EmpDetailsClass(int empid, string email)
        {
            this.RowKey = empid.ToString();
            this.PartitionKey = email;
        }

        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
    }
}