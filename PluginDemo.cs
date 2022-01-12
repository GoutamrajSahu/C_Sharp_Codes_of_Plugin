using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approvals
{
    public class Class1 : IPlugin{
        public void Execute(IServiceProvider serviceProvider){
            IPluginExecutionContext context =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = (IOrganizationService)serviceFactory.CreateOrganizationService(context.UserId);

            if (context.Depth == 1){
                if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity){
                    Entity reimburesmennt = (Entity)context.InputParameters["Target"];
                    reimburesmennt = service.Retrieve("cre2e_reimbursement", reimburesmennt.Id, new ColumnSet("cre2e_description", "cre2e_amount"));
                    decimal Amount = ((Money)reimburesmennt.Attributes["cre2e_amount"]).Value;

                    string description;
                    if (Amount > 5000){
                        
                        description = "Your amount is too high";

                        reimburesmennt.Attributes["cre2e_description"] = description;

                    }

                    service.Update(reimburesmennt);
                }
            }

        }
    }
}