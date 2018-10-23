<Query Kind="Statements">
  <Connection>
    <ID>d76d1978-6d4f-46bd-bfb1-1bc25957ae01</ID>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

//REMOVE THIS LATER
//TODO 9: Most Popular product sold (qty), by year and month

//TODO 10: Get the product and total quantity sold, grouped by year and month --This is in Northwind
var ProductQuantityByYearAndMonth =
	from soldItem in OrderDetails
	group soldItem by new //The following anonymous object will be the KEY for the group by clause
						{
							soldItem.Order.OrderDate.Value.Year,
							soldItem.Order.OrderDate.Value.Month
						}
	into groupedOrderDetails
	orderby groupedOrderDetails.Key.Year, groupedOrderDetails.Key.Month
	//select groupedOrderDetails;
	select new 
	{
		Year = groupedOrderDetails.Key.Year,
		Month = groupedOrderDetails.Key.Year,
		//Items = groupedOrderDetail
		Items = from orderDetailItem in groupedOrderDetails
				select new //orderDetailItem
				{
					Product = orderDetailItem.Product.ProductName
				}
	};
ProductQuantityByYearAndMonth.Dump();