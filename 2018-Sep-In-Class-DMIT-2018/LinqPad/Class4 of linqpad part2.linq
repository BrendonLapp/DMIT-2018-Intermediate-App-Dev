<Query Kind="Program">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

void Main()
{
	var data =  from cat in Categories
				orderby cat.CategoryName
				select new 
				{ //an anonymous type
					Name = cat.CategoryName,
					Items = from item in cat.Products
							where !item.Discontinued
							orderby item.ProductName
							select new
							{
								Product = item.ProductName,
								Price = item.UnitPrice.Value,
								QtyPerUnit = item.QuantityPerUnit,
								InStock = item.UnitsInStock
							}
				};
	data.Dump();
}

// Define other methods and classes here

public class Category // A DTO  - Data Transfer object
{
	public string Name { get; set;}
	public IEnumerable Items { get; set;}
}

public class InventoryItem // A POCO  - Plain Old CLR Object
{
	public string Product { get; set;}
	public decimal Price { get; set;}
	public int? InStock { get; set;}
	public string QtyPerUnit { get; set;}
}

/*
Class ----
A blueprint for a complex datatype used for defining what an object should
look like (properties/fields) and how an object should behave (constructors and methods)

POCO and DTO are meant to be containers for data with no methods

POCO ---- Plain Old CLR Object
					-Common Language Runtime
A class that has no methods and whose properties/fields are the built in data type in .Net

DTO ---- Data Transfer Object
Class that has no methods and whose properties/fields are not data types and/or POCOS or DTOs

CBO ---- Custom Buisness Object
A class with methods or properties/fields representing a constraint buisness concern such as
validating its property values
*/














