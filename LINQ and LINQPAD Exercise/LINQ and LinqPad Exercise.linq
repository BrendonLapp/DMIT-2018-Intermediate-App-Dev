<Query Kind="Statements">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

/* 1. Create a product list which indicates what products are purchased by our customers and how many
times that product was purchased. Sort the list by most popular product by alphabetic description. */
var productList = from product in Products
					orderby product.OrderLists.Count descending
					select new 
					{
						Product = product.Description,
						TimesPurchased = product.OrderLists.Count
					};
productList.Dump();

/*2. We want a mailing list for a Valued Customers flyer that is being sent out. List the customer addresses
for customers who have shopped at our stores. List by the store. Include the store location as well as the
customer's complete address. Do NOT include the customer name in the results. List the customer address
only once for a particular store. */
var mailingList =   from store in Stores
					orderby store.Location ascending
					select new 
					{
						Location = store.Location,
						Clients = 	(from o in Orders
								  	where store.StoreID == o.StoreID
								  	select new
								  	{
								  		Address = o.Customer.Address,
										City = o.Customer.City,
										Province = o.Customer.Province
								  	}).Distinct()
					};
mailingList.Dump();
					
/*3. Create a Daily Sales per Store request for a specified month. Sort stores by city by location. For Sales, 
show order date, number of orders, total sales without GST tax and total GST tax.*/
var MonthStart = DateTime.Parse("Dec 01, 2017");
var MonthEnd = DateTime.Parse("Dec 31, 2017");
var DailySales =	from store in Stores
					orderby store.City ascending
					select new
					{
						City = store.City,
						Location = store.Location,
						sales = from order in store.Orders
								group order by order.OrderDate into oDates
								where oDates.Key >= MonthStart
									&& oDates.Key <= MonthEnd
								orderby oDates.Count() descending
								select new
								{
									date = oDates.Key,
									numberoforders = oDates.Count(),
									productsales = oDates.Sum(x => x.SubTotal),
									GST = oDates.Sum(y => y.GST)
								}
					};
DailySales.Dump();

/*4. Print out all product items on a requested order (use Order #33). Group by Category and order by Product Description. 
You do not need to format money as this would be done at the presentation level. Use the QtyPicked in your calculations. 
Hint: You will need to use type casting (decimal). Use of the ternary operator will help. */
var result = 	from cat in Categories
				orderby cat.Description 
				select new
				{
					Category = cat.Description,
					OrderProducts = from product in cat.Products
									from order in product.OrderLists
									where order.OrderID == 33
									select new
									{
										Product = order.Product.Description,
										Price = order.Price,
										PickedQty = order.QtyPicked,
										Discout = order.Discount,
										SubTotal = ((decimal)order.Price * (decimal)order.QtyPicked),
										Tax = ((decimal)order.Price * (decimal)0.05),
										ExtendedPrice = (((decimal)order.Price * (decimal)order.QtyPicked) + ((decimal)order.Price * (decimal)0.05))
									}
				};
result.Dump();

/*5. Select all orders a picker has done on a particular week (Sunday through Saturday). Group and sorted by picker. 
Sort the orders by picked date. Hint: you will need to use the join operator. */
var Sunday = DateTime.Parse("Dec 17, 2017"); //Start of midnight of the day
var Saturday = DateTime.Parse("Dec 25, 2017"); //Start of midnight of the day
var result = from p in Pickers
			 orderby p.LastName + ", " + p.FirstName
			 select new
			 {
			 	FullName = p.LastName + ", " + p.FirstName,
				Orders = from order in p.Store.Orders
						 where order.PickerID == p.PickerID
						 	&& order.OrderDate >= Sunday 
							&& order.OrderDate <= Saturday
						 orderby order.OrderDate
						 select new
						 {
						 	ID = order.OrderID,
							Date = order.OrderDate,
							PickerId = order.PickerID
						 }
			 };
result.Dump();

/*6. List all the products a customer (use Customer #1) has purchased and the number of times the product was purchased. 
Sort the products by number of times purchased (highest to lowest) then description.*/
var customerNumber = 1;
var productListByCustomer =	from customer in Customers
							where customer.CustomerID == customerNumber
							select new
							{
								Customer = customer.LastName + ", " + customer.FirstName,
								OrdersCount = customer.Orders.Count(),
								Items =	from order in customer.Orders
										from orderList in order.OrderLists
										group order.OrderLists by orderList.Product into productOrderList
										orderby productOrderList.Count() descending 
										select new
										{
											Description = productOrderList.Key.Description,
											TimesPurchased = productOrderList.Count()
										}
							};
productListByCustomer.Dump();





