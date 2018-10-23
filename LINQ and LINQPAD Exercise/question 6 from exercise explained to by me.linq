<Query Kind="Statements">
  <Connection>
    <ID>d76d1978-6d4f-46bd-bfb1-1bc25957ae01</ID>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

var customerNumber = 1;
var productListByCustomer =	from customer in Customers
							where customer.CustomerID == customerNumber
							select new
							{
								Customer = customer.LastName + ", " + customer.FirstName,
								OrdersCount = customer.Orders.Count(),
								//This sets the connection to be from the customers table through the Orders navigational property
								//into the orders table through the orders table OrderLists navigational property
								//and then allow it to be grouped by the orderList.Products
								Items =	from order in customer.Orders
										from orderList in order.OrderLists
										//the group by in this is done by picking the navigational property from the orderList that i want it to be sorted by
										//in this case it is sorted by the property of Product
										//I do this because i want the product details that are on the OrderList
										group order.OrderLists by orderList.Product into productOrderList
										orderby productOrderList.Count() descending 
										select new
										{
										//having the group by above allows the key to be set and to be able to select the description out from the productOrderList
											Description = productOrderList.Key.Description,
											//having the productOrderList as a group allows it to be counted and sorted above in the orderBy
											TimesPurchased = productOrderList.Count()
										}
							};
productListByCustomer.Dump();