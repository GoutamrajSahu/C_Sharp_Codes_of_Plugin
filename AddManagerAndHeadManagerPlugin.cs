using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace PluginforReimbursement
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
                    Entity Reimbursement = (Entity)context.InputParameters["Target"];      /*<----Getting the targeted bookIssue info.*/
                    Entity ReimbursementRecord = service.Retrieve("cre2e_reimbursement", Reimbursement.Id, new ColumnSet("ownerid", "cre2e_applicant_guid", "cre2e_manager", "cre2e_head_manager"));

                    /*<-----Fetching user id----->*/
                    string guid1 = ReimbursementRecord.Attributes["cre2e_applicant_guid"].ToString();
                    Guid applicantID = new Guid(guid1); /*<---Getting the attribute value*/

                    /*<-----Fetching manager id----->*/
                    Entity ApplicantRecord = service.Retrieve("systemuser", applicantID, new ColumnSet("parentsystemuserid"));/*<---Getting the student record with given ID.*/
                    ReimbursementRecord.Attributes["cre2e_manager"] = ApplicantRecord.Attributes["parentsystemuserid"];
                    var managerID = ((Microsoft.Xrm.Sdk.EntityReference)(ApplicantRecord.Attributes["parentsystemuserid"])).Id; /*<---Getting ID from lookup.*/

                    /*<-----Fetching manager id----->*/
                    Entity managerRecord = service.Retrieve("systemuser", managerID, new ColumnSet("parentsystemuserid"));
                     ReimbursementRecord.Attributes["cre2e_head_manager"] = managerRecord.Attributes["parentsystemuserid"];
                     service.Update(ReimbursementRecord);   
                }
            }
        }
    }
}
