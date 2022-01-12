using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace GetAllCustomerFromNavToCRM
{
    class Program
    {
        public class Model
        {

            //NAV Fields
            public String No { get; set; }
            public String Name { get; set; }
            public String Address { get; set; }
            public String Address_2 { get; set; }
            public String Post_Code { get; set; }
            public String City { get; set; }
            public String Country_Region_Code { get; set; }
            public String District { get; set; }
            public String State_Code { get; set; }
            public String Market_Circle { get; set; }
            public String Phone_No { get; set; }
            public String Primary_Contact_No { get; set; }
            public String Contact { get; set; }
            public String Non_Dealer { get; set; }
            public String Search_Name { get; set; }
            public String Balance_LCY { get; set; }
            public String Credit_Limit_LCY { get; set; }
            public String Salesperson_Code { get; set; }
            public String Area_Sales_Manager { get; set; }
            public String Agent_Code { get; set; }
            public String Responsibility_Center { get; set; }
            public String Service_Zone_Code { get; set; }
            public String Blocked { get; set; }
            public String Justification { get; set; }
            public String Privacy_Blocked { get; set; }
            public String Last_Date_Modified { get; set; }
            public String Customer_Group_Code { get; set; }
            public String Division { get; set; }
            public String Cust_Deposit_G_L_No { get; set; }
            public String Deposit_Amount { get; set; }
            public String Customer_Type { get; set; }
            public String Date_Of_Creation { get; set; }
            public String Net_Change { get; set; }
            public String Sale_Type { get; set; }
            public String Web_Indicator { get; set; }
            public String Manual_Block_Reason { get; set; }
            public String Manual_Hold { get; set; }
            public String Rating { get; set; }
            public String CD_Allow { get; set; }
            public String DSP_Blocked { get; set; }
            public String No_Grace { get; set; }
            public String Brand_Group { get; set; }
            public String Points_Earned { get; set; }
            public String Points_Redeemed { get; set; }
            public String Welding_Kit_Block { get; set; }
            public String Customer_Discount_Map { get; set; }
            public String Discount_Map { get; set; }
            public String Last_Date_Of_Entry { get; set; }
            public String Active { get; set; }
            public String Govt_Authority { get; set; }
            public String Virtual_Account_No { get; set; }
            public String Redeem_Points { get; set; }
            public String Earning_Points { get; set; }
            public String DMS { get; set; }
            public String Resd_Phone { get; set; }
            public String Mobile { get; set; }
            public String Fax_No { get; set; }
            public String E_Mail { get; set; }
            public String Home_Page { get; set; }
            public String IC_Partner_Code { get; set; }
            public String Birth_Date { get; set; }
            public String Anniversary_Date { get; set; }
            public String SMS_Alerts { get; set; }
            public String Mobile_No_2 { get; set; }
            public String Owners_Name { get; set; }
            public String Owners_Contact_Number { get; set; }
            public String Document_Sending_Profile { get; set; }
            public String Bill_to_Customer_No { get; set; }
            public String VAT_Registration_No { get; set; }
            public String GLN { get; set; }
            public String Invoice_Copies { get; set; }
            public String Invoice_Disc_Code { get; set; }
            public String Copy_Sell_to_Addr_to_Qte_From { get; set; }
            public String Structure { get; set; }
            public String Tax_Area_Code { get; set; }
            public String Price_List_Area { get; set; }
            public String Form_Code { get; set; }
            public String Print_Net_Rate { get; set; }
            public String Target_State { get; set; }
            public String Gen_Bus_Posting_Group { get; set; }
            public String VAT_Bus_Posting_Group { get; set; }
            public String Excise_Bus_Posting_Group { get; set; }
            public String Customer_Posting_Group { get; set; }
            public String Customer_Price_Group { get; set; }
            public String Customer_Disc_Group { get; set; }
            public String Allow_Line_Disc { get; set; }
            public String Prices_Including_VAT { get; set; }
            public String Prepayment_Percent { get; set; }
            public String Application_Method { get; set; }
            public String Partner_Type { get; set; }
            public String Payment_Terms_Code { get; set; }
            public String Payment_Method_Code { get; set; }
            public String Reminder_Terms_Code { get; set; }
            public String Fin_Charge_Terms_Code { get; set; }
            public String Overdue_Grace_Limit { get; set; }
            public String Overdue_Grace_Days { get; set; }
            public String Cash_Flow_Payment_Terms_Code { get; set; }
            public String Print_Statements { get; set; }
            public String Last_Statement_No { get; set; }
            public String Block_Payment_Tolerance { get; set; }
            public String Preferred_Bank_Account { get; set; }
            public String Location_Code { get; set; }
            public String Combine_Shipments { get; set; }
            public String Reserve { get; set; }
            public String Transit_Document { get; set; }
            public String Shipping_Advice { get; set; }
            public String Shipment_Method_Code { get; set; }
            public String Shipping_Agent_Code { get; set; }
            public String Shipping_Agent_Service_Code { get; set; }
            public String Shipping_Time { get; set; }
            public String Base_Calendar_Code { get; set; }
            public String Replenishment_Location { get; set; }
            public String Customized_Calendar { get; set; }
            public String Currency_Code { get; set; }
            public String Language_Code { get; set; }
            public String L_S_T_No { get; set; }
            public String L_S_T_Date { get; set; }
            public String C_S_T_No { get; set; }
            public String C_S_T_Date { get; set; }
            public String Tax_Liable { get; set; }
            public String E_C_C_No { get; set; }
            public String Range { get; set; }
            public String Collectorate { get; set; }
            public String GST_Customer_Type { get; set; }
            public String GST_Registration_Type { get; set; }
            public String GST_Registration_No { get; set; }
            public String Merchant_Exporter { get; set; }
            public String e_Commerce_Operator { get; set; }
            public String ARN_No { get; set; }
            public String Aggregate_Turnover { get; set; }
            public String P_A_N_No { get; set; }
            public String P_A_N_Status { get; set; }
            public String P_A_N_Reference_No { get; set; }
            public String T_I_N_No { get; set; }
            public String Export_or_Deemed_Export { get; set; }
            public String VAT_Exempted { get; set; }
            public String Nature_of_Services { get; set; }
            public String Inter_Unit { get; set; }
            public String Inter_Unit_Location_Code { get; set; }
            public String Service_Entity_Type { get; set; }
            public String Document_No_4 { get; set; }
            public String Document_No_1 { get; set; }
            public String Document_No_2 { get; set; }
            public String Document_No_3 { get; set; }
            public String Bank_Print { get; set; }
            public String Web_Approver { get; set; }
            public String Global_Dimension_1_Filter { get; set; }
            public String Global_Dimension_2_Filter { get; set; }
            public String Currency_Filter { get; set; }
            public String Date_Filter { get; set; }
            public String ETag { get; set; }






        }
        static void Main(string[] args)
        {

            BaseClass service = new BaseClass();
            service.connect();
         
             var url = ConfigurationManager.AppSettings["uri"];
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            // If required by the server, set the credentials.
            var NavUserName = ConfigurationManager.AppSettings["NavUsername"];
            var NavPassword = ConfigurationManager.AppSettings["NavPassword"];
            request.Credentials = new NetworkCredential(NavUserName, NavPassword);
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                var Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseFromServer);
                var pp = Json["value"];

                var Json1 = JsonConvert.DeserializeObject<List<Model>>(pp.ToString());
                Console.WriteLine(Json1);
                int x = Json1.Count();
                for (int y = 0; x > y; y++)
                {

                    Entity Account = new Entity("account");

                  
                    Account.Attributes["zx_customerstatus"] = Json1[y].Blocked;
                    //Account.Attributes["zx_brandgroup"] = new EntityReference("zx_brandgroup",new Guid("GUID Required")); //Json1[y].Brand_Group; /*-----------------<-Lookup to Brand Group*/
                    Account.Attributes["name"] = Json1[y].Name;    /*--------------------------------------------------------/<---Confuse Nav field: Owners_Name./*/
                    //Account.Attributes["zx_firstname"] = Json1[y];  /*-----------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_lastname"] = Json1[y];   /*-----------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["accountnumber"] = Json1[y].No;
                    //Account.Attributes["zx_recordid"] = Json1[y];   /*-----------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_accounttype"] = Json1[y];  /*---------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["emailaddress1"] = Json1[y].E_Mail;
                    Account.Attributes["zx_gstregtype"] = Json1[y].GST_Registration_Type;
                    Account.Attributes["zx_gstno"] = Json1[y].GST_Registration_No;
                    //Account.Attributes["zx_gstpanimage"]=Json1[y]   /*-----------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_panno"] = Json1[y].P_A_N_No;
                    //Account.Attributes["zx_aadharno"] = Json1[y]   /*------------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_udaannumber"] = Json1[y].Owners_Contact_Number;
                    //Account.Attributes["address1_latitude"] = Json1[y].Location_Code;   /*---------------------------------/<---Confuse Nav field: Location_Code./*/
                    //Account.Attributes["address1_longitude"] = Json1[y].Location_Code;  /*---------------------------------/<---Confuse Nav field: Location_Code./*/
                    Account.Attributes["address"] = Json1[y].Address;
                    Account.Attributes["address2"] = Json1[y].Address_2;
                    //Account.Attributes["zx_zone"] =  new EntityReference("zx_salesterritory",new Guid("GUID Required")); //Json1[y].Service_Zone_Code; /*-----------<-Lookup to Sales Territory*/  /*/<---Confuse Nav field: Service_Zone_Code./*/
                    //Account.Attributes["zx_state"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].State_Code;  /*----------------<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_substate"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].Target_State; /*------------<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_district"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].District;  /*---------------<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_city"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].City;  /*-----------------------<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_area"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y];   /*---------------------------<-Lookup to Sales Territory*/ /*/<--- Nav field not available./*/
                    //Account.Attributes["zx_pincode"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].Post_Code; /*----------------<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_residencenumber"] = Json1[y];   /*--------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_residentialaddress1"] = Json1[y].Address;
                    //Account.Attributes["zx_residentiallandmark"] =Json1[y];   /*-----------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_residentialzone1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required"));//Json1[y].Service_Zone_Code; /*-<-Lookup to Sales Territory*/
                    


                    //Account.Attributes["zx_residentialstate1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required"));  //Json1[y].State_Code; /*----<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_residentialsubstate1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required"));  //Json1[y]; /*------------<-Lookup to Sales Territory*/ /*/<---Confuse Nav field: Target_State./*/
                    //Account.Attributes["zx_residentialdistrict1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required"));  //Json1[y].District; /*---<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_residentialcity1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].City; /*------------<-Lookup to Sales Territory*/
                    Account.Attributes["zx_residentialcity1"] = Json1[y].City;
                    //Account.Attributes["zx_residentialpincode1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y].Post_Code; /*----<-Lookup to Sales Territory*/
                    //Account.Attributes["zx_residentialarea1"] = new EntityReference("zx_salesterritory", new Guid("GUID Required")); //Json1[y]; /*-----------------<-Lookup to Sales Territory*/ /*/<--- Confuse Nav field: Area_Sales_Manager. /Or/ Field not Available/*/
                    //Account.Attributes["zx_activationanddeactivatonreason"] = Json1[y];   /*-----------------------------/<--- Confuse Nav field: Manual_Block_Reason./*/
                    //Account.Attributes["zx_activationstatus"] = Json1[y];   /*-------------------------------------------/<--- Confuse Nav field: P_A_N_Status./*/
                    //Account.Attributes["zx_countertype"] = Json1[y];   /*------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_enrolledinubs"] = Json1[y];    /*---------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_enrolledvalidatedonudaan"] = Json1[y];   /*-----------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_reasonfornotsellingprinceproducts"] = Json1[y].Manual_Block_Reason;   /*------/<--- Confuse Nav field/*/
                    //Account.Attributes["zx_recordstatus"] = Json1[y].P_A_N_Status;   /*----------------------------------/<--- Confuse or not sure Nav field./*/
                    Account.Attributes["zx_saletype"] = Json1[y].Sale_Type;
                    Account.Attributes["zx_sellingtype"] = Json1[y].Sale_Type; /*------------------------------------------/<--- Same with Sale type./*/
                    //Account.Attributes["zx_status"] = Json1[y];   /*-----------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_udaanstatus"] = Json1[y];    /*-----------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["telephone1"] = Json1[y].Phone_No;
                    Account.Attributes["telephone2"] = Json1[y].Mobile;   /*-----------------------------------------------/<--- Other confusion fields available these are: Mobile_No_2, Contact, Owners_Contact_Number./*/
                    Account.Attributes["telephone3"] = Json1[y].Mobile_No_2;  /*-------------------------------------------/<--- Other confusion fields available these are: Mobile, Contact, Owners_Contact_Number./*/
                    //Account.Attributes["websiteurl"] = Json1[y];  /*-----------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_age"] = Json1[y];   /*--------------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_alternatemobileno"] = Json1[y].Mobile_No_2;  /*---------------------------------/<--- Other confusion fields available these are: Mobile, Contact, Owners_Contact_Number./*/
                    //Account.Attributes["zx_children"] = Json1[y];   /*---------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_companylogo"] = Json1[y];   /*------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_cstno"] = Json1[y];   /*------------------------------------------------------/<--- Confuse Nav fields: Tax_Area_Code,Tax_Liable/*/
                    //Account.Attributes["zx_designation"] = Json1[y];   /*------------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_drivinglicensenumber"] = Json1[y];   /*---------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_education"] = Json1[y];   /*----------------------------------------------------/<--- Nav field not available./*/
                 

                    
                    //Account.Attributes["zx_landmark"] =Json[y]   /*------------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_languageofwriting"] = Json1[y].Language_Code;
                    Account.Attributes["zx_languagespoken"] = Json1[y].Language_Code;
                    Account.Attributes["zx_lstno"] = Json1[y].L_S_T_No;
                    Account.Attributes["zx_mobile"] = Json1[y].Mobile;
                    Account.Attributes["zx_nameoforganisation"] = Json1[y].Name;
                    // Account.Attributes["zx_otheraccounttype"] =Json1[y];   /*--------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_ownername"] = Json1[y].Owners_Name;
                    // Account.Attributes["zx_remarksforchangingcustomerstatus"] =Json1[y];   /*----------------------------/<--- Nav field not available./*/
                    // Account.Attributes["zx_remarksforchangingppfnonppfstatus"] =Json1[y];  /*----------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_tinno"] = Json1[y].T_I_N_No;
                    // Account.Attributes["zx_visitingcard"] =Json1[y];   /*------------------------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["zx_voteridnumber"] =Json1[y];   /*-----------------------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["numberofemployees"] =Json1[y];   /*----------------------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["zx_creditutilized"] =Json1[y];   /*----------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_loyaltypoints"] = Json1[y].Points_Earned;
                    // Account.Attributes["zx_noofchildren"] =Json1[y];   /*------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_paymentterm"] = Json1[y].Payment_Terms_Code;
                    Account.Attributes["zx_rewardpoints"] = Json1[y].Points_Redeemed;
                    // Account.Attributes["zx_valueofbusinesslineslakhspa"] =Json1[y]   /*----------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["aging30"] =Json1[y];   /*--------------------------------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["aging60"] =Json1[y];   /*--------------------------------------------------------/<--- Nav field not available./*/
                    // Account.Attributes["aging90"] =Json1[y];   /*--------------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["creditlimit"] = Json1[y].Credit_Limit_LCY;
                    Account.Attributes["zx_creditlimit"] = Json1[y].Credit_Limit_LCY;
                    //Account.Attributes["zx_outstandingamount"] = Json1[y].Deposit_Amount;   /*----------------------------/<---Confuse Nav field: Deposit_Amount./*/
                    //Account.Attributes["zx_overdueamount"] = Json1[y].Deposit_Amount;   /*--------------------------------/<---Confuse Nav field: Deposit_Amount./*/
                    //Account.Attributes["parentaccountid"] = new EntityReference("account", new Guid("GUID Required")); //Json1[y]  /*-------------------------------<-Lookup to Account*/ /*----/<--- Nav field not available./*/
                    //Account.Attributes["primarycontactid"] = new EntityReference("contact", new Guid("GUID Required")); //Json1[y].Primary_Contact_No;  /*----------<-Lookup to Contact*/
                    //Account.Attributes["zx_activateddeactivatedby"] = new EntityReference("zx_team", new Guid("GUID Required")); //Json1[y].Owners_Name;  /*--------<-Lookup to Teams*/
                   


                    //Account.Attributes["zx_architect"] = new EntityReference("account", new Guid("GUID Required"));//Json1[y];  /*----------------------------------<-Lookup to Account*/ /*-/<--- Nav field not available./*/
                    //Account.Attributes["zx_contractor"] = Json1[y];  /*-----------------------<-Lookup to [Depricate] Dynamics Customer Service Analytics*/ /*/<--- Nav field not available./*/
                    //Account.Attributes["zx_customerstatuschangedby"] = new EntityReference("zx_team", new Guid("GUID Required")); //Json1[y];  /*-------------------<-Lookup to Teams*/
                    //Account.Attributes["zx_languagespoken"] = Json1[y];  /*--------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_languageforwriting"] = new EntityReference("zx_languagespoken", new Guid("GUID Required")); //Json1[y];  /*--------------<-Lookup to Language*/ /*/<--- Nav field not available./*/
                    //Account.Attributes["zx_location"] = new EntityReference("account", new Guid("GUID Required")); //Json1[y].Location_Code;  /*--------------------<-Lookup to Account*/
                    //Account.Attributes["zx_paymenttermcreditterm"] = new EntityReference("zx_credittermst", new Guid("GUID Required")); //Json1[y].Payment_Terms_Code;/*------<-Lookup to Credit Terms*/
                    //Account.Attributes["zx_plumber"] = new EntityReference("account", new Guid("GUID Required")); //Json1[y];  /*-----------------------------------<-Lookup to Account*/
                    //Account.Attributes["zx_ppfnonppfstatuschangedby"] = new EntityReference("zx_team", new Guid("GUID Required")); //Json1[y];  /*------------------<-Lookup to Teams*//*---/<--- Nav field not available./*/
                    //Account.Attributes["zx_team"] = new EntityReference("zx_team", new Guid("GUID Required")); //Json1[y];  /*--------------------------------------<-Lookup to Teams*/ /*---/<--- Nav field not available./*/
                    //Account.Attributes["zx_activationstatuschangedon"] = Json1[y];  /*---------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_customerstatuschangedon"] = Json1[y];  /*-----------------------------------/<--- Nav field not available./*/
                    Account.Attributes["zx_dateofanniversary"] = Json1[y].Anniversary_Date;
                    Account.Attributes["zx_dateofbirth"] = Json1[y].Birth_Date;
                    Account.Attributes["zx_dateofregistration"] = Json1[y].Date_Of_Creation;
                    //Account.Attributes["zx_ppfnonppfstatuschangedon"] = Json1[y];  /*----------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_registereddate"] = Json1[y];  /*--------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["creditonhold"] = Json1[y].Credit_Limit_LCY;
                    //Account.Attributes["zx_brandingdone"] = Json1[y];   /*---------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["zx_freezeorders"] = Json1[y];   /*---------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["description"] = Json1[y];
                    //Account.Attributes["zx_reasonifnoppf"] = Json1[y];  /*---------------------------------------------/<--- Nav field not available./*/
                    //Account.Attributes["remarks"] = Json1[y];  /*------------------------------------------------------/<--- Nav field not available./*/
                    Account.Attributes["ownerid"] = Json1[y].Owners_Name;
        


                    //  service.Create(Account);
                    //  Console.WriteLine(responseFromServer);

                }

            }

            // Close the response.
            response.Close();



        }
    }
}
