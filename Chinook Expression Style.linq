<Query Kind="Expression">
  <Connection>
    <ID>5eb4a27e-758c-45dc-afc1-a8f0a4963c94</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/* 
Linq expressions, statements, programs are written useing C# syntex
*/

//query syntax list all records from entity
from x in Artists 
select x


//method syntax list all records from entity
Artists.Select(X =>X)

//sort albums by releasedate (most current) by title 
from x in Albums
orderby x.ReleaseYear descending, x.Title
select x

//list all albums belonging to artists
//the select is obtaining a subset of attributes from 
//the choosen tables
//the new {} os called anonymous data set
//anonymous datasets are IOrderedQuearyable<>
from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new {
		x.Artist.Name,
		x.Title
		}

//list all albums belonging to artists where a condition exists
//find albums released ina particular year