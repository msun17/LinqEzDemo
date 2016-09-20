<Query Kind="Statements">
  <Connection>
    <ID>5eb4a27e-758c-45dc-afc1-a8f0a4963c94</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//a statemnt has a receiving variable which is set
//by the result of a query

//when you need to use multiple steps
//to solve a problem, switch your Language
//choice to either Statement or Program


//What is the most popular mediatype for tracks
var maxcount = (from x in MediaTypes
				select x.Tracks.Count()).Max();
				
//to display the contents of a variable in LinqPad
//you use the method .Dump()
maxcount.Dump();

//to filter data you can use Where clause
//use a previously creae variable value in 
// a following statement
var mediatypecounts = from x in MediaTypes
						where x.Tracks.Count() == maxcount
						select new{
						Name = x.Name,
						TarckCount = x.Tracks.Count()
						};
mediatypecounts.Dump();					
//can this set of statements be written as one complete query?
//the answer: possibly; and in this case yes
//int this example maxcount could be exchanged for the query that 
// actually created the value in the first place
//this subsitution query is a nested query(subquery)
//the nested query needs its on instance indentifier
var mediatypecountsNested = from x in MediaTypes
						where x.Tracks.Count() == (from y in MediaTypes
												select y.Tracks.Count()).Max()
						select new{
						Name = x.Name,
						TarckCount = x.Tracks.Count()
						};
mediatypecountsNested.Dump();		

//using a method syntax to determine the count value for the where express
//this demonstrates that queries can be constructed using 
// both query syntax and method syntax
var mediatypecountsMethod = from x in MediaTypes
						where x.Tracks.Count() == MediaTypes.Select(y => y.Tracks.Count()).Max()
						select new{
						Name = x.Name,
						TarckCount = x.Tracks.Count()
						};
mediatypecountsMethod.Dump();