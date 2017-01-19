<Query Kind="Expression">
  <Connection>
    <ID>bd054115-557e-480f-be7f-9383653ebdfc</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//create a list of employees and their related customers they support
//aggregrate methods are executed against collections
//the many end of a navigation property is considered a collection
//when using a subquery with the collection source being a navigation 
//   property, only the records from the navigation collection (Customer) 
//   that belong to the  navigation parent (Employee) are considered
from x in Employees
where x.SupportRepIdCustomers.Count() >0
select new{
	Title = x.Title,
	Name = x.FirstName + " " + x.LastName,
	Phone =x.Phone,
	Customers = from y in x.SupportRepIdCustomers
				orderby y.LastName, y.FirstName
				select new{
					Name = y.LastName + ", " + y.FirstName,
					Company = y.Company,
					Phone = y.Phone,
					Email = y.Email
				}
}
