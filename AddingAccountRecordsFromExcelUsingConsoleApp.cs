using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ConsoleApp1234
{
    class Program
    {
        static IOrganizationService Getorganisation()
        {
            string username = "GoutamrajSahu@GRS187.onmicrosoft.com";
            string password = "Hey@cortana";
            ClientCredentials cc = new ClientCredentials();
            cc.UserName.UserName = username;
            cc.UserName.Password = password;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IOrganizationService service = new OrganizationServiceProxy(new Uri(ConfigurationManager.AppSettings["URL"]), null, cc, null);
            return service;
        }


        static void Main(string[] args)
        {
            IOrganizationService service = Getorganisation();
            Console.WriteLine("Connection Established with crm");

            /*<<--------Fetching Excel file-------->>*/
            Application excelApp = new Application();

            Workbook EBook = excelApp.Workbooks.Open(@"D:\My Company\D. Sir Tasks\Excel for Account\Accounts XLS Worksheet.xlsx");
            _Worksheet ESheet = EBook.Sheets[1];
            Range excelRange = ESheet.UsedRange;

            int numberOfRows = excelRange.Rows.Count;
            int numberOfCols = excelRange.Columns.Count;

            for(int i = 1; i<=numberOfRows; i++)
            {
                if (excelRange.Cells[i, 1] != null && excelRange.Cells[i, 1].Value2 != null && excelRange.Cells[i, 2] != null && excelRange.Cells[i, 2].Value2 != null)
                 {
                    string Name = excelRange.Cells[i, 1].Value2.ToString();
                    string Number = excelRange.Cells[i, 2].Value2.ToString();
                    Entity newAccountRecord = new Entity("account");
                    newAccountRecord["name"] = Name;
                    newAccountRecord["telephone1"] = Number;
                    service.Create(newAccountRecord);
                }
            }
            
            Console.WriteLine("Accounts added successfully");
            excelApp.Quit();
            Console.ReadLine();
        }
    }
}