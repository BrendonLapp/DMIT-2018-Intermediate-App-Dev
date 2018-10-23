<Query Kind="Expression">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

//There are samples in the lower left corner of linqpad
//- Can download more samples of queries
//- There is alot of documentation and samples of the LINQ queries in there



//List all the customers and the name, quantity and unitPrice of each of the items they purchased
from data in Customers
// Customers   Customer[]
select new 
{
	Company = data.CompanyName, //gets the companyname form customers
	Sales = from purchase in data.Orders
			//      Orders object   data.Orders is an Array of Orders[]
			//Orders is a navigational property of the customers entity
			from lineItem in purchase.OrderDetails
			//     OrdersDetail object    purchase.OrderDetails is an Array of OrderDetails
			select new
			{
				Name = lineItem.Product.ProductName, //Name comes from the products table through the orderdetails table
				Qty = lineItem.Quantity,		//Quantity is from the orderDetails table
				UnitPrice = lineItem.UnitPrice //UnitPrice is from the orderDetails table
			}
}



//Dans version of practice 5 
from place in Regions
select new 
{
	RegionName = place.RegionDescription,
	Territories =   from area in place.Territories
	//				from is basically: foreach(var area in place.Territories)
					select area.TerritoryDescription
					// Area is a territory object
					// "." is the property name for the object
}


//An example done in visual studio of how to do a linq query
//This example is in my Northwind example folder in the BLL on the LinqPlayground.cs file
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NorthwindTraders.DAL;

namespace NorthwindTraders.BLL
{
    	class LinqPlayground
    	{
			public void Play()
			{
				using (var context = new NorthwindContext())
				{
					var Regions = context.Regions;
					var result = from place in Regions
								select new
								{
									RegionName = place.RegionDescription,
									Territories = from area in place.Territories
												select area.TerritoryDescription
								};
		
				}
        	}
    	}
}


//Doing grouping
//List all the customer names, grouped by region
from customer in Customers
group customer by customer.Country into RegionCustomers
										//IGrouping<string, Customer[]>
//customers are grouped by their region and are put into a customer grouping of RegionCustomers
select new 
{
	CountryName = RegionCustomers.Key, //Gives the first aspect of the key and what the groupings are done by
	Customers = from info in RegionCustomers
							//RegionCustomers is an IGrouping of customers 			info is a customer
				select info.CompanyName
}



//Grouping practice
//List all the suppliers by country
from supplier in Suppliers
//supplier is a single object of the suppliers[] array
group supplier by supplier.Country into CountrySuppliers
//grouping the suppliers by the country into an IGrouping of suppliers
select new
{
	SupplierCountry = CountrySuppliers.Key, //Sets how the grouping will be done, in this case by their keys(country)
	Suppliers = from info in CountrySuppliers
				select info.CompanyName
				//info is an object of customer in the CountrySuppliers
				//selecting the column of CompanyName to display
}


//List all the employees grouped by their manager
from person in Employees
//    person object is an employee
group person by person.ReportsToEmployee
//      employee grouped by the ReportsToEmployee Employee navigational property, This will be the KEY that does the sorting
into ManagedEmployees
//    an IGrouping<keyType, CollectionType>

//Adding this where clause is to remove the NULL values from the results
where ManagedEmployees.Key != null
select new
{
	Manager = ManagedEmployees.Key.FirstName + " " + ManagedEmployees.Key.LastName,
	Photo = ManagedEmployees.Key.Photo.ToImage(), //This will only come up as a byte[] unless .ToImage() is added
	
	Employee =  from employee in ManagedEmployees
				select new
				{
				  	FirstName = employee.FirstName,
					LastName = employee.LastName,
					EmployeePhoto = employee.Photo.ToImage()
				}
}












































