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
using System.Text;
using System.Threading.Tasks;

namespace GetAllProductFromNavToCrm
{
    class Program
    {

        public class ProductMasterFields
        {
            public String No { get; set; }
            public String Description { get; set; }
            public String Sample_Item { get; set; }
            public String Complimentary_Item { get; set; }
            public String Base_Unit_of_Measure { get; set; }
            public String Item_Subcategory_Code { get; set; }
            public String Item_Category_Code { get; set; }
            public String Product_Group_Code { get; set; }
            public String Units_per_Parcel { get; set; }
            public String Max_Weight { get; set; }
            public String Avg_Weight { get; set; }
            public String Net_Weight { get; set; }
            public String Gross_Weight { get; set; }

            public String Min_Weight { get; set; }

            public String Item_Class { get; set; }
            public String Brand { get; set; }
            public String GST_Group_Code { get; set; }
            public String HSN_SAC_Code { get; set; }


        }
        static void Main(string[] args)
        {
            CrmServiceClient service = connect();
            var url = ConfigurationManager.AppSettings["uri"];
            // Create a request for the URL.
            WebRequest request = WebRequest.Create(url);
            var NavUserName = ConfigurationManager.AppSettings["NavUsername"];
            var NavPassword = ConfigurationManager.AppSettings["NavPassword"];
            request.Credentials = new NetworkCredential(NavUserName, NavPassword);
            // Get the response.
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                var Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseFromServer);
                var pp = Json["value"];

                var Json1 = JsonConvert.DeserializeObject<List<ProductMasterFields>>(pp.ToString());
                int x = Json1.Count();
                for (int y = 0; x > y; y++)
                {

                    Entity ProductMaster = new Entity("zx_productmaster");

                    //1-30 Pritam

                    ProductMaster.Attributes["zx_productcode"] = Json1[y].No;
                    ProductMaster.Attributes["zx_productname"] = Json1[y].Description;

                   /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression UOM_Qe = new QueryExpression()
                    {
                        EntityName = "zx_unitofmeasure",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_unitofmeasurecode",
                                    Operator = ConditionOperator.Equal,
                                     Values = {  Json1[y].Base_Unit_of_Measure }
                                }

                            }
                        }
                    };


                    EntityCollection UOM_Ec = service.RetrieveMultiple(UOM_Qe);
                    int count = UOM_Ec.Entities.Count;
                    if (count != 0)
                    {
                        Guid Uom_Id = UOM_Ec.Entities[0].Id;
                        ProductMaster.Attributes["zx_uom"] = new EntityReference("zx_unitofmeasure", Uom_Id); //Lookup   /*<---Add Lookup code done*/
                    }

                   /*---------------------------------------------------------------------------------------------------------*/

                    ProductMaster.Attributes["zx_unitperparcel"] = int.Parse(Json1[y].Units_per_Parcel);


                   /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression Hsn_Qe = new QueryExpression()
                    {
                        EntityName = "zx_hsnmaster",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                    new ConditionExpression
                                {
                                    AttributeName = "zx_hsncode",
                                    Operator = ConditionOperator.Equal,
                                     Values = {  Json1[y].HSN_SAC_Code }
                                }

                            }
                        }
                    };


                    EntityCollection HSN_Ec = service.RetrieveMultiple(Hsn_Qe);
                    int count_1 = HSN_Ec.Entities.Count;
                    if(count_1 != 0)
                    {
                        Guid HSN_Id = HSN_Ec.Entities[0].Id;
                        ProductMaster.Attributes["zx_hsncode"] = new EntityReference("zx_hsnmaster", HSN_Id); //Json1[y].HSN_SAC_Code; //Lookup   /*<---Add Lookup code*/ //Lookup to Entity(zx_hsnmaster)
                    }
                   /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression fetchGST_Qe = new QueryExpression()
                    {
                        EntityName = "zx_gstgroupcode",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_gstgroupcode",
                                    Operator = ConditionOperator.Equal,
                                    Values = {Json1[y].GST_Group_Code}
                                }
                            }
                        }
                    };

                    EntityCollection GSTg = service.RetrieveMultiple(fetchGST_Qe);
                    int countOfGSTgRecords = GSTg.Entities.Count;
                    if(countOfGSTgRecords != 0)
                    {
                        Guid GSTg_Id = GSTg.Entities[0].Id;
                        ProductMaster.Attributes["zx_gstgroupcode"] = new EntityReference("zx_gstgroupcode", GSTg_Id);//Json1[y].GST_Group_Code; //Lookup    /*<---Add Lookup code done*/ //Lookup to Entity(zx_gstgroupcode)
                    }
                   /*---------------------------------------------------------------------------------------------------------*/

                    ProductMaster.Attributes["zx_description"] = Json1[y].Description;
                    ProductMaster.Attributes["zx_productpublishstatus"] = new OptionSetValue(425120000);

                    // ProductMaster.Attributes["zx_brandgroup"] = Json1[y].; Lookup  /*<---Add Lookup code*/
                   /*---------------------------------------------------------------------------------------------------------*/

                    QueryExpression Item_Cate = new QueryExpression()
                    {
                        EntityName = "zx_itemcategories",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                 new ConditionExpression
                                 {
                                    AttributeName = "zx_itemcategorycode",
                                    Operator = ConditionOperator.Equal,
                                     Values = { Json1[y].Item_Category_Code }
                                 }

                            }
                        }
                    };


                    EntityCollection Item_Cate_Ec = service.RetrieveMultiple(Item_Cate);
                    int count_2 = Item_Cate_Ec.Entities.Count;
                    if (count_2 != 0)
                    {
                        Guid ItemCat_Id = Item_Cate_Ec.Entities[0].Id;
                        ProductMaster.Attributes["zx_itemcategory"] = new EntityReference("zx_itemcategories", ItemCat_Id);//Json1[y].Item_Category_Code; //Lookup    /*<---Add Lookup code done*/
                    }


                   /*---------------------------------------------------------------------------------------------------------*/                  

                    QueryExpression Item_Sub_Cate_Qe = new QueryExpression()
                    {
                        EntityName = "zx_itemsubcategory",     /*<---chaged entity schema name from zx_itemcategories to zx_itemsubcategory*/
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_categorycode",   /*<---chaged attribute schema name from zx_itemcategorycode to zx_categorycode*/
                                    Operator = ConditionOperator.Equal,
                                    Values = { Json1[y].Item_Category_Code }
                                }
                            }
                        }
                    };


                    EntityCollection Item_Sub_Cate_Ec = service.RetrieveMultiple(Item_Sub_Cate_Qe);
                    int count_3 = Item_Sub_Cate_Ec.Entities.Count;
                    if (count_3 != 0)
                    {
                        Guid Item_Sub_Cat_Id = Item_Sub_Cate_Ec.Entities[0].Id;
                        ProductMaster.Attributes["zx_itemsubcategory"] = new EntityReference("zx_itemcategories", Item_Sub_Cat_Id); //Json1[y].Item_Subcategory_Code; //Lookup   /*<---Add Lookup code done*/
                    }
                    /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression fetchProductGroup_Qe = new QueryExpression()
                    {
                        EntityName = "zx_productgroup",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression 
                                {
                                    AttributeName = "zx_productgroupcode",
                                    Operator = ConditionOperator.Equal,
                                    Values = { Json1[y].Product_Group_Code }
                                }
                            }
                        }

                    };
                    EntityCollection productGroup_Ec = service.RetrieveMultiple(fetchProductGroup_Qe);
                    int countOfProductGroup_Ec = productGroup_Ec.Entities.Count;
                    if(countOfProductGroup_Ec != 0)
                    {
                        Guid productGroup_Id = productGroup_Ec[0].Id;
                        ProductMaster.Attributes["zx_productgroup"] = new EntityReference("zx_productgroup", productGroup_Id); //Json1[y].Product_Group_Code; //Lookup   /*<---Add Lookup code done*/  //Lookup to Entity(zx_productgroup)
                    }

                    /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression fetchBrand_Qe = new QueryExpression() 
                    { 
                        EntityName = "zx_brands",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_brandcode",
                                    Operator = ConditionOperator.Equal,
                                    Values = { Json1[y].Brand }
                                }
                            }
                        }
                    };
                    EntityCollection brand_Ec = service.RetrieveMultiple(fetchBrand_Qe);
                    int coountOfBrand_Ec = brand_Ec.Entities.Count;
                    if(coountOfBrand_Ec != 0)
                    {
                        Guid brand_Id = brand_Ec[0].Id;
                        ProductMaster.Attributes["zx_brand"] = new EntityReference("zx_brands", brand_Id);  //Lookup    /*<---Add Lookup code done*/   //Lookup to Entity(zx_brands)
                    }

                    /*---------------------------------------------------------------------------------------------------------*/
                    QueryExpression fetchItemClass_Qe = new QueryExpression()
                    {

                        EntityName = "zx_itemclass",
                        ColumnSet = new ColumnSet(true),
                        Criteria = new FilterExpression
                        {
                            FilterOperator = LogicalOperator.And,
                            Conditions =
                            {
                                new ConditionExpression
                                {
                                    AttributeName = "zx_itemclasscode",
                                    Operator = ConditionOperator.Equal,
                                    Values = { Json1[y].Item_Class }
                                }
                            }
                        }
                    };
                    EntityCollection itemClass_Ec = service.RetrieveMultiple(fetchItemClass_Qe);
                    int countOfItemClass_Ec = itemClass_Ec.Entities.Count; 
                    if(countOfItemClass_Ec != 0)
                    {
                        Guid itemClass_Id = itemClass_Ec[0].Id;
                        ProductMaster.Attributes["zx_itemclass"] = new EntityReference("zx_itemclass", itemClass_Id); //Lookup   /*<---Add Lookup code done*/ //Lookup to Entity(zx_itemclass)
                    }
                    
                    /*---------------------------------------------------------------------------------------------------------*/
                    ProductMaster.Attributes["zx_itemtype"] = new OptionSetValue(425120000); //manufacturing
                 //   ProductMaster.Attributes["zx_targetgroup"] =Json1[y].


                    ProductMaster.Attributes["zx_grossweight"] = decimal.Parse(Json1[y].Gross_Weight);
                    ProductMaster.Attributes["zx_avgweight"] = decimal.Parse(Json1[y].Avg_Weight);
                    ProductMaster.Attributes["zx_netweight"] = decimal.Parse(Json1[y].Net_Weight);
                    ProductMaster.Attributes["zx_maxweight"] = decimal.Parse(Json1[y].Max_Weight);


                    if(String.Equals(Json1[y].Complimentary_Item.ToLower(), "false"))
                    {
                        ProductMaster.Attributes["zx_complementaryproduct"] = false; 
                    }else if(String.Equals(Json1[y].Complimentary_Item.ToLower(), "true")){
                        ProductMaster.Attributes["zx_complementaryproduct"] = true;
                    }
                    

                    if (String.Equals(Json1[y].Sample_Item.ToLower() ,"no"))
                    {
                        ProductMaster.Attributes["zx_productsamples"] = new OptionSetValue(425120001);
                    }
                    else if(String.Equals(Json1[y].Sample_Item.ToLower(), "yes"))
                    {
                        ProductMaster.Attributes["zx_productsamples"] = new OptionSetValue(425120000);
                    }
                    
                    //  ProductMaster.Attributes["zx_freesamples"] = Json1[y]
                    ProductMaster.Attributes["zx_minweight"] = decimal.Parse(Json1[y].Min_Weight);


                    if (String.Equals(Json1[y].Complimentary_Item.ToLower(), "no"))
                    {
                        ProductMaster.Attributes["zx_complimentaryproduct"] = new OptionSetValue(425120001);
                    }
                    else if (String.Equals(Json1[y].Complimentary_Item.ToLower(), "yes"))
                    {
                        ProductMaster.Attributes["zx_complimentaryproduct"] = new OptionSetValue(425120000);
                    }
                    
                    // ProductMaster.Attributes["zx_reedemrate"] = Json1[y].r;
                    //  ProductMaster.Attributes["zx_maketoorder"] = Json1[y]
                    //   ProductMaster.Attributes["zx_focusedproduct"] =


                    try {
                        
                        service.Create(ProductMaster);
                        Console.WriteLine($"Product No/Code: {Json1[y].No}, Product Name: {Json1[y].Description}, Status: Created Successfully in CRM !!!");
                       // Console.WriteLine("Success, Got It");
                    }

                    catch(Exception ex)
                        {
                        Console.WriteLine("Error");
                        Console.WriteLine(ex);
                        Console.WriteLine("Error, Got It");
                    //  Console.WriteLine(responseFromServer);

                        }

            }
            Console.WriteLine("All product from NAV Created Successfully in CRM !!!");         
        }

        


           
        }
        public static CrmServiceClient connect()
        {


            var url = ConfigurationManager.AppSettings["url"];
            var userName = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];

            string conn = $@"  Url = {url}; AuthType = OAuth;
            UserName = {userName};
            Password = {password};
            AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
            RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
            LoginPrompt=Auto;
            RequireNewInstance = True";



            var svc = new CrmServiceClient(conn);
            return svc;
        }
    }
}
