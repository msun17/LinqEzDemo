<Query Kind="Statements">
  <Connection>
    <ID>da5c10b2-9327-4ba6-8720-14216d7ff385</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//find the waiter with the most bills
// a) get a list of bill counts by waiter and dertmine the max
var maxbillcount = (from x in Waiters
					select x.Bills.Count()).Max();
// n) using the maxbillcount on the where clause, find
// 		the waiter that matches the count
var BestWaiter = from x in Waiters
				//where x.Bills.Count() == maxbillcount
				select new {
				Name = x.FirstName + " " + x.LastName,
				billcount = x.Bills.Count()
				};
				
BestWaiter.Dump();


//create a dataset that has an unknown number of records
// associate with a date value.
//a list of all bills associated with the waiter. list all waiters.
//the inner nested query uses the associated bill records
// of the currently processing Waiter --> x.Collection
var waiterbills = from x in Waiters 
					orderby x.LastName, x.FirstName
					select new{
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0
										select new {
											BillId = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
											}
										)
							};
waiterbills.Dump();