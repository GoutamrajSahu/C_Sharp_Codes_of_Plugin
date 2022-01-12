using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace UpdateAddress
{
    public class Class1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IExecutionContext context = (IExecutionContext) serviceProvider.GetService(typeof(IExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);


            string fetchContactsXMLQuery = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                              <entity name='contact'>
                                                <attribute name='fullname' />
                                                <attribute name='telephone1' />
                                                <attribute name='contactid' />
                                                <order attribute='fullname' descending='false' />
                                                <filter type='and'>
                                                  <condition attribute='parentcustomerid' operator='eq' uitype='account' value='{0}' />
                                                </filter>
                                              </entity>
                                            </fetch>";


            if (context.Depth == 1)
            {
                if(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                {
                    Entity account = (Entity)context.InputParameters["Target"];      /*<----Getting the targeted account info.*/
                    Entity accountAddress = service.Retrieve("account", account.Id, new ColumnSet(      
                        "address1_line1",
                        "address1_line2",
                        "address1_line3",
                        "address1_city",
                        "address1_stateorprovince",
                        "address1_postalcode",
                        "address1_country"
                        ));
                    string addLine1 = (string)accountAddress.Attributes["address1_line1"];
                    string addLine2 = (string)accountAddress.Attributes["address1_line2"];
                    string addLine3 = (string)accountAddress.Attributes["address1_line3"];
                    string city = (string)accountAddress.Attributes["address1_city"];
                    string stateorprovince = (string)accountAddress.Attributes["address1_stateorprovince"];
                    string postalcode = (string)accountAddress.Attributes["address1_postalcode"];
                    string country = (string)accountAddress.Attributes["address1_country"];

                    EntityCollection contactsCollection = service.RetrieveMultiple(new FetchExpression(string.Format(fetchContactsXMLQuery, account.Id)));

                    foreach( var record in contactsCollection.Entities)     /*<----- Updating the contacts with this loop.*/
                    {
                        record.Attributes["address1_line1"] = addLine1;
                        record.Attributes["address1_line2"] = addLine2;
                        record.Attributes["address1_line3"] = addLine3;
                        record.Attributes["address1_city"] = city;
                        record.Attributes["address1_stateorprovince"] = stateorprovince;
                        record.Attributes["address1_postalcode"] = postalcode;
                        record.Attributes["address1_country"] = country;
                        service.Update(record);
                    }
                }
            }

        }
    }
}
