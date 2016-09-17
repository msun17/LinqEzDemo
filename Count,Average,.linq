<Query Kind="Expression">
  <Connection>
    <ID>5eb4a27e-758c-45dc-afc1-a8f0a4963c94</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//List of all customers suggested by the employee Jane Peacock
//List lastname, firstname, city, sate, phone, and email.
//this ample requires a subset of the entity  record
//the data needs to be filter for specific select thus a where is needed
//using the navigation name on Customer, one can access the the 
//		associate Employee record
//reminder: this is C# syntax and thus appropriate methods can be used .Equals()
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane")
	&& x.SupportRepIdEmployee.LastName.Equals("Peacock")

orderby x.LastName, x.FirstName
select new {
		Name = x.LastName + ", " + x.FirstName,
		City = x.City,
		State = x.State,
		Phone = x.Phone,
		Email = x.Email
}
//list all the Albums nad the total number of tracks for that album
//For aggregrates it is best to consider doing parent child direction
//aggregrates are used against collections (multiple records)
//null error could occur if a collection is empty for specific aggregrate(s)
//	Such as sum() thus your may need to filter (Where) certain 
//	records from your query
//Count() count the number of instances of the collection referenced
//Sum() totals a specific field, thus  you will likely need to use
//	a delegate to indicate the collection instance attribute to be used
//Average() Averages a specific field/expression, thus you will likely need
//  a delegate to indicate the collection instance attribute to be used
from x in Albums
where x.Tracks.Count() > 0
orderby x.Title
select new {
		Title = x.Title,
		TotalTracksForAlbum = x.Tracks.Count(),
		//find the total price for each set of album tracks
		TotalPriceForalbumtracks = x.Tracks.Sum(y => y.UnitPrice),
		//find the aveage length of the album tracks in seconds.
		AverageTrackLength = x.Tracks.Average(y => y.Milliseconds)/1000
}
//find the most popular mediatype for the tracks 
from x in Tracks
orderby x.Name
select new {
		Name = x.Name,
		TotalTracksForMediatype = x.MediaTypeId.Count()
}
