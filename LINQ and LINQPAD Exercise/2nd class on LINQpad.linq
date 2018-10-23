<Query Kind="Expression">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

//Remember to run these code clocks individually with f5 or they will not work

// This query is to do
// List all the employees who have been involved in processing customer orders

from person in Employees  //Employees is an ICollection of EMPLOYEES objects
										//Person is a singular object of the employees
										//Person would be a name we assign to hold the objects, it can be named anything
where person.Orders.Count > 0 //Orders is an ICollection of ORDER objects
select new 
{
	Name = person.FirstName + " " + person.LastName
}

//In the lambda tab on the results
//The => is the lambda operator, this is to be read as "Such that"




//List all the employees without a manager
from person in Employees
//     thing        thing[]
where person.ReportsTo == null
//       thing       thing(column name)		This example uses a datatype of int? for the ReportsTo
select new 
{
	Name = person.FirstName +  " "  + person.LastName //this is an anonymous type
	//Anonymous type: An object made on the fly, they are made up of properties but no methods
	//							Can be used to hold data easily
}



//List all the employees who don't manage anyone
from person in Employees
where person.ReportsToChildren.Count == 0 //Can do the where person.WhatEver on an ICollection just like a single column datatype
select new
{
	Name = person.FirstName + " " + person.LastName 
}



//List all the employees with "an" in their first name
from person in Employees
where person.FirstName.Contains("an")
select person


//List the first name of all the employees who look after 7 or more territories
from person in Employees
where person.EmployeeTerritories.Count >= 7
select person.FirstName //Can just add the column I want to have come back from the database instead of using an anonymous type
//can also do expression on the select EG: person.FirstName + " " + person.LastName

//List the first name of all employees who look after 7 or more territories
// as well as all the names of the territories they are responsible for
from person in Employees
where person.EmployeeTerritories.Count >= 7
select new
{
	First = person.FirstName,
	Last = person.LastName,
										 //Each place in the employeeTerritories is a single item in the ICollection of person.EmployeeTerritories
	Terriroties = from place in person.EmployeeTerritories
					  select place.Territory.TerritoryDescription //Can set up a query inside of the anonymous type to allow for more descriptive searches
					  															  //In this example it allows for the territory names to be taken from the employee tables based
																				  //on the employee id's
}



//List all the customers and the name, quantity and unitPrice of each of the items they purchased
from data in Customers
select new 
{
	Company = data.CompanyName, //gets the companyname form customers
	Sales = from purchase in data.Orders		//points to the customers purchases
				//joins company to orders based on the Orders column
				from lineItem in purchase.OrderDetails //points to the customers purchases details
				//joins orders to orderdetails based on the OrderDetails column
				select new
				{
					Name = lineItem.Product.ProductName, //Name comes from the products table through the orderdetails table
					Qty = lineItem.Quantity,		//Quantity is from the orderDetails table
					UnitPrice = lineItem.UnitPrice //UnitPrice is from the orderDetails table
				}
}


/*
	Practice
	
	1) List all the customer names for those with more than 5 orders
	2) Give me a list of all the region names
	3) Give me a list of all the territory names
	4) List all the regions and the number of territories in each region
	5) List all the region and territory names as an object graph - use a nested query
*/

//  1)
from person in Customers
where person.Orders.Count > 5
select person

// 2)
from place in Regions
select place.RegionDescription

// 3)
from territory in Territories
select territory.TerritoryDescription

// 4)
from places in Regions
select new 
{
	region = places.RegionDescription,
	territory = places.Territories.Count
}


// 5)
from region in Regions
select new 
{
	Regions = region.RegionDescription,
	territory =  from tory in region.Territories
					//select tory.TerritoryDescription
					select new 
					{
						Territory = tory.TerritoryDescription
					}
}

//Note to self: when doing the nested query track how the data is moving, the last join is where i can pull data from the database