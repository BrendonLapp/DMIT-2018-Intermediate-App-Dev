<Query Kind="Statements">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

/*Example 1: Querying data from Northwind*/
var result = from row in Products
			 where row.UnitPrice > 50
			 select row;

// The following line won't work in your VS project
result.Dump();

//Find the orders that are due to be shipped
var toShip = from due in Orders
			 where !due.ShippedDate.HasValue //Because of Datetime being nullable it has an extra property of HasValue
			 				//DateTime?  -- nullable type
			 orderby due.RequiredDate
			 select new //Declaring an "anonymous type" on-the-fly
			 			//using an initializer list to set the properties
			 {
			 	ID = due.OrderID,
			 	RequiredOn = due.RequiredDate,
				Shipper = due.ShipVia.CompanyName
			 };
toShip.Count().Dump(); //Show the count of the items
toShip.Take(5).Dump(); //show the first 5 items





var employeeCities = (from person in Employees
					 select person.City).Distinct();
employeeCities.Dump();


// Program statement - a complete line of code. It's a complete thought
// Program expression - an incomplete line of code. It's a single value that can be passed around
//						It's an incomplete thought


var customerCities = from buyer in Customers
					 select buyer.City;
customerCities.Distinct().Dump();
customerCities.Count().Dump("# of customer cities");
customerCities.Distinct().Count().Dump("# of distinct cutomer cities");
var combined = employeeCities.Union(customerCities.Distinct());
combined.Count().Dump("# of cities");
combined.Distinct().Count().Dump("# of cities");

var shared = emplyoeeCities.Intersect(customerCities);
shared.Dump = ("Cities for both employees and customers");

var uniqueToEmployee = employeeCities.Except(customerCities);
uniqieToEmployees.Dump("Cities with employees but no customers");










// Get the following from the Orders table for a specific month:
// OrderDate, ID, count of distinct items, Totale sale
// for items that have been shipped
// Then display the total income for the month and the average line items.
DateTime searchPeriod = new DateTime(1998, 1, 1);
var billsForMonth = from item in Orders
					where item.ShippedDate.HasValue
					&& item.OrderDate.Value.Month == searchPeriod.Month
					&& item.OrderDate.Value.Year == searchPeriod.Year
					orderby item.OrderDate descending
					select new
					{
						OrderDate = item.OrderDate,
						OrderID = item.OrderID,
						DistinctItems = item.OrderDetails.Count,
						TotalSale = item.OrderDetails.Sum(od => od.Quantity * od.UnitPrice)
					};
var title = string.Format("Total income for {0} {1}", searchPeriod.ToString("MMMM"), searchPeriod.Year);
billsForMonth.Sum(bill => bill.TotalSale).ToString("C").Dump(title, true);
billsForMonth.Average(bill => bill.DistinctItems).Dump("Average line items", true);



















