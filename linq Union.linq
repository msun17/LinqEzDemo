<Query Kind="Expression">
  <Connection>
    <ID>5eb4a27e-758c-45dc-afc1-a8f0a4963c94</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// syntax style for .Union() is
// (query).Union(query2).Union(queryn).OrderBy(firstsortfield).ThenBy(anohtersortfield)

//to get both albums with tracks and without tracks, -->use .Union
//rules
//number of columns the same
//colum datatyoe is same
//for odrdering the unioned queries use the name of the anonymos data fiels
(from x in Albums
where x.Tracks.Count() > 0
orderby x.Title
select new {
		Title = x.Title,
		TotalTracksForAlbum = x.Tracks.Count(),
		TotalPriceForalbumtracks = x.Tracks.Sum(y => y.UnitPrice),
		AverageTrackLength = x.Tracks.Average(y => y.Milliseconds)/1000
}
).Union(
from x in Albums
where x.Tracks.Count() == 0
select new {
		Title = x.Title,
		TotalTracksForAlbum = 0,
		TotalPriceForalbumtracks = 0.00m,
		AverageTrackLength= 0.00,
		//AverageTrackLength = x.Tracks.Average(y => y.Milliseconds)/1000
}
).OrderBy(y => y.TotalTracksForAlbum).ThenBy(y => y.Title)