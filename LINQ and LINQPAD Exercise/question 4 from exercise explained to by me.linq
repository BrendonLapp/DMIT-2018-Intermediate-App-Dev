<Query Kind="Statements">
  <Connection>
    <ID>d76d1978-6d4f-46bd-bfb1-1bc25957ae01</ID>
    <Server>.</Server>
    <Database>GroceryList</Database>
  </Connection>
</Query>

var result = 	from cat in Categories
				orderby cat.Description 
				select new
				{
				//by going from category i can connect into products through its product navigation
				//from there i can go into the orderlist that the products belong to
				//and then sort it by the order ID i want
				
				//This is how i can set up and connect through the tables
				//I need to remember that when I am connecting through the tables I need to start where I can do the easiest connections to get the data
				//This one was started in category because of it being easier to connect from category all the way to the orderlists where i can start to sort by the orderID
					Category = cat.Description,
					OrderProducts = from product in cat.Products
									from order in product.OrderLists
									where order.OrderID == 33
									select new
									{
										//the setting of the properties are done by the order.Product as that is the navigational property from
										//the order table into the product table to get its data
										Product = order.Product.Description,
										//All the properties not done through the product navigational property are on the order table i get form the
										//product.Orderlists navigational property used earlier
										Price = order.Price,
										PickedQty = order.QtyPicked,
										Discout = order.Discount,
										//This is how i can do a type casting to do the math where i can do the decimals or what ever
										SubTotal = ((decimal)order.Price * (decimal)order.QtyPicked),
										Tax = ((decimal)order.Price * (decimal)0.05),
										ExtendedPrice = (((decimal)order.Price * (decimal)order.QtyPicked) + ((decimal)order.Price * (decimal)0.05))
									}
				};
result.Dump();