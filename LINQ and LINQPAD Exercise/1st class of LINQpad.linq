<Query Kind="Statements">
  <Connection>
    <ID>7e6177ca-e5d8-4264-9112-c41128c00b49</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Northwind_DMIT2018</Database>
  </Connection>
</Query>

 //Linqpad is being used to learn about Language INtegrated Query
 //Linqpad allows for SQL like querying inside of C# code instead
 //LINQ can be used for more than just database exploration and can do it on C# collections

 //C# statement tests in linqpad


var teachers = new string[] {"Dan", "Don", "Sam", "Gerry", "Ed"};

//These are SQL Like statements
var DNameTeachers = from person in teachers
					where person.StartsWith("D")
					select person;
					
DNameTeachers.Dump();



//int count = teachers.Length;
//count.Dump("Number of Teachers"); //.Dump is an extention method that goes on all objects and takes the value of it and takes the datatype value of it and displays it
//teachers.Dump(); //It will display out as a table because of .Dump


//How to add a connection
//Add connection
//server name = "."
//click specify new or esiting database and select "MyDatabasesName"
//Now in the left menu there will be a dropdown of the database and all its tables,views and procedures now


//Now typing a tables name and just hitting execute(f5) i will get all the data in the table
Products

//How to filter the results from the query
//item is a products item
//the item datatype is infered
from item in Products
where item.ProductName.Contains("Chef")
select item
//^this can run, just highlight and hit f5

//Another example
from item in Products
where item.Category.CategoryName == "Beverages"
select item

//Another example where it will bring back only the values i want to see
from item in Products
where item.Category.CategoryName == "Beverages"
select new
{
//This is an Initializer list
	item.ProductName,
	item.QuantityPerUnit,
	item.UnitPrice
	
	//I can change the column headings by doing this
	//Name = item.ProductName
}












