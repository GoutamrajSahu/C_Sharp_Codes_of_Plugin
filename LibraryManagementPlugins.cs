using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PluginsForLibManagement
{
    public class AddStudentName : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IExecutionContext context = (IExecutionContext)serviceProvider.GetService(typeof(IExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.Depth < 8)
            {
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                {
                    Entity bookIssue = (Entity)context.InputParameters["Target"];      /*<----Getting the targeted bookIssue info.*/
                    Entity bookIssueRecord = service.Retrieve("grs_bookissue", bookIssue.Id, new ColumnSet("grs_name","grs_student_user_name", "grs_student_guid"));

                    //Guid studentIdFromBookIssue = bookIssueRecord.GetAttributeValue<Guid>("grs_student_guid"); /*<---Getting the attribute value*/
                    string guid = bookIssueRecord.Attributes["grs_student_guid"].ToString();
                    Guid studentIdFromBookIssue = new Guid(guid); /*<---Getting the attribute value*/

                    Entity student = service.Retrieve("grs_student", studentIdFromBookIssue, new ColumnSet("grs_student_name")); /*<---Getting the student record with given ID.*/

                    string studentName = student.GetAttributeValue<string>("grs_student_name");
                    //string studentName = "XYZ";

                    bookIssueRecord.Attributes["grs_name"] = studentName;
                    service.Update(bookIssueRecord);
                }
            }
        }
    }
}
