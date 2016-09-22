<Query Kind="Program">
  <Connection>
    <ID>da5c10b2-9327-4ba6-8720-14216d7ff385</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
//A list of bill counts for all Waiters
//This query will create a flat dataset
//The columns are native datatypes (ie int, string, double, ...)
//One is not concerned with the repeated data in a column
//Instead of using an anonymous datatype (new{..})
//		we wish to use a defined class definition
var BestWaiter = from x in Waiters
				select new WaiterBillCounts {
				Name = x.FirstName + " " + x.LastName,
				TCount = x.Bills.Count()
				};
				
BestWaiter.Dump();

var parmMonth = 5;
var parmYear = 2014;
var waiterbills = from x in Waiters 
					where x.LastName.Contains("k")
					orderby x.LastName, x.FirstName
					select new WaiterBills{
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0 //&& y.BillDate.Month = parmMonth && y.BillDate.Year = parmYear
										select new BillIteamSummary {
											BillId = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
											}
										).ToList() 
							};
waiterbills.Dump();
}

// Define other methods and classes here
//An example of a POCO class(flat)
public class WaiterBillCounts
{
	//Whatever the recieving field on your query in your select
	//there appears a property of that name in this class
	public string Name{get;set; }
	public int TCount{get;set;}
}

public class BillIteamSummary
{
	public int BillId{get;set; }
	public DateTime BillDate{get;set;}
	public int? TableID{get;set;}
	public decimal Total{get;set;}
}

public class WaiterBills
{
	public string Name{get;set; }
	public int TotalBillCount{get;set;}
	public List<BillIteamSummary> BillInfo{get;set;}
}