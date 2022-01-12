using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CountOfOpportunityOnAccount
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IExecutionContext context = (IExecutionContext) serviceProvider.GetService(typeof(IExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);


            string fetchOpportunitiesXMLQuery = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                              <entity name='opportunity'>
                                                <attribute name='name' />
                                                <attribute name='customerid' />
                                                <attribute name='estimatedvalue' />
                                                <attribute name='statuscode' />
                                                <attribute name='opportunityid' />
                                                <order attribute='name' descending='false' />
                                                <filter type='and'>
                                                  <condition attribute='parentaccountid' operator='eq' uitype='account' value='{0}' />
                                                </filter>
                                              </entity>
                                            </fetch>";


            if (context.Depth == 1)
            {
                if(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                {
                    Entity account = (Entity)context.InputParameters["Target"];      /*<----Getting the targeted account info.*/
                    Entity opportunityCounts = service.Retrieve("account", account.Id, new ColumnSet("grs_number_of_opportunities"));

                    EntityCollection opportunitiesCollection = service.RetrieveMultiple(new FetchExpression(string.Format(fetchOpportunitiesXMLQuery,account.Id)));
                    //EntityCollection opportunitiesCollection = service.RetrieveMultiple(new FetchExpression(fetchContactsXMLQuery));
                    int countOfOpportunities = 0;
                    foreach (Entity entity in opportunitiesCollection.Entities)
                    {
                        countOfOpportunities++;
                    }
                    opportunityCounts.Attributes["grs_number_of_opportunities"] = countOfOpportunities;
                    service.Update(opportunityCounts);
                }
            }
        }
    }
}
