using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Reimbursement
{
    public class SetStatusReason : IPlugin
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
                    Entity Reimbursement = context.InputParameters["Target"] as Entity;
                    Entity ReimbursementRecord = service.Retrieve("cre2e_reimbursement", Reimbursement.Id, new ColumnSet("cre2e_amount", "cre2e_description"));

                    decimal amount = ((Money)ReimbursementRecord.Attributes["cre2e_amount"]).Value;
                    if (amount > 4999 && amount < 10000)
                    {
                        ReimbursementRecord.Attributes["statuscode"] = new OptionSetValue(287370003);
                    }
                    else if (amount > 9999)
                    {
                        ReimbursementRecord.Attributes["statuscode"] = new OptionSetValue(287370004);
                    }
                    else
                    {
                        ReimbursementRecord.Attributes["statuscode"] = new OptionSetValue(287370000);
                    }
                    service.Update(ReimbursementRecord);
                }
            }
        }
    }
}
